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
   public partial class Working : Form
   {
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

      public DAL rDAL { get; set; }

      struct dataFiles
      {
         public DAL cdDAL { get; set; }
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
         df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.SALE._NAME, null, OnConflictOption.Ignore);
         //copy CuttingUnit table
         df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.CUTTINGUNIT._NAME, null, OnConflictOption.Ignore);
         //copy TreeDefaultValues table
         df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.TREEDEFAULTVALUE._NAME, null, OnConflictOption.Ignore);
         //copy globals table
         df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.GLOBALS._NAME, null, OnConflictOption.Ignore);
         //copy logfieldsetupdefault
         df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.LOGFIELDSETUPDEFAULT._NAME, null, OnConflictOption.Ignore);
         //copy messagelog
         df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.MESSAGELOG._NAME, null, OnConflictOption.Ignore);
         //copy reports
         df.cdDAL.DirectCopy(rDAL, "Reports", null, OnConflictOption.Ignore);
         //copy treefieldsetupdefault
         df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.TREEFIELDSETUPDEFAULT._NAME, null, OnConflictOption.Ignore);
         //copy volumeequations
         df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.VOLUMEEQUATION._NAME, null, OnConflictOption.Ignore);
         //copy treeauditvalue
         df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.TREEAUDITVALUE._NAME, null, OnConflictOption.Ignore);
         //copy treedefaultvaluetreeauditvalue
         df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.TREEDEFAULTVALUETREEAUDITVALUE._NAME, null, OnConflictOption.Ignore);
         //copy tally
         df.cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.TALLY._NAME, null, OnConflictOption.Ignore);
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
