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
         this.SetOwner(Owner);

         InitializeComponent();

         // initialize data bindings
         InitializeDatabaseTables(stratumCN);
         InitializeDataBindings();
      }

        private DesignMain owner;

        public DesignMain GetOwner()
        {
            return owner;
        }

        public void SetOwner(DesignMain value)
        {
            owner = value;
        }

        public long? stratumCN;

      public BindingList<SampleGroupStatsDO> sgStats { get; set; }


      #region Intialize

      private void InitializeDatabaseTables(long? stratumCN)
      {
         
     
      }

      private void InitializeDataBindings()
      {
         bindingSourceStratumStats.DataSource = GetOwner().msStratumStats;
         //bindingSourceSgStats.DataSource = Owner.cdSgStats;
      }
      #endregion

      private void bindingSourceStratumStats_CurrentChanged(object sender, EventArgs e)
      {
            GetOwner().myStratumStats = bindingSourceStratumStats.Current as StratumStatsDO;

//            sgStats = new BindingList<SampleGroupStatsDO>(Owner.cdDAL.Read<SampleGroupStatsDO>("SampleGroupStats", "Where StratumStats_CN = ? AND SgSet = ? AND CutLeave = 'C'", Owner.myStratumStats.StratumStats_CN, Owner.myStratumStats.SgSet));
         sgStats = new BindingList<SampleGroupStatsDO>(GetOwner().cdDAL.From<SampleGroupStatsDO>().Where("StratumStats_CN = @p1 AND SgSet = @p2 AND CutLeave = 'C'").Read(GetOwner().myStratumStats.StratumStats_CN, GetOwner().myStratumStats.SgSet).ToList());
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
         if (GetOwner().myStratumStats == null)
                GetOwner().currentStratumStats.Used = 1;
         else
         {
                GetOwner().myStratumStats.Used = 1;
                GetOwner().currentStratumStats = GetOwner().myStratumStats;
                GetOwner().currentStratum.Method = GetOwner().myStratumStats.Method;
                GetOwner().currentStratum.BasalAreaFactor = GetOwner().myStratumStats.BasalAreaFactor;
                GetOwner().currentStratum.FixedPlotSize = GetOwner().myStratumStats.FixedPlotSize;
         }

         //int strRowCnt = selectedItemsGridViewStratumStats.SelectedRows.Count;
         this.DialogResult = DialogResult.OK;

      }
   }
}
