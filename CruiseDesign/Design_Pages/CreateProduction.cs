using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CruiseDAL;
using CruiseDAL.DataObjects;


namespace CruiseDesign.Design_Pages
{
   public partial class CreateProduction : Form
   {
      public CreateProduction(DesignMain Owner)
      {
         this.Owner = Owner;

         cdDAL = Owner.cdDAL;
         reconPath = Owner.dalPathR;
         string saleNumber = Owner.mySale.SaleNumber;
         string saleName = Owner.mySale.Name;
         fileName = saleNumber + "_" + saleName + "_TS.cruise";
         InitializeComponent();
         
//         if (Owner.reconExists)
//         {
            // bindinglist -> join stratumstats and sgstats and select where methods and reconplots
            // bindingsource to data select grid, show str code, method, description, recon plots, recontrees.
         InitializeDatabaseTables();
         InitializeDataBindings();

         selectedItemsGridView1.SelectedItems = new List<StratumStatsDO>();
         //selectedItemsGridView1.SelectedItems = _df.selItems;
         

//         }
//         else
//         {
//            selectedItemsGridView1.Visible = false;
//            selectedItemsGridView1.Enabled = false;
//         }
      }
      
      string destPath, reconPath, fileName;
      public DAL cdDAL { get; set; }
      public DAL rDAL { get; set; }
      public DAL fsDAL { get; set; }
      public BindingList<StratumStatsDO> reconStratum { get; set; }
      public BindingList<TreeFieldSetupDO> treeFields { get; set; }
      public BindingList<LogFieldSetupDO> logFields { get; set; }
      public List<StratumStatsDO> myStratum { get; set; }
      public List<SampleGroupStatsDO> mySgStats { get; set; }
      public List<CruiseMethodsDO> myMeth { get; set; }
      public long thisStrCN;
      public bool reconData, setRecData, logData;
      struct dataFiles
      {
         public string rFile;
         public string pFile;
         public bool recData;
         public DAL cdDAL1 { get; set; }
         public string[] strCode;
      };
      dataFiles _df;

      private void InitializeDatabaseTables()
      {
         //get stratumstats used == 1 methods == (pnt, fix, pcm, fcm)
         reconStratum = new BindingList<StratumStatsDO>();
         myStratum = new List<StratumStatsDO>(cdDAL.Read<StratumStatsDO>("StratumStats", "Where StratumStats.Used = 1 Order By StratumStats.Code",null));
         // loop through stratumstats
         foreach (StratumStatsDO _stratum in myStratum)
         {
            // get sgstats where reconplots > 0
            if (_stratum.Method == "PNT" || _stratum.Method == "FIX" || _stratum.Method == "PCM" || _stratum.Method == "FCM")
            {
               mySgStats = cdDAL.Read<SampleGroupStatsDO>("SampleGroupStats", "Where StratumStats_CN = ? and SampleGroupStats.ReconPlots > 0", _stratum.StratumStats_CN);
               // if count > 0, add to reconStratum
               if (mySgStats.Count > 0)
                  reconStratum.Add(_stratum);
            }
         }
         //reconStratum = new BindingList<StratumStatsDO>(cdDAL.Read<StratumStatsDO>("StratumStats", "JOIN SampleGroupStats ON StratumStats.StratumStats_CN = SampleGroupStats.StratumStats_CN AND StratumStats.Used = 1 AND SampleGroupStats.ReconPlots > 0 Where StratumStats.Method = ? OR StratumStats.Method = ? OR StratumStats.Method = ? OR StratumStats.Method = ? ORDER BY StratumStats.Code", "PNT","FIX","PCM","FCM"));
         if (reconStratum.Count <= 0)
         {
            labelRecon.Text = "No Recon Data Found.";
            reconData = false;
         }
         else
            reconData = true;

         //cdStratumStats = new BindingList<StratumStatsDO>(cdDAL.Read<StratumStatsDO>("StratumStats", null, null));

      }

      private void InitializeDataBindings()
      {
         bindingSource1.DataSource = reconStratum;
      }

      private void buttonBrowse_Click(object sender, EventArgs e)
      {
         destPath = AskSavePath();

      }

      private void buttonCancel_Click(object sender, EventArgs e)
      {
         Close();
      }

      private void buttonCreate_Click(object sender, EventArgs e)
      {
         if (destPath == null)
         {
            MessageBox.Show("Please enter filename.", "Information");
            return;
         }

         _df.cdDAL1 = cdDAL;
         _df.rFile = reconPath;
         _df.pFile = destPath;
         _df.recData = reconData;

         _df.strCode = new string[selectedItemsGridView1.SelectedItems.Count];
         int i = 0;

         foreach (StratumStatsDO stRec in selectedItemsGridView1.SelectedItems)
         {
            _df.strCode[i] = stRec.Code;
            i++;
         }
         this.UseWaitCursor = true;
         pictureBox1.Visible = true;
         panel1.Enabled = false;
         selectedItemsGridView1.Enabled = false;
         buttonCancel.Enabled = false;
         buttonCreate.Enabled = false;

         // start backgroundworker
         this.backgroundWorker1.RunWorkerAsync(_df);
         // background worker
         
      }

      private void startProductionSave()
      {

         
      }

      protected String AskSavePath()
      {
         saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();

         saveFileDialog1.DefaultExt = "cruise";
         saveFileDialog1.Filter = "Cruise files(*.cruise)|*.cruise";
         saveFileDialog1.FileName = fileName;
         if (saveFileDialog1.ShowDialog() == DialogResult.OK)
         {
            if (saveFileDialog1.FileName == reconPath)
            {
               MessageBox.Show("Cannot overwrite the Recon file.\nPlease select a different file name.");
               return null;
            }
            textBoxFile.Text = saveFileDialog1.FileName;

            return saveFileDialog1.FileName;
         }
         return null;
      }

      private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
      {
         dataFiles holdDF;

         holdDF = (dataFiles)e.Argument;

         //getStats.getPopulationStats(holdDF.rFile, holdDF.cdFile, holdDF.reconExists, err);

         fsDAL = new DAL(holdDF.pFile, true);

         if (holdDF.recData)
            rDAL = new DAL(holdDF.rFile, false);

         copyTablesToFScruise(holdDF.cdDAL1);
        
         copyStratumToFScruise(holdDF.cdDAL1);
   
         copyCuttingUnitStrToFScruise(holdDF.cdDAL1);
         
         copyPopulations(holdDF.cdDAL1, rDAL, fsDAL, holdDF.recData, holdDF.strCode);

         copySaleTable(holdDF.cdDAL1);


      }

      private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         // end backgroundworker
         pictureBox1.Visible = false;
         panel1.Enabled = true;
         selectedItemsGridView1.Enabled = true;
         buttonCancel.Enabled = true;
         buttonCreate.Enabled = true;
         this.UseWaitCursor = false;

         string sMes = "Production Cruise File has been created.\n";
         sMes += "Open in Cruise Manager - Customize to create the Tally Setup forms before putting file on Data Recorder"; 

         MessageBox.Show(sMes);


         Close();

      }
      private void copySaleTable(DAL cDAL)
      {
         //mySale = new List<SaleDO>();
         //open sale table
         SaleDO sale = new SaleDO(cDAL.ReadSingleRow<SaleDO>("Sale", null, null));
         SaleDO fsSale = new SaleDO(fsDAL);
         
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
         
         fsSale.Save();
         
         logData = sale.LogGradingEnabled;

         myMeth = new List<CruiseMethodsDO>(fsDAL.Read<CruiseMethodsDO>("CruiseMethods",null,null));
         if (myMeth.Count() < 4)
         {
            myMeth.Clear();
            for (int i = 0; i < 12; i++)
            {
               CruiseMethodsDO fsMeth = new CruiseMethodsDO(fsDAL);
               addCruiseMethod(fsMeth, i);
               myMeth.Add(fsMeth);
            }
            fsDAL.Save(myMeth);
         }
      }
      
      private void addCruiseMethod(CruiseMethodsDO fsMeth, int cnt)
      {
         switch (cnt)
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
               fsMeth.FriendlyValue = "Fixed Biomass (not currently available)";
               break;
         }
      }
         
      private void copyTablesToFScruise(DAL cDAL)
      {
         // copy Sale table
         //fsDAL.DirectCopy(cDAL, CruiseDAL.Schema.SALE._NAME, null, OnConflictOption.Ignore);
         //copy CuttingUnit table
         fsDAL.DirectCopy(cDAL, CruiseDAL.Schema.CUTTINGUNIT._NAME, null, OnConflictOption.Fail);
         //copy TreeDefaultValues table
         fsDAL.DirectCopy(cDAL, CruiseDAL.Schema.TREEDEFAULTVALUE._NAME, null, OnConflictOption.Ignore);
         //copy logfieldsetupdefault table
         fsDAL.DirectCopy(cDAL, CruiseDAL.Schema.LOGFIELDSETUPDEFAULT._NAME, null, OnConflictOption.Ignore);
         //copy logfieldsetup
//         fsDAL.DirectCopy(cDAL, CruiseDAL.Schema.LOGFIELDSETUP._NAME, null, OnConflictOption.Ignore);
         //copy messagelog
         fsDAL.DirectCopy(cDAL, CruiseDAL.Schema.MESSAGELOG._NAME, null, OnConflictOption.Ignore);
         //copy reports
         fsDAL.DirectCopy(cDAL, "Reports", null, OnConflictOption.Ignore);
         //copy treefieldsetupdefault
         fsDAL.DirectCopy(cDAL, CruiseDAL.Schema.TREEFIELDSETUPDEFAULT._NAME, null, OnConflictOption.Ignore);
         //copy volumeequations
         fsDAL.DirectCopy(cDAL, CruiseDAL.Schema.VOLUMEEQUATION._NAME, null, OnConflictOption.Ignore);
         //copy treeauditvalue
         fsDAL.DirectCopy(cDAL, CruiseDAL.Schema.TREEAUDITVALUE._NAME, null, OnConflictOption.Ignore);
         //copy treedefaultvaluetreeauditvalue
         fsDAL.DirectCopy(cDAL, CruiseDAL.Schema.TREEDEFAULTVALUETREEAUDITVALUE._NAME, null, OnConflictOption.Ignore);
         //copy tally
         fsDAL.DirectCopy(cDAL, CruiseDAL.Schema.TALLY._NAME, null, OnConflictOption.Ignore);
         //copy Strata
         //fsDAL.DirectCopy(cDAL, CruiseDAL.Schema.STRATUM._NAME, null, OnConflictOption.Ignore);
         //copy cuttingUnitStratum
         //fsDAL.DirectCopy(cDAL, CruiseDAL.Schema.CUTTINGUNITSTRATUM._NAME, null, OnConflictOption.Ignore);
   
      }

      private void copyStratumToFScruise(DAL cdDAL)
      {
         //loop through design stratum table
         List<StratumDO> myStr = new List<StratumDO>(cdDAL.Read<StratumDO>("Stratum",null,null));
         foreach(StratumDO curStr in myStr)
         {
         // create new stratumDO
            StratumDO fsStr = new StratumDO(fsDAL);
         // copy stratum information
            fsStr.Code = curStr.Code;
            fsStr.Description = curStr.Description;
            fsStr.Method = curStr.Method;
            fsStr.BasalAreaFactor = curStr.BasalAreaFactor;
            fsStr.FixedPlotSize = curStr.FixedPlotSize;
            fsStr.KZ3PPNT = curStr.KZ3PPNT;
            fsStr.YieldComponent = curStr.YieldComponent;
            fsStr.Year = curStr.Year;
            fsStr.Month = curStr.Month;
            
            fsStr.Save();   
        
         }
      }
      
      private void copyCuttingUnitStrToFScruise(DAL cdDAL)
      {
         StratumDO cdStr;
         List<StratumDO> myStr = new List<StratumDO>(fsDAL.Read<StratumDO>("Stratum",null,null));
         foreach(StratumDO curStr in myStr)
         {
            cdStr = cdDAL.ReadSingleRow<StratumDO>("Stratum", "Where Code = ?", curStr.Code);
            cdStr.CuttingUnits.Populate();
            
            foreach (CuttingUnitDO myUnit in cdStr.CuttingUnits)
            {
               CuttingUnitStratumDO custr = new CuttingUnitStratumDO(fsDAL);
               custr.CuttingUnit_CN = myUnit.CuttingUnit_CN;
               custr.Stratum_CN = curStr.Stratum_CN;
               
               custr.Save();
            }
         }
      }

      private void copyPopulations(DAL cDAL, DAL rDAL, DAL fsDAL, bool reconData, string[] stRecCode)
      {
         StratumDO cdStr;
         TreeDefaultValueDO currentTDV = new TreeDefaultValueDO();
         List<StratumStatsDO> cdStratumStats = new List<StratumStatsDO>(cDAL.Read<StratumStatsDO>("StratumStats", "JOIN Stratum ON StratumStats.Stratum_CN = Stratum.Stratum_CN AND StratumStats.Method = Stratum.Method AND StratumStats.Used = 1 ORDER BY Stratum.Code", null));

         List<PlotDO> myPlots = new List<PlotDO>();
         List<TreeDO> myTree = new List<TreeDO>();
         List<LogDO> myLogs = new List<LogDO>();
         treeFields = new BindingList<TreeFieldSetupDO>();
         logFields = new BindingList<LogFieldSetupDO>();
         bool first;
         string method = "";
         Random rnd = new Random();
         int measHit;

         foreach (StratumStatsDO myStStats in cdStratumStats)
         {
            setRecData = false;
            first = true;

            // check if recon data is to be saved
            cdStr = fsDAL.ReadSingleRow<StratumDO>("Stratum", "Where Code = ?", myStStats.Code);
            string strCode = myStStats.Code;
            thisStrCN = (long)cdStr.Stratum_CN;
            if (reconData)
            {
             
               if (myStStats.Method == "PNT" || myStStats.Method == "PCM")
                  method = "PNT";
               else if (myStStats.Method == "FIX" || myStStats.Method == "FCM")
                  method = "FIX";
               
               foreach (string stRec in stRecCode)
               {
                  if (stRec == strCode)
                  {
                     setRecData = true;
                     myPlots = (rDAL.Read<PlotDO>("Plot", "JOIN Stratum ON Plot.Stratum_CN = Stratum.Stratum_CN WHERE Stratum.Method = ?", method));
                     if(myTree.Count == 0)
                     {
                        myTree = (rDAL.Read<TreeDO>("Tree", null, null));
                        myLogs = (rDAL.Read<LogDO>("Log", null, null));
                     }
                  }
               }
            }
               // get fsDAl stratum record
            List<SampleGroupStatsDO> mySgStats = new List<SampleGroupStatsDO>(cDAL.Read<SampleGroupStatsDO>("SampleGroupStats", "Where StratumStats_CN = ?", myStStats.StratumStats_CN));
           // loop through sample groups
            foreach (SampleGroupStatsDO sgStats in mySgStats)
            {
               measHit = 0;
               SampleGroupDO fsSg = new SampleGroupDO(fsDAL);
               // save sample group information
               fsSg.Stratum_CN = thisStrCN;
               fsSg.Code = sgStats.Code;
               fsSg.Description = sgStats.Description;
               fsSg.CutLeave = sgStats.CutLeave;
               fsSg.UOM = sgStats.UOM;
               fsSg.PrimaryProduct = sgStats.PrimaryProduct;
               fsSg.SecondaryProduct = sgStats.SecondaryProduct;
               fsSg.DefaultLiveDead = sgStats.DefaultLiveDead;
               fsSg.KZ = sgStats.KZ;
               fsSg.InsuranceFrequency = sgStats.InsuranceFrequency;
               if (myStStats.Method == "PCM" || myStStats.Method == "FCM")
               {
                  if (radioButton1.Checked)
                  {
                     fsSg.SamplingFrequency = sgStats.SamplingFrequency;
                     // find random start
                     int freq = (int)fsSg.SamplingFrequency;
                     measHit = rnd.Next(1, freq);
                     fsSg.BigBAF = 0;
                     fsSg.SmallFPS = 0;
                  }
                  else
                  {
                     fsSg.SamplingFrequency = 0;
                     fsSg.BigBAF = Convert.ToInt32(sgStats.BigBAF);
                     fsSg.SmallFPS = Convert.ToInt32(sgStats.BigFIX);
                  }
               }
               else
               {
                  fsSg.SamplingFrequency = sgStats.SamplingFrequency;
                  fsSg.BigBAF = 0;
                  fsSg.SmallFPS = 0;
               }
               // find treedefaultvalues
               sgStats.TreeDefaultValueStats.Populate();
               foreach (TreeDefaultValueDO tdv in sgStats.TreeDefaultValueStats)
               {
                  fsSg.TreeDefaultValues.Add(tdv);
               }
               fsSg.Save();
               fsSg.TreeDefaultValues.Save();
               // if recon can be saved
               if (setRecData)
               {
                  getReconData(myStStats, sgStats, rDAL, fsDAL, myPlots, myTree, myLogs, fsSg.SampleGroup_CN, first, measHit);
                 first = false;
               }
            }
            
            getTreeFieldSetup(cDAL,fsDAL,myStStats);
            if(logData)
               getLogFieldSetup(cDAL,fsDAL,myStStats);
         }            
      }
      public void getTreeFieldSetup(DAL cDAL, DAL fsDAL, StratumStatsDO myStStats)
      {
            //select from TreeFieldSetupDefault where method = stratum.method
         List<TreeFieldSetupDefaultDO> treeFieldDefaults = new List<TreeFieldSetupDefaultDO>(cDAL.Read < TreeFieldSetupDefaultDO >("TreeFieldSetupDefault", "WHERE Method = ? ORDER BY FieldOrder", myStStats.Method));
         foreach (TreeFieldSetupDefaultDO tfd in treeFieldDefaults)
         {
            TreeFieldSetupDO tfs = new TreeFieldSetupDO();
            tfs.Stratum_CN = thisStrCN;
            tfs.Field = tfd.Field;
            tfs.FieldOrder = tfd.FieldOrder;
            tfs.ColumnType = tfd.ColumnType;
            tfs.Heading = tfd.Heading;
            tfs.Width = tfd.Width;
            tfs.Format = tfd.Format;
            tfs.Behavior = tfd.Behavior;

            treeFields.Add(tfs);

         }
         fsDAL.Save(treeFields);
      }

      public void getLogFieldSetup(DAL cDAL, DAL fsDAL, StratumStatsDO myStStats)
      {
         List<LogFieldSetupDefaultDO> logFieldDefaults = new List<LogFieldSetupDefaultDO>(cDAL.Read<LogFieldSetupDefaultDO>("LogFieldSetupDefault", null, null));
         foreach (LogFieldSetupDefaultDO lfd in logFieldDefaults)
         {
            LogFieldSetupDO lfs = new LogFieldSetupDO();
            lfs.Stratum_CN = thisStrCN;
            lfs.Field = lfd.Field;
            lfs.FieldOrder = lfd.FieldOrder;
            lfs.ColumnType = lfd.ColumnType;
            lfs.Heading = lfd.Heading;
            lfs.Width = lfd.Width;
            lfs.Format = lfd.Format;
            lfs.Behavior = lfd.Behavior;

            logFields.Add(lfs);
         }
         fsDAL.Save(logFields);
      }

      public int getReconData(StratumStatsDO curStrStats, SampleGroupStatsDO curSgStats, DAL rDAL, DAL fsDAL, List<PlotDO> myPlots, List<TreeDO> myTree, List<LogDO> myLogs, long? sampleGroupCN, bool first, int measHit)
      {
         TreeDO fsTree;
         LogDO fsLog;
         long? plotCN = 0;
         var myTreeList = myTree;
         var myLogList = myLogs;
         int treeCnt = 1;

         curStrStats.Stratum.CuttingUnits.Populate();
         curSgStats.TreeDefaultValueStats.Populate();
         // loop through design units
         foreach (CuttingUnitDO curUnit in curStrStats.Stratum.CuttingUnits)
         {
            // get number of plots for stratum and cutting unit
            var myPlotList = (from plt in myPlots
                              where plt.CuttingUnit_CN == curUnit.CuttingUnit_CN
                              select plt).ToList();
            // loop through plots
            foreach (PlotDO curPlot in myPlotList)
            {
               // if first time for stratum, save plots
               plotCN = savePlots(curPlot, curStrStats.Stratum_CN, first);

               foreach (TreeDefaultValueDO curTdv in curSgStats.TreeDefaultValueStats)
               {
                  if (curSgStats.MinDbh > 0 && curSgStats.MaxDbh > 0)
                  {
                     double maxDbh = curSgStats.MaxDbh + 0.0499;
                     double minDbh = curSgStats.MinDbh - 0.0500;
                     myTreeList = (from tcv in myTree
                                       where tcv.SampleGroup.PrimaryProduct == curSgStats.PrimaryProduct
                                       && tcv.Plot_CN == curPlot.Plot_CN
                                       && tcv.Species == curTdv.Species
                                       && tcv.LiveDead == curTdv.LiveDead
                                       && tcv.DBH >= minDbh
                                       && tcv.DBH <= maxDbh
                                       select tcv).ToList();
                  }
                  else
                  {
                     myTreeList = (from tcv in myTree
                                       where tcv.SampleGroup.PrimaryProduct == curSgStats.PrimaryProduct
                                       && tcv.Plot_CN == curPlot.Plot_CN
                                       && tcv.Species == curTdv.Species
                                       && tcv.LiveDead == curTdv.LiveDead
                                       select tcv).ToList();
                  }
                  
                  foreach (TreeDO rTree in myTreeList)
                  {
                     fsTree = new TreeDO(fsDAL);
                     fsTree.Stratum_CN = thisStrCN;
                     fsTree.TreeDefaultValue_CN = curTdv.TreeDefaultValue_CN;
                     fsTree.SampleGroup_CN = sampleGroupCN;
                     fsTree.Plot_CN = plotCN;
                     fsTree.CuttingUnit_CN = curUnit.CuttingUnit_CN;
                     fsTree.TreeNumber = rTree.TreeNumber;
                     fsTree.Species = rTree.Species;
                     if (curStrStats.Method == "PCM" || curStrStats.Method == "FCM")
                     {
                        if (radioButton1.Checked)
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
                        else if (radioButton2.Checked)
                        {
                           fsTree.CountOrMeasure = "C";
                        }
                        else
                           fsTree.CountOrMeasure = "M";
                     }
                     else
                        fsTree.CountOrMeasure = rTree.CountOrMeasure;
                     treeCnt++;
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
                     //fsTree.CreatedBy = rTree.CreatedBy;
                     //fsTree.CreatedDate = rTree.CreatedDate;

                     fsTree.Save();
                     // save logs
                     myLogList = (from lcv in myLogs
                                  where lcv.Tree_CN == rTree.Tree_CN
                                  select lcv).ToList();
                     
                     foreach (LogDO rLog in myLogList)
                     {
                        fsLog = new LogDO(fsDAL);
                        fsLog.Tree_CN = rLog.Tree_CN;
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
      private long? savePlots(PlotDO curPlot, long? stratumCN, bool first)
      {
         PlotDO fsPlot;
         if (first)
         {
            fsPlot = new PlotDO(fsDAL);

            fsPlot.Stratum_CN = thisStrCN;
            fsPlot.CuttingUnit_CN = curPlot.CuttingUnit_CN;
            fsPlot.PlotNumber = curPlot.PlotNumber;
            fsPlot.IsEmpty = curPlot.IsEmpty;
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
         else
         {
            fsPlot = fsDAL.ReadSingleRow<PlotDO>("Plot", "Where CuttingUnit_CN = ? and Stratum_CN = ? and PlotNumber = ?", curPlot.CuttingUnit_CN, thisStrCN, curPlot.PlotNumber);
            
         }
         return (fsPlot.Plot_CN);
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

         MessageBox.Show(sMes, "Information");
                          
                             
      }

   }
}
