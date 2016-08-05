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
using FMSC.ORM.Core.SQL;


namespace CruiseDesign.Strata_setup
{
   public partial class Working : Form
   {
      public Working(CruiseDesignMain Main, string dalPathRecon, bool reconExists, int err)
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
               err = 1;
            }
            else
            {

               df.cdDAL = Main.cdDAL;
               df.rFile = dalPathRecon;

               this.backgroundWorker1.RunWorkerAsync(df);
               err = 0;
               // background worker
            }
         }
      }

        public DAL rDAL;

      struct dataFiles
      {
            public DAL cdDAL;
         public string rFile;
      };
      dataFiles df;

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
            Logger.Log.E(ie);
         }
         catch (System.Exception ie)
         {
            Logger.Log.E(ie);
         }

         copyTablesToDesign(df);
      }  

      private void copyTablesToDesign(dataFiles df)
      {
         // copy Sale table
//         df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.SALE._NAME, null, OnConflictOption.Ignore);
         var sale = rDAL.Read<SaleDO>("Sale", null, null);
         foreach (SaleDO sl in sale)
         {
            sl.DAL = df.cdDAL;
            sl.Save();
         }

         //copy CuttingUnit table
            //df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.CUTTINGUNIT._NAME, null, OnConflictOption.Ignore);
         foreach (CuttingUnitDO cu in rDAL.From<CuttingUnitDO>().Query())
         {
             df.cdDAL.Insert(cu, OnConflictOption.Replace);
         }
         //copy TreeDefaultValues table
            //df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.TREEDEFAULTVALUE._NAME, null, OnConflictOption.Ignore);
         foreach (TreeDefaultValueDO tdv in rDAL.From<TreeDefaultValueDO>().Query())
         {
                df.cdDAL.Insert(tdv, OnConflictOption.Replace);
         }
         //copy globals table
            //df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.GLOBALS._NAME, null, OnConflictOption.Ignore);
         foreach (GlobalsDO gl in rDAL.From<GlobalsDO>().Query())
         {
                df.cdDAL.Insert(gl, OnConflictOption.Replace);
         }
         //copy logfieldsetupdefault
            //df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.LOGFIELDSETUPDEFAULT._NAME, null, OnConflictOption.Ignore);
         foreach (LogFieldSetupDefaultDO lfd in rDAL.From<LogFieldSetupDefaultDO>().Query())
         {
                df.cdDAL.Insert(lfd, OnConflictOption.Replace);
         }
         //copy messagelog
            //df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.MESSAGELOG._NAME, null, OnConflictOption.Ignore);
         foreach (MessageLogDO ml in rDAL.From<MessageLogDO>().Query())
         {
                df.cdDAL.Insert(ml, OnConflictOption.Replace);
         }
         //copy reports
            //df.cdDAL.DirectCopy(rDAL, "Reports", null, OnConflictOption.Ignore);
         foreach (ReportsDO rpt in rDAL.From<ReportsDO>().Query())
         {
                df.cdDAL.Insert(rpt, OnConflictOption.Replace);
         }
         //copy treefieldsetupdefault
            //df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.TREEFIELDSETUPDEFAULT._NAME, null, OnConflictOption.Ignore);
         foreach (TreeFieldSetupDefaultDO tfd in rDAL.From<TreeFieldSetupDefaultDO>().Query())
         {
                df.cdDAL.Insert(tfd, OnConflictOption.Replace);
         }
         //copy volumeequations
            //df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.VOLUMEEQUATION._NAME, null, OnConflictOption.Ignore);
         foreach (VolumeEquationDO veq in rDAL.From<VolumeEquationDO>().Query())
         {
                df.cdDAL.Insert(veq, OnConflictOption.Replace);
         }
         //copy treeauditvalue
            //df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.TREEAUDITVALUE._NAME, null, OnConflictOption.Ignore);
         foreach (TreeAuditValueDO tav in rDAL.From<TreeAuditValueDO>().Query())
         {
                df.cdDAL.Insert(tav, OnConflictOption.Replace);
         }
         //copy treedefaultvaluetreeauditvalue
            //df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.TREEDEFAULTVALUETREEAUDITVALUE._NAME, null, OnConflictOption.Ignore);
         foreach (TreeDefaultValueTreeAuditValueDO tdvtav in rDAL.From<TreeDefaultValueTreeAuditValueDO>().Query())
         {
                df.cdDAL.Insert(tdvtav, OnConflictOption.Replace);
         }
         //copy tally
            //df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.TALLY._NAME, null, OnConflictOption.Ignore);
         foreach (TallyDO tal in rDAL.From<TallyDO>().Query())
         {
                df.cdDAL.Insert(tal, OnConflictOption.Replace);
         }

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
