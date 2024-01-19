using CruiseDAL;
using CruiseDAL.DataObjects;
using CruiseDAL.UpConvert;
using CruiseDAL.V3.Models;
using CruiseDAL.V3.Sync;
using CruiseDesign.Services;
using Microsoft.AppCenter.Crashes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CruiseDesign.Design_Pages
{
    public partial class CreateProduction : Form
    {
        protected ILogger Logger { get; }
        protected IDialogService DialogService { get; }

        public CruiseDesignFileContext FileContext { get; }

        public bool IsUsingV3File => FileContext.IsUsingV3File;
        public string V3FilePath => FileContext.V3FilePath;
        private string ReconFilePath => FileContext.ReconFilePath;
        public DAL DesignDb => FileContext.DesignDb;

        private string _destPath;
        private string _defaultProductionFileName;

        protected CreateProduction()
        {
            InitializeComponent();
        }

        public CreateProduction(ICruiseDesignFileContextProvider contextProvider, ILogger<CreateProduction> logger, IDialogService dialogService)
            : this()
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            DialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            var fileContext = FileContext = contextProvider.CurrentFileContext;

            var curSale = DesignDb.From<SaleDO>().Query().First();
            string saleNumber = curSale.SaleNumber;
            string saleName = curSale.Name;
            _defaultProductionFileName = saleNumber + "_" + saleName + "_TS.cruise";

            InitializeDatabaseTables();

            reconStrataSelectedItemsGridView.SelectedItems = new List<StratumStatsDO>();
        }

        public class CreateProdParams
        {
            public string ProductionFilePath;
            public string[] SelectedStratumCodes;
        };

        private void InitializeDatabaseTables()
        {
            //get stratumstats used == 1 methods == (pnt, fix, pcm, fcm)
            var reconStrata = new BindingList<StratumStatsDO>();

            var strata = DesignDb.From<StratumStatsDO>()
                .Where("StratumStats.Used = 1")
                .OrderBy("StratumStats.Code")
                .Query();

            // loop through stratumstats
            foreach (StratumStatsDO st in strata)
            {
                // get sgstats where reconplots > 0
                if (st.Method == "PNT"
                    || st.Method == "FIX"
                    || st.Method == "PCM"
                    || st.Method == "FCM"
                    || st.Method == "FIXCNT")
                {
                    var sgStatCount = DesignDb.From<SampleGroupStatsDO>()
                        .Where("StratumStats_CN = @p1 and SampleGroupStats.ReconPlots > 0")
                        .Read(st.StratumStats_CN).Count();

                    // if count > 0, add to reconStratum
                    if (sgStatCount > 0)
                    { reconStrata.Add(st); }
                }
            }
            //reconStratum = new BindingList<StratumStatsDO>(cdDAL.Read<StratumStatsDO>("StratumStats", "JOIN SampleGroupStats ON StratumStats.StratumStats_CN = SampleGroupStats.StratumStats_CN AND StratumStats.Used = 1 AND SampleGroupStats.ReconPlots > 0 Where StratumStats.Method = ? OR StratumStats.Method = ? OR StratumStats.Method = ? OR StratumStats.Method = ? ORDER BY StratumStats.Code", "PNT","FIX","PCM","FCM"));
            if (reconStrata.Count <= 0)
            {
                labelRecon.Text = "No Recon Data Found.";
            }

            reconStrataBindingSource.DataSource = reconStrata;
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            var destPath = DialogService.AskSaveProductionFilePath(IsUsingV3File, _defaultProductionFileName);

            if (destPath != null && destPath == ReconFilePath)
            {
                DialogService.ShowMessage("Cannot overwrite the Recon file.\nPlease select a different file name.");
                destPath = null;
            }

            if (!CruiseDesignFileContext.EnsurePathValid(destPath, Logger, DialogService))
            {
                return;
            }

            _destPath = destPath;
            textBoxFile.Text = _destPath;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            if (_destPath == null)
            {
                DialogService.ShowMessage("Please enter filename.", "Information");
                return;
            }

            var dataFiles = new CreateProdParams
            {
                ProductionFilePath = _destPath,
                SelectedStratumCodes = reconStrataSelectedItemsGridView.SelectedItems
                  .OfType<StratumStatsDO>()
                  .Select(stRec => stRec.Code).ToArray(),
            };

            this.UseWaitCursor = true;
            pictureBox1.Visible = true;
            panel1.Enabled = false;
            reconStrataSelectedItemsGridView.Enabled = false;
            buttonCancel.Enabled = false;
            buttonCreate.Enabled = false;

            var useFreqSelected = radioButton1.Checked;
            var useBigBAFFPSSelected = radioButton2.Checked;

            await CreateProductionFileAsync(dataFiles, useFreqSelected, useBigBAFFPSSelected);

            pictureBox1.Visible = false;
            panel1.Enabled = true;
            reconStrataSelectedItemsGridView.Enabled = true;
            buttonCancel.Enabled = true;
            buttonCreate.Enabled = true;
            this.UseWaitCursor = false;

            string sMes = "Production Cruise File has been created.\r\n" +
                          "Open in Cruise Manager - Customize to create the Tally Setup forms before putting file on Data Recorder";
            DialogService.ShowMessage(sMes);

            Close();
        }

        public Task CreateProductionFileAsync(CreateProdParams dataFiles, bool useFreqSelected, bool useBigBAFFPSSelected)
        {
            return Task.Run(() => CreateProductionFile(dataFiles, FileContext, useFreqSelected, useBigBAFFPSSelected, DialogService, Logger));
        }

        public static void CreateProductionFile(CreateProdParams dataFiles, CruiseDesignFileContext fileContext, bool useFreqSelected, bool useBigBAFFPSSelected, IDialogService dialogService, ILogger logger)
        {
            var hasReconData = dataFiles.SelectedStratumCodes.Any()
                && File.Exists(fileContext.ReconFilePath);

            var prodFilePath = dataFiles.ProductionFilePath;
            var prodExtention = Path.GetExtension(prodFilePath).ToLower();
            var isCreateingV3ProdFile = prodExtention == ".crz3";

            string v3TemplateSource = null;
            if (isCreateingV3ProdFile)
            {

                v3TemplateSource = dialogService.AskSelectCreateProductionV3Template(fileContext.V3FilePath);
            }


            // create an in memory db to create our production db
            // we will copy the db over to a file later on in the process
            // depending on what our final output file will be
            var productionDb = new DAL();
            DAL reconDb = null;
            try
            {
                if (hasReconData)
                {
                    try
                    {
                        reconDb = new DAL(fileContext.ReconFilePath, false);
                    }
                    catch(IOException ex)
                    {
                        var msg = "Error Opening Recon File";
                        logger.LogError(ex, msg);
                        dialogService.ShowMessage(msg);
                        return;
                    }
                }

                copyTablesToFScruise(fileContext.DesignDb, productionDb);

                copyPopulations(fileContext.DesignDb,
                   reconDb,
                   productionDb,
                   hasReconData,
                   dataFiles.SelectedStratumCodes,
                   useFreqSelected,
                   useBigBAFFPSSelected);

                
                if (isCreateingV3ProdFile)
                {
                    var tmpV2Path = prodFilePath + ".tmp";
                    // dump the in memory production db into a temporary file
                    // currently the Migration requires a on disk db as the source db
                    // use CruiseDatastore because we don't need temp database initialized
                    // the BackupDatabase method will copy over the schema as well as the all the data
                    // from our in memory production db
                    // and it wont yell at us for using an unrecognized file extension
                    using var tmpV2Db = new CruiseDatastore(tmpV2Path, true, null, null);
                    productionDb.BackupDatabase(tmpV2Db);

                    try
                    {
                        using var v3Db = new CruiseDatastore_V3();
                        var migrator = new Migrator();
                        migrator.MigrateFromV2ToV3(tmpV2Db, v3Db, Environment.UserName);

                        if (!string.IsNullOrEmpty(v3TemplateSource) && File.Exists(v3TemplateSource))
                        {
                            CopyV3TemplateData(v3TemplateSource, v3Db);
                        }

                        

                        v3Db.BackupDatabase(prodFilePath);
                    }
                    catch (FMSC.ORM.ConstraintException e)
                    {
                        var message = "Data Error. Ensure cruise has been processed using latest version of Cruise Processing";
                        logger.LogError(e, message);
                        dialogService.ShowMessage(message);
                    }
                    catch (Exception e)
                    {
                        var message = "Create Production: Migrate Error (" + e.GetType().Name + ")";
                        logger.LogError(e, message);
                        dialogService.ShowMessage(message);
                        throw;
                    }
                    finally
                    {
                        // clean up our temporary file
                        File.Delete(tmpV2Path);
                    }
                }
                else
                {
                    productionDb.BackupDatabase(prodFilePath);
                }
            }
            finally
            {
                reconDb?.Dispose();
                productionDb?.Dispose();
            }
        }

        private static void CopyV3TemplateData(string v3TemplateSource, CruiseDatastore_V3 v3Db)
        {
            var templateSourceDb = new CruiseDatastore_V3(v3TemplateSource);
            var sourceCruiseID = templateSourceDb.From<CruiseDAL.V3.Models.Cruise>().Query().First().CruiseID;
            var destCruiseID = v3Db.From<CruiseDAL.V3.Models.Cruise>().Query().First().CruiseID;

            if (true)
            {
                v3Db.Execute("DELETE FROM " + nameof(TreeFieldHeading) + ";");
                v3Db.Execute("DELETE FROM " + nameof(LogFieldHeading) + ";");
                v3Db.Execute("DELETE FROM " + nameof(StratumTemplateTreeFieldSetup) + ";");
                v3Db.Execute("DELETE FROM " + nameof(StratumTemplateLogFieldSetup) + ";");
                v3Db.Execute("DELETE FROM " + nameof(StratumTemplate) + ";");
                v3Db.Execute("DELETE FROM " + nameof(TreeDefaultValue) + ";");
                v3Db.Execute("DELETE FROM " + nameof(TreeAuditRuleSelector) + ";");
                v3Db.Execute("DELETE FROM " + nameof(TreeAuditRule) + ";");
                v3Db.Execute("DELETE FROM " + nameof(LogGradeAuditRule) + ";");
                v3Db.Execute("DELETE FROM " + nameof(CruiseDAL.V3.Models.Reports) + ";");
                v3Db.Execute("DELETE FROM " + nameof(VolumeEquation) + ";");
                v3Db.Execute("DELETE FROM " + nameof(ValueEquation) + ";");
                v3Db.Execute("DELETE FROM " + nameof(BiomassEquation) + ";");
            }

            var templateCopier = new TemplateCopier() { DefaultOnConflictOption = Backpack.SqlBuilder.OnConflictOption.Ignore };
            templateCopier.Copy(templateSourceDb, v3Db, sourceCruiseID, destCruiseID);
        }

        private static void copyTablesToFScruise(DAL fromDb, DAL toDb)
        {
            // copy Sale table
            copySaleTable(fromDb, toDb);
            //copy CuttingUnit table
            foreach (CuttingUnitDO fld in fromDb.From<CuttingUnitDO>().Query())
            {
                toDb.Insert(fld, option: Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy Strata
            copyStratumToFScruise(fromDb, toDb);
            //copy cuttingUnitStratum
            foreach (var cust in fromDb.From<CuttingUnitStratumDO>().Query())
            {
                toDb.Insert(cust, option: Backpack.SqlBuilder.OnConflictOption.Replace);
            }

            //copy TreeDefaultValues table
            foreach (TreeDefaultValueDO fld in fromDb.From<TreeDefaultValueDO>().Query())
            {
                toDb.Insert(fld, option: Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy logfieldsetupdefault table
            foreach (LogFieldSetupDefaultDO fld in fromDb.From<LogFieldSetupDefaultDO>().Query())
            {
                toDb.Insert(fld, option: Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy logfieldsetup
            //         fsDAL.DirectCopy(cDAL, CruiseDAL.Schema.LOGFIELDSETUP._NAME, null, OnConflictOption.Ignore);
            //copy messagelog
            foreach (MessageLogDO fld in fromDb.From<MessageLogDO>().Query())
            {
                toDb.Insert(fld, option: Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy reports
            foreach (ReportsDO fld in fromDb.From<ReportsDO>().Query())
            {
                toDb.Insert(fld, option: Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy treefieldsetupdefault
            foreach (TreeFieldSetupDefaultDO fld in fromDb.From<TreeFieldSetupDefaultDO>().Query())
            {
                toDb.Insert(fld, option: Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy volumeequations
            foreach (VolumeEquationDO fld in fromDb.From<VolumeEquationDO>().Query())
            {
                toDb.Insert(fld, option: Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy treeauditvalue
            foreach (TreeAuditValueDO fld in fromDb.From<TreeAuditValueDO>().Query())
            {
                toDb.Insert(fld, option: Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy treedefaultvaluetreeauditvalue
            foreach (TreeDefaultValueTreeAuditValueDO fld in fromDb.From<TreeDefaultValueTreeAuditValueDO>().Query())
            {
                toDb.Insert(fld, option: Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy tally
            foreach (TallyDO fld in fromDb.From<TallyDO>().Query())
            {
                toDb.Insert(fld, "Tally", Backpack.SqlBuilder.OnConflictOption.Replace);
            }

            foreach (var lm in fromDb.From<LogMatrixDO>().Query())
            {
                toDb.Insert(lm, option: Backpack.SqlBuilder.OnConflictOption.Replace);
            }
           

            toDb.LogMessage("Create Production, Copied Data From File: " + fromDb.Path);
        }

        private static void copyStratumToFScruise(DAL fromDb, DAL toDb)
        {
            //loop through design stratum table
            var strata = fromDb.From<StratumDO>().Query()
               .Select((curStr) =>
               {
                   StratumDO fsStr = new StratumDO();
                   fsStr.Code = curStr.Code;
                   fsStr.Description = curStr.Description;
                   fsStr.Method = curStr.Method;
                   fsStr.BasalAreaFactor = curStr.BasalAreaFactor;
                   fsStr.FixedPlotSize = curStr.FixedPlotSize;
                   fsStr.KZ3PPNT = curStr.KZ3PPNT;
                   fsStr.YieldComponent = curStr.YieldComponent;
                   fsStr.Year = curStr.Year;
                   fsStr.Month = curStr.Month;
                   return fsStr;
               });

            foreach (var st in strata)
            {
                toDb.Insert(st);
            }
        }

        private static void copySaleTable(DAL fromDb, DAL toDb)
        {
            //open sale table
            SaleDO sale = fromDb.From<SaleDO>().Query().First();
            SaleDO fsSale = new SaleDO();

            fsSale.Purpose = "Timber Sale";
            fsSale.SaleNumber = sale.SaleNumber;
            fsSale.Name = sale.Name;
            fsSale.Region = sale.Region;
            fsSale.Forest = sale.Forest;
            fsSale.District = sale.District;
            fsSale.DefaultUOM = sale.DefaultUOM;
            fsSale.CalendarYear = sale.CalendarYear;
            fsSale.LogGradingEnabled = sale.LogGradingEnabled;
            fsSale.MeasurementYear = sale.MeasurementYear;
            fsSale.Remarks = sale.Remarks;

            toDb.Insert(fsSale, option: Backpack.SqlBuilder.OnConflictOption.Replace);

            if (toDb.From<CruiseMethodsDO>().Count() < 4)
            {
                for (int i = 0; i < 12; i++)
                {
                    var cruiseMethod = new CruiseMethodsDO();
                    SetCruiseMethodValues(cruiseMethod, i);
                    toDb.Insert(cruiseMethod, option: Backpack.SqlBuilder.OnConflictOption.Replace);
                }
            }

            void SetCruiseMethodValues(CruiseMethodsDO fsMeth, int num)
            {
                switch (num)
                {
                    case 0:
                        fsMeth.Code = "100";
                        fsMeth.FriendlyValue = "Classic 100%";
                        break;

                    case 1:
                        fsMeth.Code = "STR";
                        fsMeth.FriendlyValue = "Sample Tree";
                        break;

                    case 2:
                        fsMeth.Code = "3P";
                        fsMeth.FriendlyValue = "Classic 3P";
                        break;

                    case 3:
                        fsMeth.Code = "FIX";
                        fsMeth.FriendlyValue = "Fixed Plot";
                        break;

                    case 4:
                        fsMeth.Code = "FCM";
                        fsMeth.FriendlyValue = "Fixed Count/Measure";
                        break;

                    case 5:
                        fsMeth.Code = "F3P";
                        fsMeth.FriendlyValue = "Fixed 3P";
                        break;

                    case 6:
                        fsMeth.Code = "PNT";
                        fsMeth.FriendlyValue = "Point";
                        break;

                    case 7:
                        fsMeth.Code = "PCM";
                        fsMeth.FriendlyValue = "Point Count/Measure";
                        break;

                    case 8:
                        fsMeth.Code = "P3P";
                        fsMeth.FriendlyValue = "Point 3P";
                        break;

                    case 9:
                        fsMeth.Code = "S3P";
                        fsMeth.FriendlyValue = "Sample Tree with 3P subsample";
                        break;

                    case 10:
                        fsMeth.Code = "3PPNT";
                        fsMeth.FriendlyValue = "3P Point Biomass";
                        break;

                    case 11:
                        fsMeth.Code = "FIXCNT";
                        fsMeth.FriendlyValue = "Fixed Biomass";
                        break;
                }
            }
        }

        private static void copyPopulations(DAL designDb, DAL reconDb, DAL prodDb, bool hasReconData, string[] stRecCodes, bool useFreqSelected, bool useBigBAFFPSSelected)
        {
            //treeFields = new BindingList<TreeFieldSetupDO>((fsDAL.Read<TreeFieldSetupDO>("TreeFieldSetup", null, null)));
            //logFields = new BindingList<LogFieldSetupDO>();

            Random random = new Random();

            //       List<StratumStatsDO> cdStratumStats = new List<StratumStatsDO>(cDAL.Read<StratumStatsDO>("StratumStats", "JOIN Stratum ON StratumStats.Stratum_CN = Stratum.Stratum_CN AND StratumStats.Method = Stratum.Method AND StratumStats.Used = 1 ORDER BY Stratum.Code", null));
            // TODO do we need to join using method as well? like in the original query
            var cdStratumStats = designDb.From<StratumStatsDO>()
               .Join("Stratum AS s", "USING (Stratum_CN)")
               .Where("StratumStats.Used = 1")
               .OrderBy("s.Code").Query().ToArray();

            foreach (StratumStatsDO stStat in cdStratumStats)
            {
                // check if recon data is to be saved
                string stCode = stStat.Code;
                var cdStr = prodDb.From<StratumDO>()
                   .Where("Code = @p1")
                   .Query(stCode).FirstOrDefault();

                var curStratumCN = cdStr.Stratum_CN.Value;

                var method = "100";
                if (hasReconData)
                {
                    if (stStat.Method == "PNT" || stStat.Method == "PCM")
                        method = "PNT";
                    else if (stStat.Method == "FIX" || stStat.Method == "FCM")
                        method = "FIX";
                    else if (stStat.Method == "FIXCNT")
                        method = "FIXCNT";
                }

                var myPlots = new List<PlotDO>();
                var myTree = new List<TreeDO>();
                var myLogs = new List<LogDO>();
                var setRecData = hasReconData && stRecCodes.Contains(stCode);
                if (setRecData)
                {
                    if (method == "PNT")
                        //  myPlots = (rDAL.Read<PlotDO>("Plot", "JOIN Stratum ON Plot.Stratum_CN = Stratum.Stratum_CN WHERE Stratum.Method = ? AND Stratum.BasalAreaFactor = ?", method, myStStats.BasalAreaFactor));
                        myPlots = reconDb.From<PlotDO>()
                                 .Join("Stratum AS s", "USING (Stratum_CN)")
                                 .Where("s.Method = @p1 AND s.BasalAreaFactor = @p2")
                                 .Query(method, stStat.BasalAreaFactor).ToList();
                    else
                        //   myPlots = (rDAL.Read<PlotDO>("Plot", "JOIN Stratum ON Plot.Stratum_CN = Stratum.Stratum_CN WHERE Stratum.Method = ? AND Stratum.FixedPlotSize = ?", method, myStStats.FixedPlotSize));
                        myPlots = reconDb.From<PlotDO>()
                                 .Join("Stratum AS s", "USING (Stratum_CN)")
                                 .Where("s.Method = @p1 AND s.FixedPlotSize = @p2")
                                 .Query(method, stStat.FixedPlotSize).ToList();

                    myTree = reconDb.From<TreeDO>().Query().ToList();
                    myLogs = reconDb.From<LogDO>().Query().ToList();
                }

                // get fsDAl stratum record
                // List<SampleGroupStatsDO> mySgStats = new List<SampleGroupStatsDO>(cDAL.Read<SampleGroupStatsDO>("SampleGroupStats", "Where StratumStats_CN = ?", myStStats.StratumStats_CN));
                var mySgStats = designDb.From<SampleGroupStatsDO>().Where("StratumStats_CN = @p1").Query(stStat.StratumStats_CN);
                // loop through sample groups
                var first = true; // only copy plot data for first sg in st
                foreach (var sgStats in mySgStats)
                {
                    var measHit = 0;
                    SampleGroupDO fsSg = new SampleGroupDO(prodDb);
                    // save sample group information
                    fsSg.Stratum_CN = curStratumCN;
                    fsSg.Code = sgStats.Code;
                    fsSg.Description = sgStats.Description;
                    fsSg.CutLeave = sgStats.CutLeave;
                    fsSg.UOM = sgStats.UOM;
                    fsSg.PrimaryProduct = sgStats.PrimaryProduct;
                    fsSg.SecondaryProduct = sgStats.SecondaryProduct;
                    fsSg.DefaultLiveDead = sgStats.DefaultLiveDead;
                    fsSg.KZ = sgStats.KZ;
                    fsSg.InsuranceFrequency = sgStats.InsuranceFrequency;
                    if (stStat.Method == "PCM" || stStat.Method == "FCM")
                    {
                        if (useFreqSelected)
                        {
                            fsSg.SamplingFrequency = sgStats.SamplingFrequency;
                            // find random start
                            int freq = (int)fsSg.SamplingFrequency;
                            measHit = random.Next(1, freq);
                            fsSg.BigBAF = 0;
                            fsSg.SmallFPS = 0;
                        }
                        else
                        {
                            fsSg.SamplingFrequency = 0;
                            fsSg.BigBAF = (float)Math.Floor((double)sgStats.BigBAF);
                            fsSg.SmallFPS = (float)Math.Floor((double)sgStats.BigFIX);
                        }
                    }
                    else
                    {
                        fsSg.SamplingFrequency = sgStats.SamplingFrequency;
                        fsSg.BigBAF = 0;
                        fsSg.SmallFPS = 0;
                    }
                    // find treedefaultvalues
                    //sgStats.TreeDefaultValueStats.Populate();
                    // find StratumStats_CN where Stratum_CN and SgSet and method = "100"
                    // find SampleGroupStats_CN where StrataumStats_CN and code = sgStats.code
                    long? sgStatsCN100pct;
                    if (stStat.SgSetDescription == "Comparison Cruise" || method == "FIXCNT")
                        sgStatsCN100pct = findSgStatCN100(designDb, stStat.Stratum_CN, sgStats.Code, sgStats.SgSet, stStat.Method);
                    else
                        sgStatsCN100pct = findSgStatCN100(designDb, stStat.Stratum_CN, sgStats.Code, sgStats.SgSet, "100");

                    var treeDefaults = designDb.From<TreeDefaultValueDO>()
                       .Join("SampleGroupStatsTreeDefaultValue", "USING (TreeDefaultValue_CN)")
                       .Where("SampleGroupStats_CN = @p1")
                       .Query(sgStatsCN100pct);

                    foreach (var tdv in treeDefaults)
                    {
                        fsSg.TreeDefaultValues.Add(tdv);
                    }
                    fsSg.Save();
                    fsSg.TreeDefaultValues.Save();
                    // if recon can be saved
                    if (setRecData)
                    {
                        getReconData(stStat,
                           sgStats,
                           designDb,
                           reconDb,
                           prodDb,
                           myPlots,
                           myTree,
                           myLogs,
                           curStratumCN,
                           fsSg.SampleGroup_CN,
                           sgStatsCN100pct,
                           first,
                           measHit,
                           useFreqSelected,
                           useBigBAFFPSSelected);
                        first = false;
                    }
                }

                // check stratum plots for null plots
                if (hasReconData)
                {
                    SetNullPlots(prodDb, curStratumCN);
                }
                //getTreeFieldSetup(cDAL,fsDAL,myStStats);
                //if(logData)
                //   getLogFieldSetup(cDAL,fsDAL,myStStats);
            }
        }

        private static void SetNullPlots(DAL db, long stratumCN)
        {
            //for production file
            //  get all plots for thisStrCN
            var myPlotList = db.From<PlotDO>().Where("Stratum_CN = @p1").Query(stratumCN);

            foreach (PlotDO curPlot in myPlotList)
            {
                // loop through, getting all tree records for each plot
                var treeCount = db.From<TreeDO>().Where("Plot_CN = @p1").Count(curPlot.Plot_CN);
                if (treeCount <= 0)
                {
                    curPlot.IsEmpty = "True";
                    curPlot.Save();
                }
            }
        }

        public static long? findSgStatCN100(DAL db, long? strCN, string code, long sgSet, string meth)
        {
            // find StratumStats_CN where Stratum_CN and SgSet and method = "100"
            var strStat100 = db.From<StratumStatsDO>()
                   .Where("Stratum_CN = @p1 AND method = @p2 AND SgSet = @p3")
                  .Query(strCN, meth, sgSet).FirstOrDefault();

            var sgStat100 = db.From<SampleGroupStatsDO>()
                  .Where("StratumStats_CN = @p1 AND Code = @p2")
                  .Read(strStat100.StratumStats_CN, code).FirstOrDefault();

            return sgStat100.SampleGroupStats_CN;
            // find SampleGroupStats_CN where StrataumStats_CN and code = sgStats.code
        }

        //public void getTreeFieldSetup(DAL cDAL, DAL fsDAL, StratumStatsDO myStStats)
        //{
        //   //select from TreeFieldSetupDefault where method = stratum.method
        //   //            List<TreeFieldSetupDefaultDO> treeFieldDefaults = new List<TreeFieldSetupDefaultDO>(cDAL.Read<TreeFieldSetupDefaultDO>("TreeFieldSetupDefault", "WHERE Method = ? ORDER BY FieldOrder", myStStats.Method));
        //   //            treeFields = new BindingList<TreeFieldSetupDO>((fsDAL.Read<TreeFieldSetupDO>("TreeFieldSetup", null, null)));
        //   List<TreeFieldSetupDefaultDO> treeFieldDefaults = cDAL.From<TreeFieldSetupDefaultDO>().Where("Method = @p1").OrderBy("FieldOrder").Read(myStStats.Method).ToList();
        //   treeFields = new BindingList<TreeFieldSetupDO>(fsDAL.From<TreeFieldSetupDO>().Read().ToList());

        //   foreach (TreeFieldSetupDefaultDO tfd in treeFieldDefaults)
        //   {
        //      TreeFieldSetupDO tfs = new TreeFieldSetupDO();
        //      tfs.Stratum_CN = thisStrCN;
        //      tfs.Field = tfd.Field;
        //      tfs.FieldOrder = tfd.FieldOrder;
        //      tfs.ColumnType = tfd.ColumnType;
        //      tfs.Heading = tfd.Heading;
        //      tfs.Width = tfd.Width;
        //      tfs.Format = tfd.Format;
        //      tfs.Behavior = tfd.Behavior;

        //      treeFields.Add(tfs);
        //   }
        //   foreach (TreeFieldSetupDO thisSetup in treeFields)
        //   {
        //      thisSetup.Save();
        //   }
        //}

        //public void getLogFieldSetup(DAL cDAL, DAL fsDAL, StratumStatsDO myStStats)
        //{
        //   List<LogFieldSetupDefaultDO> logFieldDefaults = cDAL.From<LogFieldSetupDefaultDO>().Read().ToList();
        //   foreach (LogFieldSetupDefaultDO lfd in logFieldDefaults)
        //   {
        //      LogFieldSetupDO lfs = new LogFieldSetupDO();
        //      lfs.Stratum_CN = thisStrCN;
        //      lfs.Field = lfd.Field;
        //      lfs.FieldOrder = lfd.FieldOrder;
        //      lfs.ColumnType = lfd.ColumnType;
        //      lfs.Heading = lfd.Heading;
        //      lfs.Width = lfd.Width;
        //      lfs.Format = lfd.Format;
        //      lfs.Behavior = lfd.Behavior;

        //      logFields.Add(lfs);
        //   }
        //   foreach (LogFieldSetupDO thisSetup in logFields)
        //   {
        //      thisSetup.Save();
        //   }
        //}

        public static int getReconData(StratumStatsDO curStrStats,
                                SampleGroupStatsDO curSgStats,
                                DAL cdDAL,
                                DAL rDAL, // rDAL not used here because all recon data is provided via myPlots, myTrees, myLogs
                                DAL fsDAL,
                                List<PlotDO> myPlots,
                                List<TreeDO> myTree,
                                List<LogDO> myLogs,
                                long curStratumCN,
                                long? sampleGroupCN,
                                long? sgStatsCN100,
                                bool first,
                                int measHit,
                                bool useFreqSelected,
                                bool useBigBAFFPSSelected)
        {
            int treeCnt = 1;

            curStrStats.Stratum.CuttingUnits.Populate();
            curSgStats.TreeDefaultValueStats.Populate();
            // loop through design units

            var treeDefaults = cdDAL.From<TreeDefaultValueDO>()
               .Join("SampleGroupStatsTreeDefaultValue", "USING (TreeDefaultValue_CN)")
               .Where("SampleGroupStats_CN = @p1")
               .Query(sgStatsCN100);

            double maxDbh = 200;
            double minDbh = 0;
            if (curSgStats.MinDbh > 0 && curSgStats.MaxDbh > 0)
            {
                maxDbh = curSgStats.MaxDbh + 0.0499;
                minDbh = curSgStats.MinDbh - 0.0500;
            }

            var units = cdDAL.From<CuttingUnitDO>()
               .Join("CuttingUnitStratum", "USING (CuttingUnit_CN)")
               .Where("CuttingUnitStratum.Stratum_CN = @p1")
               .Query(curStrStats.Stratum_CN).ToArray();
            foreach (CuttingUnitDO curUnit in units)
            {
                // get number of plots for stratum and cutting unit
                var myPlotList = (from plt in myPlots
                                  where plt.CuttingUnit_CN == curUnit.CuttingUnit_CN
                                  select plt).ToList();
                // loop through plots
                foreach (PlotDO curPlot in myPlotList)
                {
                    // if first time for stratum, save plots
                    var plotCN = CopyOrGetExistingPlot(fsDAL, curPlot, curStrStats.Stratum_CN, curStratumCN, first);

                    foreach (TreeDefaultValueDO curTdv in treeDefaults)
                    {
                        var plotSubpopTrees = (from tcv in myTree
                                               where tcv.SampleGroup.PrimaryProduct == curSgStats.PrimaryProduct
                                               && tcv.SampleGroup.CutLeave == curSgStats.CutLeave
                                               && tcv.Plot_CN == curPlot.Plot_CN
                                               && tcv.TreeDefaultValue_CN == curTdv.TreeDefaultValue_CN
                                               && tcv.DBH >= minDbh
                                               && tcv.DBH <= maxDbh
                                               select tcv).ToList();

                        foreach (TreeDO rTree in plotSubpopTrees)
                        {
                            var fsTree = new TreeDO(fsDAL);
                            fsTree.Stratum_CN = curStratumCN;
                            fsTree.TreeDefaultValue_CN = curTdv.TreeDefaultValue_CN;
                            fsTree.SampleGroup_CN = sampleGroupCN;
                            fsTree.Plot_CN = plotCN;
                            fsTree.CuttingUnit_CN = curUnit.CuttingUnit_CN;
                            fsTree.TreeNumber = rTree.TreeNumber;
                            fsTree.Species = rTree.Species;
                            if (curStrStats.Method == "PCM" || curStrStats.Method == "FCM")
                            {
                                if (useFreqSelected)
                                {
                                    // use frequency
                                    // check hit
                                    if (treeCnt == measHit)
                                    {
                                        fsTree.CountOrMeasure = "M";
                                        measHit += (int)curSgStats.SamplingFrequency;
                                    }
                                    else
                                        fsTree.CountOrMeasure = "C";
                                }
                                else if (useBigBAFFPSSelected)
                                {
                                    fsTree.CountOrMeasure = "C";
                                }
                                else
                                    fsTree.CountOrMeasure = "M";
                            }
                            else
                                fsTree.CountOrMeasure = rTree.CountOrMeasure;
                            treeCnt++;
                            fsTree.TreeCount = 1;
                            fsTree.SeenDefectPrimary = rTree.SeenDefectPrimary;
                            fsTree.SeenDefectSecondary = rTree.SeenDefectSecondary;
                            fsTree.RecoverablePrimary = rTree.RecoverablePrimary;
                            fsTree.Initials = rTree.Initials;
                            fsTree.LiveDead = rTree.LiveDead;
                            fsTree.Grade = rTree.Grade;
                            fsTree.HeightToFirstLiveLimb = rTree.HeightToFirstLiveLimb;
                            fsTree.DBH = rTree.DBH;
                            fsTree.DRC = rTree.DRC;
                            fsTree.TotalHeight = rTree.TotalHeight;
                            fsTree.MerchHeightPrimary = rTree.MerchHeightPrimary;
                            fsTree.MerchHeightSecondary = rTree.MerchHeightSecondary;
                            fsTree.FormClass = rTree.FormClass;
                            fsTree.UpperStemHeight = rTree.UpperStemHeight;
                            fsTree.TopDIBPrimary = rTree.TopDIBPrimary;
                            fsTree.TopDIBSecondary = rTree.TopDIBSecondary;
                            fsTree.Remarks = rTree.Remarks;

                            //fsTree.CreatedBy = rTree.CreatedBy;
                            //fsTree.CreatedDate = rTree.CreatedDate;

                            fsTree.Save();
                            // save logs
                            var myLogList = (from lcv in myLogs
                                             where lcv.Tree_CN == rTree.Tree_CN
                                             select lcv).ToList();

                            foreach (LogDO rLog in myLogList)
                            {
                                long? treeCN = fsTree.Tree_CN;
                                var fsLog = new LogDO(fsDAL);
                                fsLog.Tree_CN = treeCN;
                                fsLog.LogNumber = rLog.LogNumber;
                                fsLog.Grade = rLog.Grade;
                                fsLog.SeenDefect = rLog.SeenDefect;
                                fsLog.PercentRecoverable = rLog.PercentRecoverable;
                                fsLog.Length = rLog.Length;
                                fsLog.ExportGrade = rLog.ExportGrade;
                                fsLog.SmallEndDiameter = rLog.SmallEndDiameter;
                                fsLog.LargeEndDiameter = rLog.LargeEndDiameter;
                                fsLog.GrossBoardFoot = rLog.GrossBoardFoot;
                                fsLog.NetBoardFoot = rLog.NetBoardFoot;
                                fsLog.GrossCubicFoot = rLog.GrossCubicFoot;
                                fsLog.NetCubicFoot = rLog.NetCubicFoot;
                                fsLog.DIBClass = rLog.DIBClass;
                                fsLog.BarkThickness = rLog.BarkThickness;
                                fsLog.Save();
                            }
                        }
                    }
                }
            }
            return (0);
        }

        private static long? CopyOrGetExistingPlot(DAL fsDAL, PlotDO curPlot, long? stratumCN, long curStratumCN, bool first)
        {
            var fsPlot = fsDAL.From<PlotDO>()
               .Where("CuttingUnit_CN = @p1 and Stratum_CN = @p2 and PlotNumber = @p3")
               .Read(curPlot.CuttingUnit_CN, curStratumCN, curPlot.PlotNumber).FirstOrDefault();

            if (first && fsPlot == null)
            {
                fsPlot = new PlotDO(fsDAL);

                fsPlot.Stratum_CN = curStratumCN;
                fsPlot.CuttingUnit_CN = curPlot.CuttingUnit_CN;
                fsPlot.PlotNumber = curPlot.PlotNumber;
                //fsPlot.IsEmpty = curPlot.IsEmpty;
                fsPlot.Slope = curPlot.Slope;
                fsPlot.Aspect = curPlot.Aspect;
                fsPlot.Remarks = curPlot.Remarks;
                fsPlot.XCoordinate = curPlot.XCoordinate;
                fsPlot.YCoordinate = curPlot.YCoordinate;
                fsPlot.ZCoordinate = curPlot.ZCoordinate;
                fsPlot.MetaData = curPlot.MetaData;
                fsPlot.Blob = curPlot.Blob;
                //fsPlot.CreatedBy = curPlot.CreatedBy;
                //fsPlot.CreatedDate = curPlot.CreatedDate;

                fsPlot.Save();
            }
            //else
            //{
            // fsPlot = fsDAL.ReadSingleRow<PlotDO>("Plot", "Where CuttingUnit_CN = ? and Stratum_CN = ? and PlotNumber = ?", curPlot.CuttingUnit_CN, thisStrCN, curPlot.PlotNumber);
            //}
            return fsPlot.Plot_CN;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sMes = "Freq: Measured trees selected using frequency method (1:n).\n";
            sMes += "      Recon trees will be tagged Measure or Count based on frequency.\n\n";
            sMes += "BigBAF/FPS: Measured trees selected using BigBAF or small Fixed Plot Size.\n";
            sMes += "      All recon trees will be tagged as Count trees.\n";
            sMes += "      Plots will need to revisited so correct Measured trees can be determined.\n\n";
            sMes += "Meas/Cnt Plots: Some plots have all Measured trees with the rest having all Count trees.\n";
            sMes += "      All recon trees will be tagged as Measured trees.\n";

            DialogService.ShowMessage(sMes, "Information");
        }
    }
}