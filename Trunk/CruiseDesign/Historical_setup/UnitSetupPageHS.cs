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

namespace CruiseDesign.Historical_setup
{
    public partial class UnitSetupPageHS : UserControl
    {
        int unitRowCnt;

        public UnitSetupPageHS(HistoricalSetupWizard Owner)
        {
            this.Owner = Owner;
            InitializeComponent();
            //Owner.currentStratum = new StratumDO(Owner.cdDAL);

            //bindingSourceCurrentStratum.DataSourceChanged += new EventHandler(bindingSourceCurrentStratum_DataSourceChanged);
            // initialize data bindings
            InitializeDataBindings();
            // initialize combo box
            textBoxUOM.Text = Owner.UOM;
        }

//        void bindingSourceCurrentStratum_DataSourceChanged(object sender, EventArgs e)
//        {
            // create the unit/stratum links
            //selectedItemsGridViewUnits.SelectedItems = Owner.currentStratum.CuttingUnits;
            
//        }

       private void InitializeDataBindings()
        {
           bindingSourceUnit.DataSource = Owner.cdCuttingUnits;
           //bindingSourceCurrentStratum.DataSource = Owner.currentStratum;
           bindingSourceStratum.DataSource = Owner.cdStratum;
        }

       public HistoricalSetupWizard Owner { get; set; }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Owner.Finish();
        }

        private void buttonStrata_Click(object sender, EventArgs e)
        {
           if(saveStratum() < 0)
              return;
           //list of selected units
           // get file dialog

           Owner.GoToHistPage();
        }

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
           //StratumDO strDO = bindingSourceStratum.Current as StratumDO;
           //if (strDO != null)
           //{
           //   selectedItemsGridViewUnits.SelectedItems = strDO.CuttingUnits;
           //}
        }

       private int saveStratum()
        {
           // determine the units selected
           unitRowCnt = selectedItemsGridViewUnits.SelectedItems.Count;
           if (unitRowCnt <= 0)
           {
              MessageBox.Show("Need to select at least one unit", "Information");
              return (-1);
           }
           else
           {
               // create list of selected units
              foreach (CuttingUnitDO cu in selectedItemsGridViewUnits.SelectedItems)
              {
                 Owner.selectedUnits.Add(cu.Code);
              }
           }

           // determine if Stratum Code is entered
           if (Owner.currentStratum.Code.Length <= 0)
           {
              MessageBox.Show("Enter Stratum Code", "Information");
              return(-1);
           }
           //Owner.currentStratum.Method = "100";
           Owner.currentStratum.Save();
           
          foreach (CuttingUnitDO cu in Owner.cdCuttingUnits)
           {
              cu.Save();
           }

           Owner.currentStratum.CuttingUnits.Save();
           
           return (0);
        }

        private void buttonAddStratum_Click(object sender, EventArgs e)
        {
           if (dataGridViewStratum.RowCount > 0)
           {
              if (saveStratum() < 0)
                 return;
           }

           Owner.currentStratum = new StratumDO(Owner.cdDAL);
           Owner.currentStratum.Method = "100";

           Owner.cdStratum.Add(Owner.currentStratum);

           bindingSourceStratum.DataSource = Owner.cdStratum;
           buttonStrata.Enabled = true;
        }

        private void buttonViewStratum_Click(object sender, EventArgs e)
        {

           //call SelectSGset Page
           //Owner.GoToSgPage();
        }

        private void buttonDeleteStratum_Click(object sender, EventArgs e)
        {
           //Delete Stratumstats,SampleGroupStats
           removePopulations(Owner.currentStratum.Stratum_CN);              

           Owner.cdStratum.Remove(Owner.currentStratum);
           
           
        }

       public void removePopulations(long? stratum_cn)
        {
           // loop through stratumstats and remove all rows for methods != 100

           List<StratumStatsDO> strataStats;

           strataStats = new List<StratumStatsDO>(Owner.cdDAL.Read<StratumStatsDO>("StratumStats", "WHERE Stratum_CN = ?", stratum_cn));
           // loop by stratumstats for multiple SgSets
           foreach (StratumStatsDO curStrStats in strataStats)
           {
              curStrStats.Delete();
              curStrStats.Save();
           }
        }

        private void buttonAddUnit_Click(object sender, EventArgs e)
        {
           CuttingUnitDO newUnit = new CuttingUnitDO(Owner.cdDAL);
           Owner.cdCuttingUnits.Add(newUnit);
           bindingSourceUnit.DataSource = Owner.cdCuttingUnits;
        }

    }
}
