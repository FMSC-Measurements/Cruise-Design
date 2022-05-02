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


namespace CruiseDesign
{
    public partial class StrataSetup : Form
    {
        #region Fields
        private DAL setupDAL = null;
        private UnitSetupPage unitPage = null;
        private StrataSetupPage strataPage = null;
        private SelectSGset sgselectPage = null;
        #endregion

        #region Constructor
        public StrataSetup()
        {
            InitializeComponent();
            InitializePages();
        }

        #endregion

        #region Properties
        // add the binding lists
        #endregion

        #region Initialization Methods
        private void InitializePages()
        {
            unitPage = new UnitSetupPage(this);
            pageHost1.Add(unitPage);

            strataPage = new StrataSetupPage(this);
            pageHost1.Add(strataPage);

            sgselectPage = new SelectSGset(this);
            pageHost1.Add(sgselectPage);

            this.DialogResult = DialogResult.Cancel;
        }
        #endregion

        #region Paging Methods
        public void GoToUnitPage()
        {
            pageHost1.Display(unitPage);

        }

        public void GoToStrataPage()
        {
            pageHost1.Display(strataPage);
        }

        public void GoToSgPage()
        {
            pageHost1.Display(sgselectPage);
        }
        #endregion

        //cancels the cruise wizard diolog and discards all resorurces
        public void Cancel()
        {
            if (MessageBox.Show("Are you sure you want to cancel? Entered information will not be saved", "Warning", MessageBoxButtons.OKCancel)
                == DialogResult.OK)
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

    }
}
