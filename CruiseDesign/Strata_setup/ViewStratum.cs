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
   public partial class ViewStratum : UserControl
   {
      public ViewStratum(StrataSetupWizard Owner)
      {
         this.Owner = Owner;
         InitializeComponent();

         // initialize data bindings
         InitializeDataBindings();
         toolStripComboBoxCode.ComboBox.DataSource = bindingSourceStratum;
         toolStripComboBoxCode.ComboBox.DisplayMember = "Code";
         toolStripComboBoxCode.ComboBox.FormattingEnabled = true;
      }
      public StrataSetupWizard Owner { get; set; }

      #region Intialize

      private void InitializeDataBindings()
      {
         // bindingSourceCurrentStratumStats.DataSource = Owner.currentStratumStats;
         bindingSourceTDV.DataSource = Owner.cdTreeDefaults;
         bindingSourceStratumStats.DataSource = Owner.cdStratumStats;
         bindingSourcSampleGroup.DataSource = Owner.cdSgStats;
         bindingSourceStratum.DataSource = Owner.cdStratum;
      }
      #endregion

      #region Click Events

      private void buttonEditSet_Click(object sender, EventArgs e)
      {
         if( Owner.currentStratumStats.SgSetDescription == "Comparison Cruise" )
         {
            MessageBox.Show("Stratum Created From Comparison Cruise.\nCannot Edit Design.", "Warning", MessageBoxButtons.OK);
            return;
         }
         Owner.GoToStrataPage(0);
      }

      private void buttonDelStr_Click(object sender, EventArgs e)
      {
         // check for last stratum stats row
            // delete last row will delete stratum
            // delete stratum
            // if last stratum in table, go to page (0)
         if (dataGridView1.RowCount > 1)
         {
            Owner.currentStratumStats.DeleteStratumStats(Owner.currentStratum.DAL, Owner.currentStratumStats.StratumStats_CN);
            Owner.cdStratumStats.Remove(Owner.currentStratumStats);
         }
         
      }

      private void buttonDone_Click(object sender, EventArgs e)
      {
         Owner.GoToUnitPage();
      }

      #endregion

      private void bindingSourceStratumStats_CurrentChanged(object sender, EventArgs e)
      {
         Owner.currentStratumStats = bindingSourceStratumStats.Current as StratumStatsDO;

         Owner.cdSgStats = new BindingList<SampleGroupStatsDO>(Owner.cdDAL.From<SampleGroupStatsDO>().Where("StratumStats_CN = @p1 AND SgSet = @p2").Read(Owner.currentStratumStats.StratumStats_CN, Owner.currentStratumStats.SgSet).ToList());
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

      private void bindingSourceStratum_CurrentChanged(object sender, EventArgs e)
      {
         Owner.currentStratum = bindingSourceStratum.Current as StratumDO;
         if (Owner.currentStratum.Stratum_CN == null) return;
         Owner.cdStratumStats = new BindingList<StratumStatsDO>(Owner.cdDAL.From<StratumStatsDO>().Where("Stratum_CN = @p1 AND Method = 100").Read(Owner.currentStratum.Stratum_CN).ToList());
         //currentStratumStats = (cdDAL.ReadSingleRow<StratumStatsDO>("StratumStats", "WHERE Stratum_CN = ? AND SgSet = 1", currentStratum.Stratum_CN));
         if (Owner.cdStratumStats.Count == 0)
         {
            Owner.cdStratumStats = new BindingList<StratumStatsDO>(Owner.cdDAL.From<StratumStatsDO>().Where("Stratum_CN = @p1").Read(Owner.currentStratum.Stratum_CN).ToList());
         }

         //set sg binding list using stratumstats_cn
         bindingSourceStratumStats.DataSource = Owner.cdStratumStats;
         //sgselectPage.bindingSourceStratumStats.DataSource = cdStratumStats;

         Owner.cdSgStats = new BindingList<SampleGroupStatsDO>(Owner.cdDAL.From<SampleGroupStatsDO>()
                                                              .Where("Where StratumStats_CN = @p1 AND SgSet = @p2")
                                                              .Read(Owner.currentStratumStats.StratumStats_CN, Owner.currentStratumStats.SgSet).ToList());
            //set TDV binding list using sgstats_tdv link

            //sgselectPage.bindingSourcSampleGroup.DataSource = cdSgStats;
            //pageHost1.Display(sgselectPage);
         bindingSourcSampleGroup.DataSource = Owner.cdSgStats;

      }

      private void buttonFinish_Click(object sender, EventArgs e)
      {
         Owner.Finish();
      }
     

    }
}
