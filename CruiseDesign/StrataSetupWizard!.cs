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

namespace CruiseDesign
{

   public partial class StrataSetupWizard : Form
   {
      public bool reconExists;
      public string sUOM;
      #region Fields
      private UnitSetupPage unitPage = null;
      private StrataSetupPage strataPage = null;
        //private SelectSGset sgselectPage = null;
      private ViewStratum viewStratumPage = null;
      #endregion

      #region Constructor
      public StrataSetupWizard(CruiseDesignMain Main, string dalPathRecon, bool recExists)
      {
         this.Main = Main;
         this.cdDAL = Main.cdDAL;

         InitializeComponent();
         reconExists = recExists;
         if (reconExists)
         {
               //reconExists = true;
            try
            {
               this.rDAL = new DAL(dalPathRecon);
            }
            catch (System.IO.IOException e)
            {
               Logger.Log.E(e);
               MessageBox.Show("Error: Cannot open recon file");
            }
            catch (System.Exception e)
            {
               Logger.Log.E(e);
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
     public CruiseDesignMain Main { get; set; }  
     public List<TreeDefaultValueDO> myTreeDefaultList;
        
        // add the binding lists
     public DAL rDAL { get; set; }
     public DAL cdDAL { get; set; }

     public SaleDO mySale;
     public StratumDO currentStratum;
     public StratumDO newStratum;
     public CuttingUnitDO myCuttingUnit;
     public SampleGroupStatsDO currentSgStats;
     public StratumStatsDO currentStratumStats;

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
           cdCuttingUnits = new BindingList<CuttingUnitDO>(cdDAL.Read<CuttingUnitDO>("CuttingUnit", null, null));
           cdStratum = new BindingList<StratumDO>(cdDAL.Read<StratumDO>("Stratum", null, null));
           cdTreeDefaults = new BindingList<TreeDefaultValueDO>();
           cdSgStats = new BindingList<SampleGroupStatsDO>(cdDAL.Read<SampleGroupStatsDO>("SampleGroupStats", null, null));
           cdStratumStats = new BindingList<StratumStatsDO>(cdDAL.Read<StratumStatsDO>("StratumStats", null, null));
           //mySale = (cdDAL.ReadSingleRow<SaleDO>("Sale", null, null));

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
           mySale.DefaultUOM = sUOM;

           //currentStratumStats.Save();
           //setUsed(currentStratumStats.Stratum_CN);
         
           myTreeDefaultList = new List<TreeDefaultValueDO>();
           cdTreeDefaults.Clear();

           // save the current stratum
           currentStratumStats.Save();
           setUsed(currentStratumStats.Stratum_CN);
           
           cdSgStats = new BindingList<SampleGroupStatsDO>(cdDAL.Read<SampleGroupStatsDO>("SampleGroupStats", "Where StratumStats_CN = ? AND SgSet = ?", currentStratumStats.StratumStats_CN, currentStratumStats.SgSet));
           // setup Tree Default Values by CuttingUnits
           fillTreeDefaultList();
           //getTreeDefaultSql();

           strataPage.bindingSource1.DataSource = cdSgStats;
           
           strataPage.bindingSourceTDV.DataSource = cdTreeDefaults;
          
           strataPage.textBoxSGset.Text = currentStratumStats.SgSet.ToString();
           strataPage.textBoxSGsetDescr.Text = currentStratumStats.SgSetDescription;
           strataPage.textBoxStratum.Text = currentStratumStats.Code;
           pageHost1.Display(strataPage);
        }
       // view all stratum page
        public void setUsed(long? stratumCN)
        {
           // get stratumstats where stratumcn, sgSet = 1 and method = 100
           //StratumStatsDO thisStrStats = (cdDAL.Read<StratumStatsDO>("StratumStats", "WHERE Stratum_CN = ? AND Method = ?", stratumCN,"100"));
           List<StratumStatsDO> thisStrStats = new List<StratumStatsDO>(cdDAL.Read<StratumStatsDO>("StratumStats", "WHERE Stratum_CN = ? AND Method = ?", stratumCN, "100"));
           foreach (StratumStatsDO myStStats in thisStrStats)
           {
              myStStats.Used = 2;
              myStStats.Save();
           }
           currentStratum.Method = "100";
           currentStratum.Save();
        }

         public void GoToSgPage(int compCheck)
         {
           if (currentStratum == null ) return;
           //viewStratumPage.bindingSourceStratum.DataSource = cdStratum;
           //set binding list using currentStratum_cn
            if(compCheck == 0)
               cdStratumStats = new BindingList<StratumStatsDO>(cdDAL.Read<StratumStatsDO>("StratumStats", "WHERE Stratum_CN = ? AND Method = 100", currentStratum.Stratum_CN));
            else
               cdStratumStats = new BindingList<StratumStatsDO>(cdDAL.Read<StratumStatsDO>("StratumStats", "WHERE Stratum_CN = ?", currentStratum.Stratum_CN));
               
           //currentStratumStats = (cdDAL.ReadSingleRow<StratumStatsDO>("StratumStats", "WHERE Stratum_CN = ? AND SgSet = 1", currentStratum.Stratum_CN));
           if (currentStratumStats == null) return;

           //sgselectPage.bindingSourceStratumStats.DataSource = cdStratumStats;
           viewStratumPage.bindingSourceStratumStats.DataSource = cdStratumStats;
           //set sg binding list using stratumstats_cn
           cdSgStats = new BindingList<SampleGroupStatsDO>(cdDAL.Read<SampleGroupStatsDO>("SampleGroupStats", "Where StratumStats_CN = ? AND SgSet = ?", currentStratumStats.StratumStats_CN, currentStratumStats.SgSet));
           //set TDV binding list using sgstats_tdv link
           
           //sgselectPage.bindingSourcSampleGroup.DataSource = cdSgStats;
           //pageHost1.Display(sgselectPage);
           viewStratumPage.bindingSourcSampleGroup.DataSource = cdSgStats;
           viewStratumPage.bindingSourceStratum.Position = Convert.ToInt32(currentStratum.rowID-1);
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
                //if (cdDAL != null)
                //   cdDAL.Dispose();
                if (rDAL != null)
                   rDAL.Dispose();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
      public void Finish()
      {
         //if (cdDAL != null)
           // cdDAL.Dispose();
         if (rDAL != null)
            rDAL.Dispose();
         this.DialogResult = DialogResult.OK;
         this.Close();
      }
      #endregion

      #region Copy Tables

 
        private void checkSalePurpose()
        {
          
          mySale = new SaleDO(cdDAL.ReadSingleRow<SaleDO>("Sale", null, null));
//           mySale = cdDAL.ReadSingleRow<SaleDO>("Sale", null, null);
           sUOM = mySale.DefaultUOM;
           String remark = mySale.Remarks;
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
                 CuttingUnitDO cur = rDAL.ReadSingleRow<CuttingUnitDO>("CuttingUnit", "Where code = ?", cu.Code);
                 if (cur != null)
                 {
                    //CuttingUnitDO cur = rDAL.Read<CuttingUnitDO>("CuttingUnit", "Where code = ?", cu.Code);
                    List<TreeDO> treer = new List<TreeDO>(rDAL.Read<TreeDO>("Tree", "Where CuttingUnit_CN = ? GROUP BY TreeDefaultValue_CN", cur.CuttingUnit_CN));

                    //myTreeDefaultList = new List<TreeDefaultValueDO>(cdDAL.Read<TreeDefaultValueDO>("TreeDefaultValue", null, null));
                    foreach (TreeDO tree in treer)
                    {
                       // get the record from Design TDV where recon spec, prod, LD match.
                       List<TreeDefaultValueDO> checkTDV = new List<TreeDefaultValueDO>(cdDAL.Read<TreeDefaultValueDO>("TreeDefaultValue", "WHERE Species = ? AND PrimaryProduct = ? AND LiveDead = ?", tree.TreeDefaultValue.Species, tree.TreeDefaultValue.PrimaryProduct, tree.TreeDefaultValue.LiveDead));
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
              myTreeDefaultList = new List<TreeDefaultValueDO>(cdDAL.Read<TreeDefaultValueDO>("TreeDefaultValue", null, null));
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
              foreach (CuttingUnitDO cu in currentStratum.CuttingUnits)
              {
                 CuttingUnitDO cur = rDAL.ReadSingleRow<CuttingUnitDO>("CuttingUnit", "Where code = ?", cu.Code);
                 if (cur != null)
                 {
                    cur.Strata.Populate();
                    foreach (StratumDO strr in cur.Strata)
                    {
                       List<SampleGroupDO> sgr = rDAL.Read<SampleGroupDO>("SampleGroup", "Where Stratum_CN = ?", strr.Stratum_CN);
                       if (sgr != null)
                       {
                          foreach (SampleGroupDO mysgr in sgr)
                          {
                             mysgr.TreeDefaultValues.Populate();
                             foreach (TreeDefaultValueDO myTDVr in mysgr.TreeDefaultValues)
                                if (!myTreeDefaultList.Contains(myTDVr))
                                   myTreeDefaultList.Add(myTDVr);
                          }
                       }
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
              myTreeDefaultList = new List<TreeDefaultValueDO>(cdDAL.Read<TreeDefaultValueDO>("TreeDefaultValue", null, null));
           }
           foreach (TreeDefaultValueDO myTDV in myTreeDefaultList)
              cdTreeDefaults.Add(myTDV);

           return;

        }

    }
}
