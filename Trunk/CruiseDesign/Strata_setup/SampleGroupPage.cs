using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CruiseDAL.DataObjects;

namespace CruiseSystemManager.CruiseWizardPages
{
    public partial class SampleGroupPage : UserControl
    {
        #region Ctor
        public SampleGroupPage(CruiseWizard Owner)
        {
            this.Owner = Owner;
            InitializeComponent();

            BindingNavigatorItemComboBox.ComboBox.DataSource = StratumBindingSource;
            BindingNavigatorItemComboBox.ComboBox.DisplayMember = "Code";
            BindingNavigatorItemComboBox.ComboBox.FormattingEnabled = true;

        }
        #endregion

        #region Properties
        public CruiseWizard Owner { get; set; }
        #endregion

        #region Initialization Methods
        private void InitializeBindings()
        {
            StratumBindingSource.DataSource = Owner.Strata;
            TreeDefaultBindingSource.DataSource = Owner.TreeDefaults;
            
        }
        #endregion

        #region Click events
        private void StrataButton_Click(object sender, EventArgs e)
        {
            Owner.GoToStrata();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Owner.Cancel();
        }

        private void FinishButton_Click(object sender, EventArgs e)
        {
            Owner.Finish();
        }

        #endregion

        private void StratumBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var stratum = StratumBindingSource.Current as StratumDO;
            SampleGroupBindingSource.DataSource = stratum.Tag as List<SampleGroupDO>;
        }


        private void SampleGroupBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            TreeDefaultListBox.ClearSelected();

            var curSG = SampleGroupBindingSource.Current as SampleGroupDO;
            if (curSG != null)
            {
                foreach (TreeDefaultValueDO td in curSG.TreeDefaultValues)
                {
                    TreeDefaultListBox.SelectedItems.Add(td);
                }
            }
        }


        private void SampleGroupBindingSource_DataSourceChanged(object sender, EventArgs e)
        {
            TreeDefaultListBox.ClearSelected();
        }

        //from what I can tell this is the only way to tell what item has been deselected 
        //with a listbox 
        private void TreeDefaultListBox_MouseClick(object sender, MouseEventArgs e)
        {
            var index = TreeDefaultListBox.IndexFromPoint(e.X, e.Y);

            if (index < 0)
            {
                //filter out
                return;
            }

            var curSG = SampleGroupBindingSource.Current as SampleGroupDO;
            var selTDV = Owner.TreeDefaults[index];
            

            if (curSG == null || selTDV == null)
            {
                return;
            }

            if (TreeDefaultListBox.SelectedIndices.Contains(index))
            {
                curSG.TreeDefaultValues.Add(selTDV);
            }
            else
            {
                curSG.TreeDefaultValues.Remove(selTDV);
            }
        }

        private void SampleGroupPage_Load(object sender, EventArgs e)
        {
            InitializeBindings();
        }




        internal void SetSelectedStratum(StratumDO stratrum)
        {
            var index = StratumBindingSource.IndexOf(stratrum);
            StratumBindingSource.Position = index;
        }
    }

        
}
