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


namespace CruiseDesign.Design_Pages
{
   public partial class MethodSelect : Form
   {

      public MethodSelect(DesignMain Owner)
      {
         this.Owner = Owner;

         InitializeComponent();

         // initialize data bindings
         InitializeDatabaseTables(stratumCN);
         InitializeDataBindings();
      }

      public DesignMain Owner { get; set; }
      
      public long? stratumCN;

      public BindingList<SampleGroupStatsDO> sgStats { get; set; }


      #region Intialize

      private void InitializeDatabaseTables(long? stratumCN)
      {
         
     
      }

      private void InitializeDataBindings()
      {
         bindingSourceStratumStats.DataSource = Owner.msStratumStats;
         //bindingSourceSgStats.DataSource = Owner.cdSgStats;
      }
      #endregion

      private void bindingSourceStratumStats_CurrentChanged(object sender, EventArgs e)
      {
         Owner.myStratumStats = bindingSourceStratumStats.Current as StratumStatsDO;

         sgStats = new BindingList<SampleGroupStatsDO>(Owner.cdDAL.Read<SampleGroupStatsDO>("SampleGroupStats", "Where StratumStats_CN = ? AND SgSet = ?", Owner.myStratumStats.StratumStats_CN, Owner.myStratumStats.SgSet));
         bindingSourceSgStats.DataSource = sgStats;
      }


      private void buttonDone_Click(object sender, EventArgs e)
      {
 

         this.Close();
         //this.DialogResult = DialogResult.OK;
         //Owner.Close();

      }

      private void MethodSelect_FormClosing(object sender, FormClosingEventArgs e)
      {
        // check to see if row is selected
         if (Owner.myStratumStats == null)
            Owner.currentStratumStats.Used = 1;
         else
         {
            Owner.myStratumStats.Used = 1;
            Owner.currentStratumStats = Owner.myStratumStats;
            Owner.currentStratum.Method = Owner.myStratumStats.Method;
            Owner.currentStratum.BasalAreaFactor = Owner.myStratumStats.BasalAreaFactor;
            Owner.currentStratum.FixedPlotSize = Owner.myStratumStats.FixedPlotSize;
         }

         //int strRowCnt = selectedItemsGridViewStratumStats.SelectedRows.Count;
         this.DialogResult = DialogResult.OK;

      }
   }
}
