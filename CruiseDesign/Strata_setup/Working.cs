using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CruiseDAL;
using CruiseDAL.DataObjects;
using FMSC.ORM.Core.SQL;

namespace CruiseDesign.Strata_setup
{
  
   public partial class Working : Form
   {
      public DAL rDAL { get; set; }

      struct dataFiles
      {
         public DAL cdDAL { get; set; }
         public string rFile;
      };
      dataFiles df;
 
      public Working(CruiseDesignMain Main, string dalPathRecon, bool reconExists)
      {
         InitializeComponent();
 
         if(!reconExists)
         {
            Finish(true);
         }
         else if (reconExists)
         {
            // create or open DAL
            if (!Main.openDesignFile())
            {
               MessageBox.Show("Unable to create the design file", "Information");
               return;
            }

            df.cdDAL = Main.cdDAL;
            df.rFile = dalPathRecon;

            this.backgroundWorker1.RunWorkerAsync(df);
            // background worker
         }
      }


      private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
      {
         createNewDatabase((dataFiles)e.Argument);
      }

        private void createNewDatabase(dataFiles df)
      {
         try
         {
            rDAL = new DAL(df.rFile);
         }
         catch (System.IO.IOException ie)
         {
            //Logger.Log.E(ie);
         }
         catch (System.Exception ie)
         {
            //Logger.Log.E(ie);
         }

         copyTablesToDesign(df);
      }  

      private void copyTablesToDesign(dataFiles df)
      {
            // copy Sale table
            //       df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.SALE._NAME, null, OnConflictOption.Ignore);
            var sale = rDAL.From<SaleDO>().Read().ToList();
         foreach (SaleDO sl in sale)
         {
            sl.DAL = df.cdDAL;
            sl.Save();
         }

            //copy CuttingUnit table
            //df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.CUTTINGUNIT._NAME, null, OnConflictOption.Ignore);
            foreach (CuttingUnitDO fld in rDAL.From<CuttingUnitDO>().Query())
            {
                df.cdDAL.Insert(fld,"CuttingUnit", Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy TreeDefaultValues table
            //df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.TREEDEFAULTVALUE._NAME, null, OnConflictOption.Ignore);
            foreach (TreeDefaultValueDO fld in rDAL.From<TreeDefaultValueDO>().Query())
            {
                df.cdDAL.Insert(fld, "TreeDefaultValue", Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy globals table
            //df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.GLOBALS._NAME, null, OnConflictOption.Ignore);
            foreach (GlobalsDO fld in rDAL.From<GlobalsDO>().Query())
            {
                df.cdDAL.Insert(fld,"Globals", Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy logfieldsetupdefault
            //df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.LOGFIELDSETUPDEFAULT._NAME, null, OnConflictOption.Ignore);
            foreach (LogFieldSetupDefaultDO fld in rDAL.From<LogFieldSetupDefaultDO>().Query())
            {
                df.cdDAL.Insert(fld,"LogfieldSetupDefault", Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy logfieldsetup
            //df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.LOGFIELDSETUP._NAME, null, OnConflictOption.Ignore);
            //copy messagelog
            //df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.MESSAGELOG._NAME, null, OnConflictOption.Ignore);
            foreach (MessageLogDO fld in rDAL.From<MessageLogDO>().Query())
            {
                df.cdDAL.Insert(fld, "MessageLog", Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy reports
            //df.cdDAL.DirectCopy(rDAL, "Reports", null, OnConflictOption.Ignore);
            foreach (ReportsDO fld in rDAL.From<ReportsDO>().Query())
            {
                df.cdDAL.Insert(fld,"Reports", Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy treefieldsetupdefault
            //df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.TREEFIELDSETUPDEFAULT._NAME, null, OnConflictOption.Ignore);
            foreach (TreeFieldSetupDefaultDO fld in rDAL.From<TreeFieldSetupDefaultDO>().Query())
            {
                df.cdDAL.Insert(fld,"TreeFieldSetupDefault" ,Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy treefieldsetup
            //df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.TREEFIELDSETUP._NAME, null, OnConflictOption.Ignore);
            //copy volumeequations
            //df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.VOLUMEEQUATION._NAME, null, OnConflictOption.Ignore);
            foreach (VolumeEquationDO fld in rDAL.From<VolumeEquationDO>().Query())
            {
                df.cdDAL.Insert(fld,"VolumeEquation", Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy treeauditvalue
            //df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.TREEAUDITVALUE._NAME, null, OnConflictOption.Ignore);
            foreach (TreeAuditValueDO fld in rDAL.From<TreeAuditValueDO>().Query())
            {
                df.cdDAL.Insert(fld,"TreeAuditValue", Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy treedefaultvaluetreeauditvalue
            //df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.TREEDEFAULTVALUETREEAUDITVALUE._NAME, null, OnConflictOption.Ignore);
            foreach (TreeDefaultValueTreeAuditValueDO fld in rDAL.From<TreeDefaultValueTreeAuditValueDO>().Query())
            {
                df.cdDAL.Insert(fld,"TreeDefaultValueTreeAuditValue", Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy tally
            //df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.TALLY._NAME, null, OnConflictOption.Ignore);
            foreach (TallyDO fld in rDAL.From<TallyDO>().Query())
            {
                df.cdDAL.Insert(fld,"Tally", Backpack.SqlBuilder.OnConflictOption.Replace);
            }

            foreach (var lm in rDAL.From<LogMatrixDO>().Query())
            {
                df.cdDAL.Insert(lm, option: Backpack.SqlBuilder.OnConflictOption.Replace);
            }

            df.cdDAL.LogMessage("Working, Copied Data From File: " + rDAL.Path);
        }

        private void Finish(bool result)
      {
         label1.Text = "No Recon File Found.\nMake sure Recon file is in same Folder as the design file.\nDesign file not created.";
         pictureBox1.Visible = false;
         buttonOK.Enabled = true;
         buttonOK.Visible = true;
      }

      private void buttonOK_Click(object sender, EventArgs e)
      {
         Close();
      }

      private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         
         label1.Text = "File Created.";
         pictureBox1.Visible = false;
         buttonOK.Enabled = true;
         if (rDAL != null)
            rDAL.Dispose();
         //if (cdDAL != null)
         //   cdDAL.Dispose();
      }
   }
}
