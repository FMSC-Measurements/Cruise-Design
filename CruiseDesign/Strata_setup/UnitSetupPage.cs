using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CruiseDAL;
using CruiseDAL.DataObjects;

namespace CruiseDesign.Strata_setup
{
    public partial class UnitSetupPage : UserControl
    {
        int unitRowCnt;
        
        public UnitSetupPage(StrataSetupWizard Owner)
        {
            this.Owner = Owner;
           
            InitializeComponent();
            //Owner.currentStratum = new StratumDO(Owner.cdDAL);
            //bindingSourceCurrentStratum.DataSourceChanged += new EventHandler(bindingSourceCurrentStratum_DataSourceChanged);
            // initialize data bindings
            InitializeDataBindings();
            // initialize combo box
            textBoxUOM.Text = Owner.sUOM;
        }


        #region Intialize

        private void InitializeDataBindings()
        {
           bindingSourceUnit.DataSource = Owner.cdCuttingUnits;
           //bindingSourceCurrentStratum.DataSource = Owner.currentStratum;
           bindingSourceStratum.DataSource = Owner.cdStratum;
        }

        public StrataSetupWizard Owner { get; set; }

        #endregion


        #region Button Click Events

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Owner.Cancel();
        }

        private void buttonStrata_Click(object sender, EventArgs e)
        {
           if(saveStratum(true) < 0)
              return;
           //list of selected units
           Owner.currentStratumStats = (Owner.cdDAL.ReadSingleRow<StratumStatsDO>("StratumStats", "WHERE Stratum_CN = ? AND SgSet = 1", Owner.currentStratum.Stratum_CN));

           if (Owner.currentStratumStats != null)
           {
              if (Owner.currentStratumStats.SgSetDescription == "Comparison Cruise")
              {
                 MessageBox.Show("Stratum Created From Comparison Cruise.\nCannot Edit Design.", "Warning", MessageBoxButtons.OK);
                 return;
              }
           }
           checkUOMfield();

           Owner.GoToStrataPage(1);
        }
       
       private void buttonAddStratum_Click(object sender, EventArgs e)
       {
           if (dataGridViewStratum.RowCount > 0)
           {
              if (saveStratum(true) < 0)
                 return;
           }

           Owner.newStratum = new StratumDO(Owner.cdDAL);
           Owner.newStratum.Method = "100";

           Owner.cdStratum.Add(Owner.newStratum);

           bindingSourceStratum.DataSource = Owner.cdStratum;
           buttonStrata.Enabled = true;
       }

        private void buttonViewStratum_Click(object sender, EventArgs e)
        {
           if (saveStratum(true) < 0)
              return;
           //list of selected units
           Owner.currentStratumStats = (Owner.cdDAL.ReadSingleRow<StratumStatsDO>("StratumStats", "WHERE Stratum_CN = ? AND SgSet = 1", Owner.currentStratum.Stratum_CN));

           checkUOMfield();

           // check for comparison cruise
           if (Owner.currentStratumStats.SgSetDescription == "Comparison Cruise")
              Owner.GoToSgPage(1);
           else
              Owner.GoToSgPage(0);
           //call SelectSGset Page

        }
      private void checkUOMfield()
      {
           
         if (textBoxUOM.Text.Length <= 0)
         {
              textBoxUOM.Text = "03";
              Owner.sUOM = "03";    
         }
         textBoxUOM.ReadOnly = true;
      
      
      }
        private void buttonDeleteStratum_Click(object sender, EventArgs e)
        {
           if (StratumDO.DeleteStratum(Owner.currentStratum.DAL, Owner.currentStratum) < 0)
           {
              MessageBox.Show("Stratum has data. Cannot delete.", "Warning", MessageBoxButtons.OK);
           }
           else
           {
              Cursor.Current = Cursors.WaitCursor;
              Owner.cdStratum.Remove(Owner.currentStratum);
              Cursor.Current = this.Cursor;
           }

        }

        private void buttonAddUnit_Click(object sender, EventArgs e)
        {
           CuttingUnitDO newUnit = new CuttingUnitDO(Owner.cdDAL);
           Owner.cdCuttingUnits.Add(newUnit);
           bindingSourceUnit.DataSource = Owner.cdCuttingUnits;
        }
       # endregion

        #region Context Menu
        private void useReconStrataToolStripMenuItem_Click(object sender, EventArgs e)
        {
           //check for existing strata
           if (checkStrata())
              addReconStratum(false);
           //add stratum
  
           buttonStrata.Enabled = true;
        }

        private void useReconStrataAndSampleGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           //check for existing strata
           if (checkStrata())
              addReconStratum(true);
           //add stratum

           buttonStrata.Enabled = true;

        }
        
        private bool checkStrata()
        {
           //check for existing strata
           if (!Owner.reconExists)
           {
              MessageBox.Show("No Recon File", "Warning", MessageBoxButtons.OK);
              return (false);
           }
           
           if (dataGridViewStratum.RowCount > 0)
           {
              MessageBox.Show("Stratum already exists.\nPlease delete all existing Strata first.","Warning",MessageBoxButtons.OK);
              return (false);
           }
           return (true);
        }

        private void addReconStratum(bool addSG)
        {
           // use historical data logic
           WaitForm waitFrm = new WaitForm();

           Cursor.Current = Cursors.WaitCursor;
           waitFrm.Show();

           // open recon file
           List<StratumDO> reconStratum = new List<StratumDO>(Owner.rDAL.Read<StratumDO>("Stratum", null, null));
           foreach (StratumDO myRSt in reconStratum)
           {
              // add stratum
              Owner.newStratum = new StratumDO(Owner.cdDAL);

              // copy stratum from recon
              Owner.newStratum.Code = myRSt.Code;
              Owner.newStratum.Method = "100";
              Owner.newStratum.Description = myRSt.Description;

              // set unit codes
              myRSt.CuttingUnits.Populate();
              float totalAcres = 0;
              foreach (CuttingUnitDO rcu in myRSt.CuttingUnits)
              {
                 Owner.myCuttingUnit = Owner.cdDAL.ReadSingleRow<CuttingUnitDO>("CuttingUnit", "WHERE Code = ?", rcu.Code);
                 Owner.newStratum.CuttingUnits.Add(Owner.myCuttingUnit);
                 float acres = Owner.myCuttingUnit.Area;
                 totalAcres += acres;
              }
              Owner.newStratum.Save();
              Owner.newStratum.CuttingUnits.Save();

              //check for tree vs plot and one vs two stage methods
              // copy stratumstats to design
              Owner.newStratumStats = new StratumStatsDO(Owner.cdDAL);
              //         Owner.currentStratumStats.Stratum = Owner.currentStratum;
              Owner.newStratumStats.Stratum_CN = Owner.newStratum.Stratum_CN;
              Owner.newStratumStats.Code = Owner.newStratum.Code;
              Owner.newStratumStats.Description = Owner.newStratum.Description;
              Owner.newStratumStats.Method = "100";
              Owner.newStratumStats.SgSetDescription = "";
              Owner.newStratumStats.SgSet = 1;
              Owner.newStratumStats.Used = 2;
              Owner.newStratumStats.TotalAcres = totalAcres;
              Owner.newStratumStats.Save();

              Owner.cdStratum.Add(Owner.newStratum);
              if (addSG)
              {
                 List<SampleGroupDO> reconSG = new List<SampleGroupDO>(Owner.rDAL.Read<SampleGroupDO>("SampleGroup", "WHERE Stratum_CN = ?", myRSt.Stratum_CN));

                 //           getSampleGroupStats();
                 foreach (SampleGroupDO myRsg in reconSG)
                 {
                    // create samplegroupstats
                    Owner.newSgStats = new SampleGroupStatsDO(Owner.cdDAL);
                    //set foriegn key
                    Owner.newSgStats.StratumStats = Owner.newStratumStats;
                    Owner.newSgStats.SgSet = 1;
                    Owner.newSgStats.Code = myRsg.Code;
                    Owner.newSgStats.CutLeave = myRsg.CutLeave;
                    Owner.newSgStats.UOM = myRsg.UOM;
                    Owner.newSgStats.PrimaryProduct = myRsg.PrimaryProduct;
                    Owner.newSgStats.SecondaryProduct = myRsg.SecondaryProduct;
                    Owner.newSgStats.DefaultLiveDead = myRsg.DefaultLiveDead;
                    Owner.newSgStats.Description = myRsg.Description;

                    Owner.newSgStats.Save();

                    // loop through TDV information

                    myRsg.TreeDefaultValues.Populate();
                    foreach (TreeDefaultValueDO tdv in myRsg.TreeDefaultValues)
                    {
                       // check with current TDV values
                       Owner.newTreeDefault = Owner.cdDAL.ReadSingleRow<TreeDefaultValueDO>("TreeDefaultValue", "WHERE Species = ? AND PrimaryProduct = ? AND LiveDead = ?", tdv.Species, tdv.PrimaryProduct, tdv.LiveDead);
                       // if exists, create link
                       Owner.newSgStats.TreeDefaultValueStats.Add(Owner.newTreeDefault);
                    }

                    Owner.newSgStats.Save();
                    Owner.newSgStats.TreeDefaultValueStats.Save();

                 }
              }
           }
           waitFrm.Close();
           Cursor.Current = this.Cursor;
        }


        #endregion

        private void UnitSetupPage_Load(object sender, EventArgs e)
       {
           //bindingSourceCurrentStratum.DataSource = Owner.currentStratum;
           if (dataGridViewStratum.RowCount > 0)
              buttonStrata.Enabled = true;
       }

        private void bindingSourceStratum_CurrentChanged(object sender, EventArgs e)
        {

           // display the Unit selected items
           Owner.currentStratum = bindingSourceStratum.Current as StratumDO;
           if (Owner.currentStratum != null)
           {
              Owner.currentStratum.CuttingUnits.Populate();
              selectedItemsGridViewUnits.SelectedItems = Owner.currentStratum.CuttingUnits;
           }
        }

        private int saveStratum(bool chkUnit)
        {
           // determine the units selected
           if (chkUnit)
           {
              unitRowCnt = selectedItemsGridViewUnits.SelectedItems.Count;
              if (unitRowCnt <= 0)
              {
                 MessageBox.Show("Need to select at least one unit", "Information");
                 return (-1);
              }
           }
 
           // determine if Stratum Code is entered
           if (Owner.currentStratum.Code.Length <= 0)
           {
              MessageBox.Show("Enter Stratum Code", "Information");
              return(-1);
           }
           // check for Historical Cruise Setup

           Owner.currentStratum.Save();
           
           foreach (CuttingUnitDO cu in Owner.cdCuttingUnits)
           {
              cu.Save();
           }

           Owner.currentStratum.CuttingUnits.Save();

           Owner.currentStratumStats = (Owner.cdDAL.ReadSingleRow<StratumStatsDO>("StratumStats", "WHERE Stratum_CN = ? AND SgSet = 1", Owner.currentStratum.Stratum_CN));

           if (Owner.currentStratumStats == null)
           {
              makeNewStratumStats();
           }

           return (0);
        }

        public void makeNewStratumStats()
        {
           Owner.currentStratumStats = new StratumStatsDO(Owner.cdDAL);
           //set foriegn keys
           Owner.currentStratumStats.Stratum_CN = Owner.currentStratum.Stratum_CN;
           Owner.currentStratumStats.Method = "100";
           Owner.currentStratumStats.Code = Owner.currentStratum.Code;
           Owner.currentStratumStats.Description = Owner.currentStratum.Description;
           Owner.currentStratumStats.SgSet = 1;
           Owner.currentStratumStats.SgSetDescription = "";
           Owner.currentStratumStats.Used = 2;
           float totalAcres = 0;
           foreach (CuttingUnitDO cu in Owner.currentStratum.CuttingUnits)
           {
              float acres = cu.Area;
              totalAcres += acres;
           }
           Owner.currentStratumStats.TotalAcres = totalAcres;

           Owner.currentStratumStats.Save();
           Owner.setUsed(Owner.currentStratumStats.Stratum_CN);

        }

        private void dataGridViewStratum_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
          // if (saveStratum(false) < 0)
          //    return;
        }


   }
}
