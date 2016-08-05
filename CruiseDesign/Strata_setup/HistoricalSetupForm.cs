using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CruiseDesign.Strata_setup
{
    public partial class HistoricalSetupForm : Form
    {
        public HistoricalSetupForm()
        {
            InitializeComponent();
            fillStrList();
            fillUnitGrid();
        }

        #region Intialize
        private void fillUnitGrid()
        {

        }
        private void fillStrList()
        {

        }
        #endregion

        #region Methods
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion
        

        #region Click Events

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //set title bar with file name
                textBoxFilename.Text = openFileDialog1.SafeFileName;
            }

        }

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        private void sideLabelTextBoxStr_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
