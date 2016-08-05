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
      public TDV_Select(StrataSetupPage Owner)
      {
         this.Owner = Owner;

         InitializeComponent();

         InitializeDataBindings();
      }
      
      new public StrataSetupPage Owner { get; set; }                        // used to be a warning w/o new
      List<TreeDefaultValueDO> selectedTDV = new List<TreeDefaultValueDO>();

      private void InitializeDataBindings()
      {
         // bindingSourceCurrentStratumStats.DataSource = Owner.currentStratumStats;
        bindingSourceTreeDV.DataSource = Owner.newTDV;
        selectedItemsGridView1.SelectedItems = selectedTDV;
      }

      private void buttonNew_Click(object sender, EventArgs e)
      {
         TreeDefaultValueDO newSpecies = new TreeDefaultValueDO();
         Owner.newTDV.Add(newSpecies);
         bindingSourceTreeDV.DataSource = Owner.newTDV;
      }

      private void buttonReturn_Click(object sender, EventArgs e)
      {
         if (selectedTDV.Count > 0)
         {
            foreach (TreeDefaultValueDO tdv in selectedTDV)
            {
               if (!Owner.Owner.cdTreeDefaults.Contains(tdv))
                  Owner.Owner.cdTreeDefaults.Add(tdv);

               if (!Owner.Owner.myTreeDefaultList.Contains(tdv))
                  Owner.Owner.myTreeDefaultList.Add(tdv);
            }
            //Owner.bindingSourceTDV.DataSource = Owner.Owner.cdTreeDefaults;
         }
         else
            return;

         this.DialogResult = DialogResult.OK;
         this.Close();
      }
   }
}
