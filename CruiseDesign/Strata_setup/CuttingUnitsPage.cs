using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CruiseDAL;

namespace CruiseSystemManager.CruiseWizardPages
{



    public partial class CuttingUnitsPage : UserControl
    {

        #region Constants 
        public string[] logMeths = new String[] { "1", "2", "3", "..." };
        #endregion

        #region Constructer
        public CuttingUnitsPage(CruiseWizard Owner)
        {
            this.Owner = Owner;
            InitializeComponent();
            InitializePage();
            InitializeBindings();
            
            
        }
        #endregion

        #region Properties
        public CruiseWizard Owner { get; set; }
        #endregion 

        #region Initialization methods
        private void InitializePage()
        {
            LogMethComboBox.Items.AddRange(logMeths); 
        }

        public void InitializeBindings()
        {
            CuttingUnitBindingSource.DataSource = Owner.CuttingUnits;
        }
        #endregion


        #region Click Envent
        private void StrataButton_Click(object sender, EventArgs e)
        {
            Owner.GoToStrata();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Owner.Cancel();
        }
        #endregion

    }
}
