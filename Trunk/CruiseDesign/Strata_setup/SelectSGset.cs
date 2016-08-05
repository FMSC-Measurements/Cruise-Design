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
    public partial class SelectSGset : UserControl
    {

        public SelectSGset(StrataSetupWizard Owner)
        {
            this.Owner = Owner;
            InitializeComponent();

           // initialize data bindings
            InitializeDataBindings();
        }

        public StrataSetupWizard Owner { get; set; }

        #region Intialize

        private void InitializeDataBindings()
        {
           // bindingSourceCurrentStratumStats.DataSource = Owner.currentStratumStats;
           bindingSourceTDV.DataSource = Owner.cdTreeDefaults;
           bindingSourceStratumStats.DataSource = Owner.cdStratumStats;

           bindingSourcSampleGroup.DataSource = Owner.cdSgStats;
        }
        #endregion

        
        #region Click Events

        private void buttonEditSet_Click(object sender, EventArgs e)
        {
            
           Owner.GoToStrataPage(0);
        }

        private void buttonDelStr_Click(object sender, EventArgs e)
        {

        }

        private void buttonDone_Click(object sender, EventArgs e)
        {

           Owner.GoToUnitPage();
        }

        #endregion

        private void bindingSourceStratumStats_CurrentChanged(object sender, EventArgs e)
        {
           Owner.currentStratumStats = bindingSourceStratumStats.Current as StratumStatsDO;
 
           Owner.cdSgStats = new BindingList<SampleGroupStatsDO>(Owner.cdDAL.Read<SampleGroupStatsDO>("SampleGroupStats", "Where StratumStats_CN = ? AND SgSet = ?", Owner.currentStratumStats.StratumStats_CN, Owner.currentStratumStats.SgSet));
           bindingSourcSampleGroup.DataSource = Owner.cdSgStats;

        }

        private void bindingSourcSampleGroup_CurrentChanged(object sender, EventArgs e)
        {
           // display the TDV selected items
           Owner.currentSgStats = bindingSourcSampleGroup.Current as SampleGroupStatsDO;
           if (Owner.currentSgStats != null)
           {
              Owner.currentSgStats.TreeDefaultValueStats.Populate();
              bindingSourceTDV.DataSource = Owner.currentSgStats.TreeDefaultValueStats;
           }
        }
    }
}
