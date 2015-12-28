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
   public partial class StrataSetupPage : UserControl
   {
        //bool useDiaClass = false;

      public StrataSetupPage(StrataSetupWizard Owner)
      {
         this.Owner = Owner;
         InitializeComponent();

         textBoxSGsetDescr.Text = "";
         textBoxSGset.Text = "1";
         
         // initialize data bindings
         InitializeDataBindings();

         // get the unique set of product numbers
         setProductBox();
      }

      String columnName = "not set";
      string product;
      public StrataSetupWizard Owner { get; set; }
      public BindingList<TreeDefaultValueDO> newTDV { get; set; }
      public List<TreeDefaultValueDO> ppList { get; set; }

      #region Intialize

        private void InitializeDataBindings()
        {
          // bindingSourceCurrentStratumStats.DataSource = Owner.currentStratumStats;
           bindingSourceTDV.DataSource = Owner.cdTreeDefaults;
           bindingSourceSgStats.DataSource = Owner.cdSgStats;
           bindingSourceCurrentSgStats.DataSource = Owner.currentSgStats;
        }


        private void StrataSetupPage_Load(object sender, EventArgs e)
        {
           //textBoxSGsetDescr.Text = "";
           //textBoxSGset.Text = "1";
           //Owner.currentStratumStats.Save();

           //bindingSourceCurrentStratumStats.DataSource = Owner.currentStratumStats;
           //bindingSourceTDV.DataSource = Owner.cdTreeDefaults;
           //bindingSourceSgStats.DataSource = Owner.cdSgStats;
        }

        private void bindingSourceSgStats_CurrentChanged(object sender, EventArgs e)
        {
           // display the TDV selected items
           Owner.currentSgStats = bindingSourceSgStats.Current as SampleGroupStatsDO;
           if (Owner.currentSgStats != null)
           {
              selectedItemsGridViewTDV.SelectedItems = Owner.currentSgStats.TreeDefaultValueStats;
           }
           else
           {
              // make sure TDV grid has no selected columns
              selectedItemsGridViewTDV.ClearSelection();
           }
        }

        private void setProductBox()
        {
          ppList = new List<TreeDefaultValueDO>(Owner.cdDAL.Read<TreeDefaultValueDO>("TreeDefaultValue","Group By PrimaryProduct",null));
          bindingSourcePPlist.DataSource = ppList;
        } 
      #endregion


        #region Button Click Events
              //***Go back to Unit Page
        private void buttonUnits_Click(object sender, EventArgs e)
        {
           //check for samplegroups
           if (dataGridViewSG.RowCount > 0)
           {
              // save current
              saveStratumStats();
              int nRes = saveSampleGroupStats();
              if (nRes > 0) return;
           }
           else
           {
              MessageBox.Show("Need to add at least one Sample Group", "Information");
              return;
           }
             
           Owner.GoToUnitPage();
        }

        //***Go to Sample Group Page
        private void buttonViewStr_Click(object sender, EventArgs e)
        {
           if (Owner.currentSgStats != null)
           {
              int nRes = saveSampleGroupStats();
              if (nRes > 0) return;
              Owner.currentStratumStats.SgSetDescription = textBoxSGsetDescr.Text;
              saveStratumStats();
           }
           Owner.GoToSgPage(0);
        }

        //***Finish
        private void buttonFinish_Click(object sender, EventArgs e)
        {
            // save current
           if (Owner.currentSgStats != null)
           {
              int nRes = saveSampleGroupStats();
              if (nRes > 0) return;
              Owner.currentStratumStats.SgSetDescription = textBoxSGsetDescr.Text;
              saveStratumStats();
           }
            Owner.Finish();
        }
        


        //***Create New Strata and Return to Unit Page
        private void buttonNewStr_Click(object sender, EventArgs e)
        {
           if (Owner.currentSgStats != null)
           {
              int nRes = saveSampleGroupStats();
              if (nRes > 0) return;
           }
            Owner.GoToUnitPage();
        
        }

        private void buttonAddSp_Click(object sender, EventArgs e)
        {

        }

        private void buttonNewSet_Click(object sender, EventArgs e)
        {
            // save current sample group
           if (dataGridViewSG.RowCount > 0)
           {
              // save current
              int nRes = saveSampleGroupStats();
              if (nRes > 0) return;
           }
           else
           {
              MessageBox.Show("Need to add at least one Sample Group", "Information");
              return;
           }
          

           long nSgSet = getLastSgSet(Owner.currentStratumStats.Stratum_CN);

           float totalAcres = Owner.currentStratumStats.TotalAcres;
           // call saveStratumStats
           Owner.currentStratumStats.SgSetDescription = textBoxSGsetDescr.Text;
           saveStratumStats();
           // create new instance of StratumStats
           Owner.currentStratumStats = new StratumStatsDO(Owner.cdDAL);

           // find last SgSet code for stratumstats
           Owner.currentStratumStats.SgSet = nSgSet + 1;
           Owner.currentStratumStats.TotalAcres = totalAcres;
            
           saveStratumStats();
 
           //bindingSourceCurrentStratumStats.DataSource = Owner.currentStratumStats;
           //clear out currentSgStats
           Owner.cdSgStats = new BindingList<SampleGroupStatsDO>(Owner.cdDAL.Read<SampleGroupStatsDO>("SampleGroupStats", "Where StratumStats_CN = ? AND SgSet = ?", Owner.currentStratumStats.StratumStats_CN, nSgSet+1));
           bindingSourceSgStats.DataSource = Owner.cdSgStats;


           textBoxSGsetDescr.Text = "";
           textBoxSGset.Text = (nSgSet + 1).ToString();
           //clear cdSgStats (select by stratum and SgSet)
        }

        #endregion

        private long getLastSgSet(long? stratumCN)
        {
           List<StratumStatsDO> myStratumStats = new List<StratumStatsDO>(Owner.cdDAL.Read<StratumStatsDO>("StratumStats", "WHERE Stratum_CN = ? and Method = ? ORDER BY SgSet",stratumCN,"100"));
           int cnt = myStratumStats.Count;
           long nSgSet = Convert.ToInt32(myStratumStats[cnt - 1].SgSet);
           return (nSgSet);
        }

        private void buttonAddNew_Click(object sender, EventArgs e)
        {
           // if first row
           if (dataGridViewSG.RowCount <= 0)
           {
              
              // Owner.currentStratumStats.Save();
              Owner.currentSgStats = new SampleGroupStatsDO(Owner.cdDAL);

              Owner.currentSgStats.SgSet = Convert.ToInt32(textBoxSGset.Text);
              Owner.currentSgStats.Code = " ";
              Owner.currentSgStats.PrimaryProduct = "01";
              Owner.currentSgStats.SecondaryProduct = "02";
              Owner.currentSgStats.DefaultLiveDead = "L";
              Owner.currentSgStats.CutLeave = "C";
              Owner.currentSgStats.UOM = Owner.sUOM;
              
              Owner.currentSgStats.StratumStats = Owner.currentStratumStats;

              Owner.cdSgStats.Add(Owner.currentSgStats);
              product = "01";
                         
           }
           else
           {
               // save current row
              //saveStratumStats();
              product = Owner.currentSgStats.PrimaryProduct.ToString();
              //int nRes = saveSampleGroupStats();
              //if (nRes > 0) return;
               // get current row data

              Owner.currentSgStats = new SampleGroupStatsDO(Owner.cdDAL);

              //display last row information
              Owner.currentSgStats.SgSet = Convert.ToInt32(textBoxSGset.Text);
              Owner.currentSgStats.Code = " ";
              Owner.currentSgStats.PrimaryProduct = product;
              Owner.currentSgStats.SecondaryProduct = "02";
              Owner.currentSgStats.DefaultLiveDead = "L";
              Owner.currentSgStats.CutLeave = "C";
              Owner.currentSgStats.UOM = Owner.sUOM;
              //
              Owner.currentSgStats.StratumStats = Owner.currentStratumStats;

              Owner.cdSgStats.Add(Owner.currentSgStats);
          }

           //bindingSourceCurrentSgStats.DataSource = Owner.currentSgStats;

           bindingSourceSgStats.DataSource = Owner.cdSgStats;
           
        }

        #region Save Events

        private int saveSampleGroupStats()
        {
            
           // Loop through binding list
           foreach (SampleGroupStatsDO curSgStats in bindingSourceSgStats)
           {
              // display the TDV selected items
              if (curSgStats != null)
              {
                 //curSgStats.TreeDefaultValueStats.Populate();
                 // check for selected TDV
                 if (selectedItemsGridViewTDV.SelectedItems.Count <= 0)
                 {
                    MessageBox.Show("Sample Group Missing Species", "Information");
                    return (1);
                 }
                 curSgStats.StratumStats = Owner.currentStratumStats;
                 // Create SampleGroupStats_TDV map table links
                 curSgStats.Save();
                 curSgStats.TreeDefaultValueStats.Save();
              }
           // end loop
           }
            return (0);
        }
      
      private void saveStratumStats()
      {
         if (Owner.currentStratum.IsPersisted)
         {
            // set the forign key
            Owner.currentStratumStats.Stratum = Owner.currentStratum;
            Owner.currentStratumStats.Method = "100";
            Owner.currentStratumStats.Code = Owner.currentStratum.Code;
            Owner.currentStratumStats.Description = Owner.currentStratum.Description;
            Owner.currentStratumStats.SgSetDescription = textBoxSGsetDescr.Text;
            Owner.currentStratumStats.Save();

         }
         else
         {
            throw new Exception("currentStratum persisted error");
                // display a message and return to unit page
         }
      }

      #endregion

       private void buttonNewTDV_Click(object sender, EventArgs e)
       {
          // open the TDV in a select data grid on a new form
          TDV_Select tdvSelect = new TDV_Select(this);
          newTDV = new BindingList<TreeDefaultValueDO>(Owner.cdDAL.Read<TreeDefaultValueDO>("TreeDefaultValue", "ORDER BY Species", 0));
          tdvSelect.bindingSourceTreeDV.DataSource = newTDV;
          tdvSelect.ShowDialog(this);
          
          //Owner.bindingSourceTDV.DataSource = Owner.Owner.cdTreeDefaults;
          
          setTreeDefaultValue(product);
       }

      private void dataGridViewSG_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
      {
         //MessageBox.Show("CellBeginEdit");
//         columnName = this.dataGridViewSG.Columns[e.ColumnIndex].Name;
      }
      private void dataGridViewSG_CellValueChanged(object sender, DataGridViewCellEventArgs e)
      {
         columnName = this.dataGridViewSG.Columns[e.ColumnIndex].Name;
         if (columnName == "primaryProduct")
         {
            //Owner.currentSgStats.TreeDefaultValueStats.Populate();
            Owner.currentSgStats.TreeDefaultValueStats.Clear();
            product = this.dataGridViewSG[e.ColumnIndex, e.RowIndex].Value.ToString();
            setTreeDefaultValue(product);
         }
      }
      private void dataGridViewSG_RowEnter(object sender, DataGridViewCellEventArgs e)
      {
         //MessageBox.Show("RowEnter");

         //get product value for current row
         if (dataGridViewSG.CurrentRow != null)
         {
            int column = dataGridViewSG.Columns["primaryProduct"].Index;
            product = this.dataGridViewSG[column, e.RowIndex].Value.ToString();
            setTreeDefaultValue(product);
         }
      }
      private void setTreeDefaultValue(String prod)
      {
         //Owner.cdTreeDefaults.Clear();
//         foreach (TreeDefaultValueDO myTDV in Owner.myTreeDefaultList)
//         {
//            if (myTDV.PrimaryProduct == prod)
//               Owner.cdTreeDefaults.Add(myTDV);
//         }
         var visableTDV = (from tdv in Owner.cdTreeDefaults
                           where tdv.PrimaryProduct == prod
                           select tdv).ToList();
         bindingSourceTDV.DataSource = visableTDV;
         //bindingSourceTDV.DataSource = Owner.cdTreeDefaults;
      }

      private void buttonDelete_Click(object sender, EventArgs e)
      {
         Owner.currentSgStats.Delete();

         Owner.cdSgStats.Remove(Owner.currentSgStats);
      }
    }
}
