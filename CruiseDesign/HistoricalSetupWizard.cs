using CruiseDAL;
using CruiseDAL.DataObjects;
using CruiseDesign.Historical_setup;
using CruiseDesign.Services;
using FMSC.ORM.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CruiseDesign
{
    public partial class HistoricalSetupWizard : Form
    {
        public ILogger Logger { get; }

        #region Fields

        private UnitSetupPageHS unitPage = null;
        private HistoricalSetupPage histPage = null;
        //private SelectSGset sgselectPage = null;

        #endregion Fields

        #region Constructor

        protected HistoricalSetupWizard()
        {
            InitializeComponent();
        }

        public HistoricalSetupWizard(ICruiseDesignFileContextProvider fileContextProvider, ILogger<HistoricalSetupWizard> logger, IDialogService dialogService)
            : this()
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            var fileContext = fileContextProvider.CurrentFileContext;

            DialogService = dialogService;

            cdDAL = fileContext.DesignDb;
            setSalePurpose();

            InitializeDataBindings();
            InitializePages();
        }

        #endregion Constructor

        #region Properties

        public ArrayList selectedUnits = new ArrayList();
        public String UOM;

        public IDialogService DialogService { get; }

        // add the binding lists
        public DAL cdDAL { get; set; }

        public DAL hDAL { get; set; }

        public SaleDO Sale { get; set; }

        // current design lists
        public StratumDO currentStratum;

        public SampleGroupStatsDO currentSgStats;
        public StratumStatsDO currentStratumStats;
        public TreeDefaultValueDO currentTreeDefaults;

        public BindingList<CuttingUnitDO> cdCuttingUnits { get; set; }
        public BindingList<StratumDO> cdStratum { get; set; }

        public List<TreeDefaultValueDO> cdTreeDefaults { get; set; }

        // selected historical cruise lists
        public StratumDO selectedStratum;

        public SampleGroupDO selectedSampleGroup;
        public POPDO selectedPOP;
        public List<LCDDO> selectedLCD { get; set; }

        public BindingList<StratumDO> histStratum { get; set; }
        public BindingList<TreeDefaultValueDO> histTreeDefaults { get; set; }
        public BindingList<SampleGroupDO> histSampleGroup { get; set; }

        #endregion Properties

        #region Initialization Methods

        private void InitializePages()
        {
            unitPage = new UnitSetupPageHS(this);
            pageHost2.Add(unitPage);

            histPage = new HistoricalSetupPage(this);
            pageHost2.Add(histPage);

            //sgselectPage = new SelectSGset(this);
            //pageHost2.Add(sgselectPage);

            this.DialogResult = DialogResult.Cancel;
        }

        #endregion Initialization Methods

        public void OpenHistoricalCruiseFile(string path)
        {
            if(!CruiseDesignFileContext.EnsurePathValid(path, Logger, DialogService)
                && !CruiseDesignFileContext.EnsurePathExistsAndCanWrite(path, Logger, DialogService)) return;

            var fileExtention = Path.GetExtension(path).Trim();
            if(fileExtention == ".crz3")
            {
                var processFilePath = CruiseDesignFileContext.GetProcessFilePathFromV3Cruise(path);

                if(!CruiseDesignFileContext.EnsurePathValid(processFilePath, Logger, DialogService)) { return; }
                if (!File.Exists(processFilePath))
                {
                    var message = ".process File Does Not Exist for V3 Cruise.\r\nPlease process file first.";
                    Logger.LogWarning(message);
                    DialogService.ShowMessage(message, "Warning");
                    return;
                }

                if (File.GetAttributes(processFilePath).HasFlag(FileAttributes.ReadOnly))
                {
                    var message = ".process File Is Read Only.\r\nIf opening file from non-local location, please copy file to a location on your PC before opening.";
                    Logger.LogWarning(message);
                    DialogService.ShowMessage(message, "Warning");
                    return;
                }

                path = processFilePath;
            }

            //open new cruise DAL
            try
            {
                hDAL = new DAL(path);
            }
            catch (System.IO.IOException ie)
            {
                Logger.LogError(ie, "");
            }
            catch (System.Exception ie)
            {
                Logger.LogError(ie, "");
            }

            Sale = new SaleDO(hDAL.From<SaleDO>().Read().FirstOrDefault());
            string sUOM = Sale.DefaultUOM;
            if (sUOM != UOM)
            {
                MessageBox.Show("Cruise does not have same UOM.\nCannot import data.", "Warning");
                return;
            }

            //set binding list for stratum
            //histStratum = new BindingList<StratumDO>(hDAL.Read<StratumDO>("Stratum", null, null));
            histStratum = new BindingList<StratumDO>(hDAL.From<StratumDO>().Read().ToList());
            histPage.bindingSourceStratum.DataSource = histStratum;

            //set title bar with file name
            histPage.textBoxFile.Text = openFileDialog1.SafeFileName;
        }

        #region Paging Methods

        public void GoToUnitPage()
        {
            pageHost2.Display(unitPage);
        }

        public void GoToHistPage()
        {
            // If from UnitPage - get new currentStratumStats
            currentStratumStats = (cdDAL.From<StratumStatsDO>().Where("Stratum_CN = @p1 AND SgSet = 1")
                   .Read(currentStratum.Stratum_CN).FirstOrDefault());

            if (currentStratumStats != null)
            {
                // data already exists for stratum, delete stratum and continue?
                MessageBox.Show("Stratum data already exists for this stratum", "Information");
                return;
            }

            currentStratumStats = new StratumStatsDO(cdDAL);
            currentStratumStats.Stratum = currentStratum;
            currentStratumStats.Code = currentStratum.Code;
            currentStratumStats.Description = currentStratum.Description;
            currentStratumStats.SgSet = 1;
            currentStratumStats.SgSetDescription = "";

            float totalAcres = 0;
            foreach (CuttingUnitDO cu in currentStratum.CuttingUnits)
            {
                float acres = cu.Area;
                totalAcres += acres;
            }
            currentStratumStats.TotalAcres = totalAcres;
            //         Owner.currentStratumStats.Save();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OpenHistoricalCruiseFile(openFileDialog1.FileName);

                pageHost2.Display(histPage);
            }
        }

        public void GoToSgPage()
        {
            //set binding list using currentStratum_cn
            //cdStratumStats = new BindingList<StratumStatsDO>(cdDAL.Read<StratumStatsDO>("StratumStats", "WHERE Stratum_CN = ?", currentStratum.Stratum_CN));
            //if (cdStratumStats == null)
            //{
            //   return;
            //}
            //sgselectPage.bindingSourceStratumStats.DataSource = cdStratumStats;
            //set sg binding list using stratumstats_cn
            //cdSampleGroup = new BindingList<SampleGroupStatsDO>(cdDAL.Read<SampleGroupStatsDO>("SampleGroupStats", "Where StratumStats_CN = ? AND SgSet = ?", currentStratumStats.StratumStats_CN, currentStratumStats.SgSet));
            //set TDV binding list using sgstats_tdv link
            //sgselectPage.bindingSourcSampleGroup.DataSource = cdSgStats;
            //pageHost2.Display(sgselectPage);
        }

        #endregion Paging Methods

        #region Other Methods

        //cancels the cruise wizard diolog and discards all resorurces

        private void setSalePurpose()
        {
            SaleDO sale = new SaleDO(cdDAL.From<SaleDO>().Read().FirstOrDefault());

            if (sale.DefaultUOM == null)
                UOM = "03";
            else
                UOM = sale.DefaultUOM.ToString();
        }

        public void Cancel()
        {
            //if (MessageBox.Show("Are you sure you want to cancel? Entered information will not be saved", "Warning", MessageBoxButtons.YesNo)
            //    == DialogResult.Yes)
            //{
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public void Finish()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            hDAL?.Dispose();
            hDAL = null;
        }

        private void InitializeDataBindings()
        {
            // cdCuttingUnits = new BindingList<CuttingUnitDO>(cdDAL.Read<CuttingUnitDO>("CuttingUnit", null, null));
            cdCuttingUnits = new BindingList<CuttingUnitDO>(cdDAL.From<CuttingUnitDO>().Read().ToList());
            //cdStratum = new BindingList<StratumDO>(cdDAL.Read<StratumDO>("Stratum", null, null));
            cdStratum = new BindingList<StratumDO>(cdDAL.From<StratumDO>().Read().ToList());
            //cdTreeDefaults = new List<TreeDefaultValueDO>(cdDAL.Read<TreeDefaultValueDO>("TreeDefaultValue", null, null));
            cdTreeDefaults = new List<TreeDefaultValueDO>(cdDAL.From<TreeDefaultValueDO>().Read().ToList());
        }

        private void createHistoricalData()
        {
            // copy stratum row to design
            // copy stratumstats to design
            // copy sample groups row to design
            // create samplegroupstats
            // for each samplegroup
            // get POP data
            // calculate statistics (based on method)
            // get LCD data
            // calculate trees/acre and volume/acre by sample group
            // save samplegroupstats row
            // loop through TDV information
            // check with current TDV values
            // create link
            // save everything
        }

        #endregion Other Methods
    }
}