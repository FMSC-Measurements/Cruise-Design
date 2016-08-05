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
      };
      dataFiles df;
      int err = 0;


      public Processing(CruiseDesignMain Main, string dalPathRecon, bool recExists)
      {
         this.Main = Main;

         InitializeComponent();

         df.cdDAL = Main.cdDAL;
         df.rFile = dalPathRecon;
         df.reconExists = recExists;

         this.backgroundWorker1.RunWorkerAsync(df);
         // background worker
         
      }

      public CruiseDesignMain Main { get; set; }
      
      private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
      {
         
         getPopulationStatistics getStats = new getPopulationStatistics();
         dataFiles holdDF;
         holdDF = (dataFiles)e.Argument;
         
         getStats.getPopulationStats(holdDF.rFile,holdDF.cdDAL,holdDF.reconExists, err);

      }

      private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         if(err == 1)
            MessageBox.Show("Recon File has not been Processsed.\nStatistics set to default values.");

         Close();
      }
   }
}
