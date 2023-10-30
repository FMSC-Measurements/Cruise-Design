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
using CruiseDesign.Strata_setup;
using System.Collections;
using CruiseDesign.Services;

namespace CruiseDesign
{

    public partial class StrataSetupWizard : Form
    {
        public bool reconExists;
        public string sUOM;

        public CruiseDesignFileContext FileContext { get; }

        #region Fields
        private UnitSetupPage unitPage = null;
        private StrataSetupPage strataPage = null;
        //private SelectSGset sgselectPage = null;
        private ViewStratum viewStratumPage = null;
        #endregion

        #region Constructor
        protected StrataSetupWizard()
        {
            InitializeComponent();
        }

        public StrataSetupWizard(ICruiseDesignFileContextProvider fileContextProvider)
            : this()
        {
            var fileContext = FileContext = fileContextProvider.CurrentFileContext;

            this.cdDAL = fileContext.DesignDb;

            reconExists = fileContext.DoesReconExist;
            if (reconExists)
            {
                try
                {
                    rDAL = new DAL(fileContext.ReconFilePath);
                }
                catch (System.IO.IOException e)
                {
                    //FMSC.ORM.Core.Logger..Log.E(e);
                    MessageBox.Show("Error: Cannot open recon file");
                }
                catch (System.Exception e)
                {
                    //Logger.Log.E(e);
                    MessageBox.Show("Error: Cannot open recon file");
                }
            }
            else
            {
                MessageBox.Show("No Recon File Found.\nMake sure Recon file is in same Folder as the design file.\nDesign can still be created manually, but no Recon data will be used.", "Warning", MessageBoxButtons.OK);
            }

            //check for historical data

            InitializeDataBindings();

            checkSalePurpose();

            InitializePages();
        }

        #endregion

        #region Properties

        //public ArrayList selectedUnits = new ArrayList();
        public List<TreeDefaultValueDO> myTreeDefaultList;

        // add the binding lists
        public DAL rDAL { get; set; }
        public DAL cdDAL { get; set; }

        public SaleDO mySale;
        public StratumDO currentStratum;
        public StratumDO newStratum;
        public SampleGroupStatsDO currentSgStats;
        public SampleGroupStatsDO newSgStats;
        public StratumStatsDO currentStratumStats;
        public StratumStatsDO newStratumStats;
        public CuttingUnitDO myCuttingUnit;
        public TreeDefaultValueDO newTreeDefault;

        public BindingList<CuttingUnitDO> cdCuttingUnits { get; set; }
        public BindingList<StratumDO> cdStratum { get; set; }
        public BindingList<TreeDefaultValueDO> cdTreeDefaults { get; set; }
        public BindingList<SampleGroupStatsDO> cdSgStats { get; set; }
        public BindingList<StratumStatsDO> cdStratumStats { get; set; }

        #endregion

        #region Initialization
        private void InitializePages()
        {
            unitPage = new UnitSetupPage(this);
            pageHost1.Add(unitPage);

            strataPage = new StrataSetupPage(this);
            pageHost1.Add(strataPage);

            viewStratumPage = new ViewStratum(this);
            pageHost1.Add(viewStratumPage);

            this.DialogResult = DialogResult.Cancel;
        }

        private void InitializeDataBindings()
        {
            cdCuttingUnits = new BindingList<CuttingUnitDO>(cdDAL.From<CuttingUnitDO>().Read().ToList());
            cdStratum = new BindingList<StratumDO>(cdDAL.From<StratumDO>().Read().ToList());
            cdTreeDefaults = new BindingList<TreeDefaultValueDO>();
            cdSgStats = new BindingList<SampleGroupStatsDO>(cdDAL.From<SampleGroupStatsDO>().Read().ToList());
            cdStratumStats = new BindingList<StratumStatsDO>(cdDAL.From<StratumStatsDO>().Read().ToList());

        }


        #endregion

        #region Paging Methods
        public void GoToUnitPage()
        {
            //currentStratum = new StratumDO(cdDAL);

            pageHost1.Display(unitPage);

        }
        public void GoToStrataPage(int pageID)
        {

            // If from UnitPage - get new currentStratumStats
            //var mSale = cdDAL.QuerySingleRecord<SaleDO>("Select * from Sale");
            var mSale = cdDAL.Query<SaleDO>("Select * from Sale").First();
            //var mSale = cdDAL.ReadSingleRow<SaleDO>(0);

            mSale.DefaultUOM = sUOM;
            mSale.Save();

            //currentStratumStats.Save();
            //setUsed(currentStratumStats.Stratum_CN);

            myTreeDefaultList = new List<TreeDefaultValueDO>();
            cdTreeDefaults.Clear();

            // save the current stratum
            currentStratumStats.Save();
            setUsed(currentStratumStats.Stratum_CN);

            cdSgStats = new BindingList<SampleGroupStatsDO>(cdDAL.From<SampleGroupStatsDO>().Where("StratumStats_CN = @p1 AND SgSet = @p2")
                                                           .Read(currentStratumStats.StratumStats_CN, currentStratumStats.SgSet).ToList());
            // setup Tree Default Values by CuttingUnits
            fillTreeDefaultList();
            //getTreeDefaultSql();

            strataPage.bindingSourceSgStats.DataSource = cdSgStats;

            strataPage.bindingSourceTDV.DataSource = cdTreeDefaults;

            strataPage.textBoxSGset.Text = currentStratumStats.SgSet.ToString();
            strataPage.textBoxSGsetDescr.Text = currentStratumStats.SgSetDescription;
            strataPage.textBoxStratum.Text = currentStratumStats.Code;
            // check for no sample groups
            if (cdSgStats.Count <= 0)
            {
                // add a blank sample group
                currentSgStats = new SampleGroupStatsDO(cdDAL);

                currentSgStats.SgSet = 1; ;
                currentSgStats.Code = " ";
                currentSgStats.PrimaryProduct = "01";
                currentSgStats.SecondaryProduct = "02";
                currentSgStats.DefaultLiveDead = "L";
                currentSgStats.CutLeave = "C";
                currentSgStats.UOM = sUOM;

                currentSgStats.StratumStats = currentStratumStats;

                cdSgStats.Add(currentSgStats);
                strataPage.product = "01";
                strataPage.setTreeDefaultValue("01");
            }
            else
            {
                // find product for first sample group
                strataPage.product = cdSgStats[0].PrimaryProduct;
                strataPage.setTreeDefaultValue(strataPage.product);

            }

            pageHost1.Display(strataPage);
        }
        // view all stratum page
        public void setUsed(long? stratumCN)
        {
            // get stratumstats where stratumcn, sgSet = 1 and method = 100
            List<StratumStatsDO> thisStrStats = cdDAL.From<StratumStatsDO>().Where("Stratum_CN = @p1 AND Method = @p2").Read(stratumCN, "100").ToList();
            foreach (StratumStatsDO myStStats in thisStrStats)
            {
                myStStats.Used = 2;
                myStStats.Save();
            }
            if (currentStratum.Method != "FIXCNT")
                currentStratum.Method = "100";
            currentStratum.Save();

        }

        public void GoToSgPage(int compCheck)
        {
            if (currentStratum == null) return;
            //viewStratumPage.bindingSourceStratum.DataSource = cdStratum;
            //set binding list using currentStratum_cn
            if (compCheck == 0)
                cdStratumStats = new BindingList<StratumStatsDO>(cdDAL.From<StratumStatsDO>().Where("Stratum_CN = @p1 AND Method = 100").Read(currentStratum.Stratum_CN).ToList());
            else
                cdStratumStats = new BindingList<StratumStatsDO>(cdDAL.From<StratumStatsDO>().Where("Stratum_CN = @p1").Read(currentStratum.Stratum_CN).ToList());

            //currentStratumStats = (cdDAL.ReadSingleRow<StratumStatsDO>("StratumStats", "WHERE Stratum_CN = ? AND SgSet = 1", currentStratum.Stratum_CN));
            if (currentStratumStats == null) return;

            //sgselectPage.bindingSourceStratumStats.DataSource = cdStratumStats;
            viewStratumPage.bindingSourceStratumStats.DataSource = cdStratumStats;
            //set sg binding list using stratumstats_cn
            cdSgStats = new BindingList<SampleGroupStatsDO>(cdDAL.From<SampleGroupStatsDO>().Where("StratumStats_CN = @p1 AND SgSet = @p2")
                                                           .Read(currentStratumStats.StratumStats_CN, currentStratumStats.SgSet).ToList());
            //set TDV binding list using sgstats_tdv link

            //sgselectPage.bindingSourcSampleGroup.DataSource = cdSgStats;
            //pageHost1.Display(sgselectPage);
            viewStratumPage.bindingSourcSampleGroup.DataSource = cdSgStats;
            viewStratumPage.bindingSourceStratum.Position = Convert.ToInt32(currentStratum.rowID - 1);
            pageHost1.Display(viewStratumPage);
        }


        #endregion

        #region Click Events

        //cancels the cruise wizard diolog and discards all resorurces
        public void Cancel()
        {
            if (MessageBox.Show("Are you sure you want to cancel? Entered information will not be saved", "Warning", MessageBoxButtons.YesNo)
                == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
        public void Finish()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            rDAL?.Dispose();
            rDAL = null;
        }
        #endregion

        #region Copy Tables


        private void checkSalePurpose()
        {

            //mySale = new SaleDO(cdDAL.ReadSingleRow<SaleDO>("Sale", null, null));
            var mSale = cdDAL.Query<SaleDO>("Select * from Sale").First();
            //           mySale = cdDAL.ReadSingleRow<SaleDO>("Sale", null, null);

            sUOM = mSale.DefaultUOM;
            String remark = mSale.Remarks;
            if (remark == "Historical Design")
                reconExists = false;

        }

        #endregion

        private void getTreeDefaultSql()
        {
            //get list of selected cutting units (cu is design file, cur is recon file)
            currentStratum.CuttingUnits.Populate();
            if (reconExists)
            {
                foreach (CuttingUnitDO cu in currentStratum.CuttingUnits)
                {
                    CuttingUnitDO cur = rDAL.From<CuttingUnitDO>().Where("code = @p1").Read(cu.Code).FirstOrDefault();
                    if (cur != null)
                    {
                        List<TreeDO> treer = rDAL.From<TreeDO>().Where("CuttingUnit_CN = @p1")
                                               .GroupBy("TreeDefaultValue_CN").Read(cur.CuttingUnit_CN).ToList();

                        //myTreeDefaultList = new List<TreeDefaultValueDO>(cdDAL.Read<TreeDefaultValueDO>("TreeDefaultValue", null, null));
                        foreach (TreeDO tree in treer)
                        {
                            // get the record from Design TDV where recon spec, prod, LD match.
                            List<TreeDefaultValueDO> checkTDV = cdDAL.From<TreeDefaultValueDO>()
                                     .Where("Species = @p1 AND PrimaryProduct = @p2 AND LiveDead = @p3")
                                     .Read(tree.TreeDefaultValue.Species, tree.TreeDefaultValue.PrimaryProduct, tree.TreeDefaultValue.LiveDead).ToList();
                            foreach (TreeDefaultValueDO myTDV in checkTDV)
                                if (!myTreeDefaultList.Contains(myTDV))
                                    myTreeDefaultList.Add(myTDV);
                        }
                    }
                }
                // check the selected list in cdSgStats for any additional values to add
                if (cdSgStats.Count > 0)
                {
                    foreach (SampleGroupStatsDO mySgStats in cdSgStats)
                    {
                        mySgStats.TreeDefaultValueStats.Populate();
                        foreach (TreeDefaultValueDO myTDV in mySgStats.TreeDefaultValueStats)
                        {
                            if (!myTreeDefaultList.Contains(myTDV))
                                myTreeDefaultList.Add(myTDV);
                        }
                    }
                }
            }
            else
            {
                // add all tree default values
                myTreeDefaultList = cdDAL.From<TreeDefaultValueDO>().Read().ToList();
            }
            foreach (TreeDefaultValueDO myTDV in myTreeDefaultList)
                cdTreeDefaults.Add(myTDV);

            return;

        }
        private void fillTreeDefaultList()
        {
            //getTreeDefaultSql();
            //get list of selected cutting units (cu is design file, cur is recon file)
            currentStratum.CuttingUnits.Populate();
            if (reconExists)
            {
                // check the selected list in cdSgStats for any additional values to add
                if (cdSgStats.Count > 0)
                {
                    foreach (SampleGroupStatsDO mySgStats in cdSgStats)
                    {
                        mySgStats.TreeDefaultValueStats.Populate();
                        foreach (TreeDefaultValueDO myTDV in mySgStats.TreeDefaultValueStats)
                        {
                            if (!myTreeDefaultList.Contains(myTDV))
                                myTreeDefaultList.Add(myTDV);
                        }
                    }
                }
                foreach (CuttingUnitDO cu in currentStratum.CuttingUnits)
                {
                    CuttingUnitDO cur = rDAL.From<CuttingUnitDO>().Where("code = @p1").Read(cu.Code).FirstOrDefault();
                    if (cur != null)
                    {
                        cur.Strata.Populate();
                        foreach (StratumDO strr in cur.Strata)
                        {
                            List<SampleGroupDO> sgr = rDAL.From<SampleGroupDO>().Where("Stratum_CN = @p1").Read(strr.Stratum_CN).ToList();
                            if (sgr != null)
                            {
                                foreach (SampleGroupDO mysgr in sgr)
                                {
                                    mysgr.TreeDefaultValues.Populate();
                                    foreach (TreeDefaultValueDO myTDVr in mysgr.TreeDefaultValues)
                                    {
                                        TreeDefaultValueDO checkTDV = cdDAL.From<TreeDefaultValueDO>()
                                                    .Where("TreeDefaultValue_CN = @p1")
                                                    .Read(myTDVr.TreeDefaultValue_CN).FirstOrDefault();
                                        //if (!myTreeDefaultList.Contains(checkTDV))
                                        int cnt = 0;
                                        foreach (TreeDefaultValueDO check in myTreeDefaultList)
                                        {
                                            if (checkTDV.TreeDefaultValue_CN == check.TreeDefaultValue_CN)
                                                cnt++;
                                        }
                                        if (cnt == 0 || myTreeDefaultList.Count() == 0)
                                            myTreeDefaultList.Add(checkTDV);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                // add all tree default values
                myTreeDefaultList = cdDAL.From<TreeDefaultValueDO>().Read().ToList();
            }
            foreach (TreeDefaultValueDO myTDV in myTreeDefaultList)
                cdTreeDefaults.Add(myTDV);

            return;

        }

    }
}
