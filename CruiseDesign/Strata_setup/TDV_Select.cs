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


namespace CruiseDesign.Strata_setup
{
    public partial class TDV_Select : Form
    {
        public TDV_Select(DAL dal)
        {
            InitializeComponent();

            SelectedTDV = new List<TreeDefaultValueDO>();
            TreeDefaultValues = new BindingList<TreeDefaultValueDO>(dal.From<TreeDefaultValueDO>().OrderBy("Species").Read().ToList());

            InitializeDataBindings();
        }
        public List<TreeDefaultValueDO> SelectedTDV { get; }
        BindingList<TreeDefaultValueDO> TreeDefaultValues { get; }

        private void InitializeDataBindings()
        {
            // bindingSourceCurrentStratumStats.DataSource = Owner.currentStratumStats;
            bindingSourceTreeDV.DataSource = TreeDefaultValues;
            selectedItemsGridView1.SelectedItems = SelectedTDV;
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            TreeDefaultValueDO newSpecies = new TreeDefaultValueDO();
            TreeDefaultValues.Add(newSpecies);
            //bindingSourceTreeDV.DataSource = Owner.newTDV;
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            if (SelectedTDV.Count > 0)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("No Tree Defaults Selected");
            }
        }
    }
}
