using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CruiseDAL.DataObjects;
using Logger;
using System.ComponentModel;

namespace CruiseSystemManager.CruiseWizardPages
{
    public partial class StrataPage : UserControl
    {
        public StrataPage(CruiseWizard Owner)
        {
            this.Owner = Owner;
            InitializeComponent();
            InitializeBindings();
        }

        #region Properties
        public CruiseWizard Owner { get; set; }
        #endregion

        #region Initialion Methods
        private void InitializeBindings()
        {
            StrataBindingSource.DataSource = Owner.Strata;
            CuttingUnitBindingSource.DataSource = Owner.CuttingUnits;
            
        }
        #endregion 

        #region Click Events


        private void CuttingUnitButton_Click(object sender, EventArgs e)
        {
            Owner.GoToCuttingUnits();
        }
        private void SampleGroupButton_Click(object sender, EventArgs e)
        {
            Owner.GoToSampleGroups();
        }

        private void CurrentSTAddSGButton_Click(object sender, EventArgs e)
        {
            Owner.GoToSampleGroups(StrataBindingSource.Current as StratumDO);
        }

        private void CuttingUnitListBox_MouseClick(object sender, MouseEventArgs e)
        {
            var index = CuttingUnitListBox.IndexFromPoint(e.X, e.Y);

            if (index < 0)
            {
                //filter out
                return;
            }

            var stratum = StrataBindingSource.Current as StratumDO;
            var cuttingUnit = CuttingUnitBindingSource[index] as CuttingUnitDO;

            if (stratum == null || cuttingUnit == null)
            {
                return;
            }

            if (CuttingUnitListBox.SelectedIndices.Contains(index))
            {

                stratum.CuttingUnits.Add(cuttingUnit);
            }
            else
            {
                stratum.CuttingUnits.Remove(cuttingUnit);
            }
        }
        #endregion

        private void StrataBindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            //because we want to store a list of samplegroups for each strata
            //lets create a list and store it in the Tag. 
            //the tag is a general term for a property on an object that is 
            // designed to attatche extera data to that object that, fufills 
            //an unexpected need. 
            var newStrata = new StratumDO();
            newStrata.Tag = new List<SampleGroupDO>();
            e.NewObject = newStrata;
        }


        private void StrataBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            CuttingUnitListBox.ClearSelected();
            var stratum = StrataBindingSource.Current as StratumDO;
            //mark all cutting units in stratum.CuttingUnits as selected 
            foreach (CuttingUnitDO c in stratum.CuttingUnits)
            {
                CuttingUnitListBox.SelectedItems.Add(c);
            }
        }

    }
}
