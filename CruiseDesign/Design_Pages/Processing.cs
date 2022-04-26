using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CruiseDesign.Stats;
using CruiseDAL;
using CruiseDAL.DataObjects;



namespace CruiseDesign.Design_Pages
{
   public partial class Processing : Form
   {
      struct dataFiles
      {
         public DAL cdDAL { get; set; }
         public string rFile;
         public bool reconExists;
         public bool prodFile;
      };
      dataFiles df;
      int err = 0;


      public Processing(CruiseDesignMain Main, string dalPathRecon, bool recExists, bool prodFile)
      {
         this.Main = Main;
         Main.errFlag = 0;
         InitializeComponent();
         // check if file is processed
         if(prodFile)
         {
            if (!checkProcessedFile(Main.cdDAL))
            {
               // if not, set error code, return
               Main.errFlag = 1;
               MessageBox.Show("Cruise Not Processed. Please Process Cruise Before Continuing.", "Warning");
               return;
            }
         }

         df.cdDAL = Main.cdDAL;
         df.rFile = dalPathRecon;
         df.reconExists = recExists;
         df.prodFile = prodFile;

         this.backgroundWorker1.RunWorkerAsync(df);
         // background worker
         
      }

      public CruiseDesignMain Main { get; set; }

      private bool checkProcessedFile(DAL pDAL)
      {
         // get LCD file
         List<LCDDO> selectedLCD = pDAL.From<LCDDO>().Read().ToList();

         // if NULL, return false
         if (selectedLCD.Count == 0)
            return (false);
         
         // else, return true
         return (true);
      }
      
      private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
      {
         
         dataFiles holdDF;
         holdDF = (dataFiles)e.Argument;

         if (holdDF.prodFile)
         {
            getProductionStatistics getProd = new getProductionStatistics();
            getProd.getProductionStats(holdDF.cdDAL, err);
         }
         else
         {
            getPopulationStatistics getStats = new getPopulationStatistics();
            getStats.getPopulationStats(holdDF.rFile, holdDF.cdDAL, holdDF.reconExists, err);
         }
      }

      private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         if(err == 1)
            MessageBox.Show("Recon File has not been Processsed.\nStatistics set to default values.");

         Close();
      }
   }
}
