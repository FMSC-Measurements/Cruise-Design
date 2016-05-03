using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CruiseDAL;
using CruiseDAL.DataObjects;


namespace CruiseDesign.ProductionDesign
{
   public partial class ProductionDesignMain : Form
   {

      #region Initialize
      public ProductionDesignMain(CruiseDesignMain Main)
      {
         InitializeComponent();

         cdDAL = Main.cdDAL;
         InitializeDatabaseTables();
         InitializeDataBindings();
         double saleError = getSaleError();

         textBoxError.Text = (Math.Round(saleError, 2)).ToString();
         double totVolume = cdStratumStats.Sum(P => P.TotalVolume);
         textBoxVolume.Text = (Math.Round(totVolume, 0)).ToString();

      }
 
        // add the binding lists
      //public DAL rDAL { get; set; }
      public DAL cdDAL { get; set; }
      //public DAL fsDAL { get; set; }

      public SaleDO mySale;
      public SampleGroupStatsDO currentSgStats;
      public StratumStatsDO currentStratumStats;
      public StratumDO currentStratum;
      public StratumStatsDO msSelectedStratum;
      public StratumStatsDO myStratumStats;

      //public List<StratumDO> cdStratum;
      public BindingList<SampleGroupStatsDO> cdSgStats { get; set; }
      public BindingList<StratumStatsDO> cdStratumStats { get; set; }
      public BindingList<StratumStatsDO> msStratumStats { get; set; }
      public List<GlobalsDO> myGlobals { get; set; }

      string sColEdit;
      public string meth, dalPath, dalPathR, defaultUOM;
      float totAcres;
      private void InitializeDatabaseTables()
      {
        // cdStratum = new List<StratumDO>(cdDAL.Read<StratumDO>("Stratum", null, null));
         cdStratumStats = new BindingList<StratumStatsDO>(cdDAL.Read<StratumStatsDO>("StratumStats", "JOIN Stratum ON StratumStats.Stratum_CN = Stratum.Stratum_CN AND StratumStats.Method = Stratum.Method AND StratumStats.Used = 1 ORDER BY Stratum.Code", null));
         mySale = cdDAL.ReadSingleRow<SaleDO>("Sale", null, null);
         myGlobals = cdDAL.Read<GlobalsDO>("Globals", "WHERE Block = ?", "CruiseDesign");
      }
      
      private void InitializeDataBindings()
      {
         bindingSourceStratumStats.DataSource = cdStratumStats;
      }

 
      private void bindingSourceStratumStats_CurrentChanged(object sender, EventArgs e)
      {
         // display the SgStats
         if (currentStratumStats != null) currentStratumStats.Save();

         currentStratumStats = bindingSourceStratumStats.Current as StratumStatsDO;
         if (currentStratumStats != null)
         {
            cdSgStats = new BindingList<SampleGroupStatsDO>(cdDAL.Read<SampleGroupStatsDO>("SampleGroupStats", "Where StratumStats_CN = ?", currentStratumStats.StratumStats_CN));
            bindingSourceSgStats.DataSource = cdSgStats;
            meth = currentStratumStats.Method;
            totAcres = currentStratumStats.TotalAcres;

            setGridDisplay();
           // setGridFonts();
         }
      }
      private void bindingSourceSgStats_CurrentChanged(object sender, EventArgs e)
      {
         // display the TDV selected items
         if (currentSgStats != null) currentSgStats.Save();

         currentSgStats = bindingSourceSgStats.Current as SampleGroupStatsDO;
         //plotSampleSizeCheck();
      }
      #endregion

      #region Properties
      private void setGridDisplay()
      {
         // select visible fields by method, update headers
         calcStats cStat = new calcStats();
         //string meth = currentStratumStats.Method;
         int stage = cStat.isTwoStage(meth);
         if (cStat.isKz)
            kZDataGridViewTextBoxColumn.Visible = true;
         else
            kZDataGridViewTextBoxColumn.Visible = false;

         if (cStat.isFreq)
            samplingFrequencyDataGridViewTextBoxColumn.Visible = true;
         else
            samplingFrequencyDataGridViewTextBoxColumn.Visible = false;

         if ((cStat.isFreq || cStat.isKz) && stage < 20)
            insuranceFrequencyDataGridViewTextBoxColumn.Visible = true;
         else
            insuranceFrequencyDataGridViewTextBoxColumn.Visible = false;


         if (stage == 10 || stage == 11)
         {
            // second Stage columns
            cV2DataGridViewTextBoxColumn.Visible = false;
            sampleSize2DataGridViewTextBoxColumn1.Visible = false;
            // trees per plot
            treesPerPlotDataGridViewTextBoxColumn.Visible = false;

            // cruise data columns
            ReconPlotsDataGridViewTextBoxColumn.Visible = false;
            
            // sample size tooltips
            sampleSize1DataGridViewTextBoxColumn1.ToolTipText = "Number of Trees";
         
         }
         else if (stage == 12)
         {
            // second Stage columns
            cV2DataGridViewTextBoxColumn.Visible = true;
            sampleSize2DataGridViewTextBoxColumn1.Visible = true;
            // trees per plot
            treesPerPlotDataGridViewTextBoxColumn.Visible = false;
            // cruise data columns
            ReconPlotsDataGridViewTextBoxColumn.Visible = true;
            ReconPlotsDataGridViewTextBoxColumn.HeaderText = "3P Tree";
            ReconPlotsDataGridViewTextBoxColumn.ToolTipText = "Number of 1st Stage Samples from Cruise";
            // sample size tooltips
            sampleSize1DataGridViewTextBoxColumn1.ToolTipText = "Number of 3P Trees";
            sampleSize2DataGridViewTextBoxColumn1.ToolTipText = "Number of Measure Trees";
         }
         else if (stage == 21)
         {
            // second Stage columns
            cV2DataGridViewTextBoxColumn.Visible = false;
            sampleSize2DataGridViewTextBoxColumn1.Visible = false;
            // trees per plot
            treesPerPlotDataGridViewTextBoxColumn.Visible = true;
            // cruise data columns
            ReconPlotsDataGridViewTextBoxColumn.Visible = true;
            ReconPlotsDataGridViewTextBoxColumn.HeaderText = "Plots";
            ReconPlotsDataGridViewTextBoxColumn.ToolTipText = "Number of Plots from Cruise";
            // sample size tooltips
            sampleSize1DataGridViewTextBoxColumn1.ToolTipText = "Number of Plots";
         
         }
         else if (stage == 22)
         {
            // second Stage columns
            cV2DataGridViewTextBoxColumn.Visible = true;
            sampleSize2DataGridViewTextBoxColumn1.Visible = true;
            // trees per plot
            treesPerPlotDataGridViewTextBoxColumn.Visible = true;
            // cruise data columns
            ReconPlotsDataGridViewTextBoxColumn.Visible = true;
            ReconPlotsDataGridViewTextBoxColumn.HeaderText = "Plots";
            ReconPlotsDataGridViewTextBoxColumn.ToolTipText = "Number of Plots from Cruise";
            // sample size tooltips
            sampleSize1DataGridViewTextBoxColumn1.ToolTipText = "Number of Plots";
            sampleSize2DataGridViewTextBoxColumn1.ToolTipText = "Number of Measure Trees";
         }

         if (meth == "P3P")
         {
            averageHeightDataGridViewTextBoxColumn.Visible = true;
         }
         else
         {
            averageHeightDataGridViewTextBoxColumn.Visible = false;
         }

         if (meth == "PCM")
         {
            bigBAFDataGridViewTextBoxColumn.Visible = true;
         }
         else
         {
            bigBAFDataGridViewTextBoxColumn.Visible = false;
         }

         if (meth == "3PPNT")
         {
            insuranceFrequencyDataGridViewTextBoxColumn.Visible = true;
            sampleSize2DataGridViewTextBoxColumn1.ToolTipText = "Number of Measure Plots";
         }

      }

      private void plotSampleSizeCheck()
      {
         // check for plot cruise, if yes, all SampleSize1 is changed and errors recomputed            
         if (meth == "FIX" || meth == "FCM" || meth == "F3P" || meth == "PNT" || meth == "PCM" || meth == "P3P" || meth == "3PPNT")
            setFirstStagePlots(currentSgStats.SampleSize1);
      }


      #endregion

      # region Grid Control


      private void dataGridViewSgStats_CellEndEdit(object sender, DataGridViewCellEventArgs e)
      {
         long n, n2;
         double sgCV, sgCV2, sgErr;
         // check for sgError, SampleSize1, SampleSize2
         calcStats cStats = new calcStats();
         // find stage 11=tree,single 12=tree,2 stage 21=plot,single 22=plot,2 stage
         int stage = cStats.isTwoStage(meth);

         sColEdit = this.dataGridViewSgStats.Columns[e.ColumnIndex].DataPropertyName;
         if (sColEdit == "SgError")
         {
            //string sgErr = dataGridViewSgStats["SgError", e.RowIndex].Value.ToString();
            sgErr = currentSgStats.SgError;
            sgCV = currentSgStats.CV1;
            // find errors stage 11=tree,single 12=tree,2 stage 21=plot,single 22=plot,2 stage
            if (stage == 11)
            {
               float tpa = currentSgStats.TreesPerAcre;
               long N = (long)Math.Ceiling(tpa * totAcres);

               if (sgErr == 0)
                  currentSgStats.SampleSize1 = N;
               else
                  currentSgStats.SampleSize1 = cStats.getSampleSize(sgErr, sgCV, N);
            }
            else if (stage == 21)
            {

               currentSgStats.SampleSize1 = cStats.getSampleSize(sgErr, sgCV);
               setFirstStagePlots(currentSgStats.SampleSize1);
            }
            else if (stage == 12 || stage == 22)
            {
               sgCV2 = currentSgStats.CV2;
               cStats.getTwoStageSampleSize(sgCV, sgCV2, sgErr);
               currentSgStats.SampleSize1 = cStats.sampleSize1;
               currentSgStats.SampleSize2 = cStats.sampleSize2;
               //call routine to loop through
               setFirstStagePlots(currentSgStats.SampleSize1);

            }
         }

         else if (sColEdit == "SampleSize1")
         {
            if (stage == 11 || stage == 12)
            {
               getSgErr(stage, currentSgStats);

            }
            else if (stage == 21 || stage == 22)
            {
               // check for plot cruise, if yes, all SampleSize1 is changed and errors recomputed            
               setFirstStagePlots(currentSgStats.SampleSize1);
            }
         }
         else if (sColEdit == "SampleSize2")
         {
            // calculate sgError
            getSgErr(stage,currentSgStats);
            // change freq, KZ
         }

         getStratumStats();
         bindingSourceStratumStats.DataSource = cdStratumStats;
         bindingSourceSgStats.DataSource = cdSgStats;

         double saleError = getSaleError();

      }

      #endregion
      
      #region Calculations

      //***************************
      private double getSaleError()
      {
         double totVol, strError, saleError;
         double sumVol = 0;
         double sumError = 0;
         //crewCost, crewSize, costPaint, paintTrees, travelTime, timeTree, timePlot, walkRate;
         //loop through myStrata
         foreach (StratumStatsDO thisStrStats in cdStratumStats)
         {
            // get total volume, stratum error
            if (mySale.DefaultUOM == "01")
               totVol = thisStrStats.TotalVolume * 1000.0;
            else
               totVol = thisStrStats.TotalVolume * 100.0;

            strError = thisStrStats.StrError;

            sumError += (totVol * strError) * (totVol * strError);
            sumVol += totVol;

         }
         if (sumVol <= 0) return (0);

         saleError = Math.Sqrt(sumError) / sumVol;

         textBoxError.Text = (Math.Round(saleError, 2)).ToString();
         double totVolume = cdStratumStats.Sum(P => P.TotalVolume);
         textBoxVolume.Text = (Math.Round(totVolume, 0)).ToString();
         return (saleError);
      }
      //*******************************
      private void getSgErr(int stage, SampleGroupStatsDO _currentSgStats)
      {
         double sgCV = _currentSgStats.CV1;
         long n = _currentSgStats.SampleSize1;
         calcStats cStat = new calcStats();
         // check for plot cruise, if yes, all SampleSize1 is changed and errors recomputed            
         if (stage == 11)
         {
            float tpa = _currentSgStats.TreesPerAcre;
            long N = (long)Math.Ceiling(tpa * totAcres);

            _currentSgStats.SgError = Convert.ToSingle(Math.Round(cStat.getSampleError(sgCV, n, 0, N), 2));
         }
         else if (stage == 21)
         {
            _currentSgStats.SgError = Convert.ToSingle(Math.Round(cStat.getSampleError(sgCV, n, 0), 2));
         }
         else if (stage == 12 || stage == 22)
         {
            _currentSgStats.SgError = Convert.ToSingle(Math.Round(cStat.getTwoStageError(sgCV, currentSgStats.CV2, n, currentSgStats.SampleSize2), 2));
         }

      }
      //*******************************
      private void getN2()
      {
         float tpp = currentSgStats.TreesPerPlot;
         long n = currentSgStats.SampleSize1;
         long n2 = Convert.ToInt32(tpp * n);

         currentSgStats.SampleSize2 = n2;
      }
      //*******************************
      private void getEstTree()
      {
         currentSgStats.TPA_Def = (long)(currentSgStats.TreesPerAcre * totAcres);

      }
      //*******************************
      private void getEstVol()
      {

         currentSgStats.VPA_Def = (long)(currentSgStats.VolumePerAcre * totAcres);

      }
      //********************************************************
      private void setFirstStagePlots(long sampSize)
      {
         // loop through all sample groups
         long _freq;
         float totalTrees;
         calcStats cStat = new calcStats();
         foreach (SampleGroupStatsDO thisSgStats in cdSgStats)
         {
            // change first stage sample size
            thisSgStats.SampleSize1 = sampSize;
            // update Freq, KZ, sgErr
           
            
            if (meth == "PNT" || meth == "FIX")
            {
               // update error
               thisSgStats.SgError = Convert.ToSingle(Math.Round(cStat.getSampleError(thisSgStats.CV1, sampSize, 0), 2));
               thisSgStats.SampleSize2 = (long)(sampSize * thisSgStats.TreesPerPlot);
            }
            else
            {
               long n2 = thisSgStats.SampleSize2;
               if (n2 < 1) return;
               float tpp = thisSgStats.TreesPerPlot;
               float vpa = thisSgStats.VolumePerAcre;
               float tpa = thisSgStats.TreesPerAcre;
               if (tpa <= 0) tpa = 1;

               if (meth == "PCM" || meth == "FCM")
               {
                  // update freq
                  _freq = thisSgStats.SamplingFrequency;
                  if (_freq == 1)
                  {
                     thisSgStats.SampleSize2 = Convert.ToInt32((tpp * sampSize));
                  }
                  else if (_freq <= 0)
                  {
                     _freq = Convert.ToInt32((tpp * sampSize) / n2);
                     thisSgStats.SamplingFrequency = _freq;
                     thisSgStats.SampleSize2 = Convert.ToInt32((tpp * sampSize) / _freq);
                  }
                  else
                  {
                     thisSgStats.SampleSize2 = Convert.ToInt32((tpp * sampSize) / _freq);
                  }
               }
               else if (meth == "F3P")
               {
                  long _kz = thisSgStats.KZ;
                  totalTrees = tpp * sampSize;
                  float totalVol = (vpa / tpa) * totalTrees;

                  if (_kz > totalVol)
                  {
                     thisSgStats.SampleSize2 = 1;
                  }
                  else if (_kz <= 1)
                     thisSgStats.SampleSize2 = Convert.ToInt32(totalTrees);
                  else
                  {

                     n2 = Convert.ToInt32(totalVol / _kz);
                     if (n2 > totalTrees)
                     {
                        thisSgStats.SampleSize2 = Convert.ToInt32(totalTrees);
                     }
                     else
                        thisSgStats.SampleSize2 = n2;
                  }
               }
               else if (meth == "P3P")
               {
                  long _kz = thisSgStats.KZ;
                  float avgHt = thisSgStats.AverageHeight;
                  thisSgStats.SampleSize2 = Convert.ToInt32((tpp * sampSize * avgHt) / _kz);
               }
               else if (meth == "3PPNT")
               {
                  long _kz = thisSgStats.KZ;
                  n2 = Convert.ToInt32((tpp * sampSize * (vpa / tpa)) / _kz);
                  if (n2 > sampSize)
                     thisSgStats.SampleSize2 = sampSize;
                  else
                     thisSgStats.SampleSize2 = n2;
               }
               // update err
               thisSgStats.SgError = Convert.ToSingle(Math.Round(cStat.getTwoStageError(thisSgStats.CV1, thisSgStats.CV2, sampSize, thisSgStats.SampleSize2), 2));
            }
            thisSgStats.Save();
         }
      }
      //***************************
      private void getStratumStats()
      {
         //List<SampleGroupStatsDO> mySgStats;
         float totalVolumeAcre, totalVolume, wtCV1, wtCv2, volumeAcre;
         float cv1, cv2, wtErr, sgErr, treesAcre;
         long sampleSize1, sampleSize2;
         //loop through SampleGroupStats

         totalVolumeAcre = cdSgStats.Sum(P => P.VolumePerAcre);
         totalVolume = (float)((totalVolumeAcre * currentStratumStats.TotalAcres));

         treesAcre = cdSgStats.Sum(P => P.TreesPerAcre);
         sampleSize1 = 0;
         sampleSize2 = cdSgStats.Sum(P => P.SampleSize2); 
         wtCV1 = 0;
         wtCv2 = 0;
         wtErr = 0;
         foreach (SampleGroupStatsDO thisSgStats in cdSgStats)
         {
            //sum volumes, trees/acre, sample sizes
            //treesAcre += thisSgStats.TreesPerAcre;

            if (meth == "FIX" || meth == "FCM" || meth == "F3P" || meth == "PNT" || meth == "PCM" || meth == "P3P" || meth == "3PPNT")
               sampleSize1 = thisSgStats.SampleSize1;
            else
               sampleSize1 += thisSgStats.SampleSize1;
            //sampleSize2 += thisSgStats.SampleSize2;
            volumeAcre = thisSgStats.VolumePerAcre;
            cv1 = thisSgStats.CV1;
            cv2 = thisSgStats.CV2;
            sgErr = thisSgStats.SgError;
            //calculate weighted CVs
            if (totalVolumeAcre > 0)
            {
               wtCV1 += cv1 * (volumeAcre / totalVolumeAcre);
               wtCv2 += cv2 * (volumeAcre / totalVolumeAcre);
            }

            wtErr += (float)Math.Pow((sgErr * (volumeAcre * currentStratumStats.TotalAcres)), 2);
         }
            //save calculated values
         if (totalVolume > 0)
            currentStratumStats.StrError = (float)Math.Sqrt(wtErr) / totalVolume;
         else
            currentStratumStats.StrError = 0;

         currentStratumStats.SampleSize1 = sampleSize1;
         currentStratumStats.SampleSize2 = sampleSize2;
         currentStratumStats.WeightedCV1 = wtCV1;
         currentStratumStats.WeightedCV2 = wtCv2;

        
         currentStratumStats.Save();
      }
    

      private void buttonReturn_Click(object sender, EventArgs e)
      {
         if(currentStratumStats != null)
            currentStratumStats.Save();
         if(currentSgStats != null)
            currentSgStats.Save();
         
         //cdDAL.Dispose();

         Close();
      }
     
      #endregion

      #region Context Menu
      
      private void addInsuranceTreesToSampleSizeToolStripMenuItem_Click(object sender, EventArgs e)
      {
         long sampSize = 0;
         // find insTrees
         long insTree = currentSgStats.InsuranceFrequency;
         long cruiseSamp = currentSgStats.ReconTrees;
         // add to sample size
         if (meth == "STR" || meth == "3P")
         {
            sampSize = currentSgStats.SampleSize1;
            if (cruiseSamp == sampSize)
            {
               currentSgStats.SampleSize1 += insTree;
               getSgErr(11,currentSgStats);
            }

         }
         else if (meth == "S3P")
         {
            sampSize = currentSgStats.SampleSize2;
            if (cruiseSamp == sampSize)
            {
               currentSgStats.SampleSize2 += insTree;
               getSgErr(12,currentSgStats);
            }
         }
         // recalculate strata error

         if (cruiseSamp < sampSize)
            MessageBox.Show("Additional Samples Already Added to Sample Group", "Information");

      }

      private void resetSampleGroupSampleSizesToolStripMenuItem_Click(object sender, EventArgs e)
      {
         calcStats cStat = new calcStats();
         //string meth = currentStratumStats.Method;
         int stage = cStat.isTwoStage(meth);
         if (stage == 11)
         {
            currentSgStats.SampleSize1 = currentSgStats.ReconTrees;
         }
         else
         {
            currentSgStats.SampleSize1 = currentSgStats.ReconPlots;
            currentSgStats.SampleSize2 = currentSgStats.ReconTrees;
         }
         if(stage > 20)
            setFirstStagePlots(currentSgStats.SampleSize1);
            
         getSgErr(stage,currentSgStats);

         // recalculate strata error
      }

      private void resetSampleSizesForStratumToolStripMenuItem_Click(object sender, EventArgs e)
      {
         calcStats cStat = new calcStats();
         int stage = cStat.isTwoStage(meth);

         //loop through sample groups
         foreach (SampleGroupStatsDO thisSgStats in cdSgStats)
         {
            if (stage == 11)
            {
               thisSgStats.SampleSize1 = thisSgStats.ReconTrees;
            }
            else
            {
               thisSgStats.SampleSize1 = thisSgStats.ReconPlots;
               thisSgStats.SampleSize2 = thisSgStats.ReconTrees;
            }
            getSgErr(stage,thisSgStats);
         }

         // recalculate strata errors
      }

      private void dataGridViewSgStats_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
      {
         if (e.Button == MouseButtons.Right)
         {
            // Add this
            dataGridViewSgStats.CurrentCell = dataGridViewSgStats.Rows[e.RowIndex].Cells[e.ColumnIndex];
            // Can leave these here - doesn't hurt
            //dataGridViewSgStats.Rows[e.RowIndex].Selected = true;
           
            dataGridViewSgStats.Focus();
         }
      }
      #endregion

      private void buttonReport_Click(object sender, EventArgs e)
      {
         long newTree, oldTree, newPlot, oldPlot;
         Reports.ReportAdditional reportForm = new CruiseDesign.Reports.ReportAdditional(cdStratumStats.Count());
         //Reports.ReportViewer reportForm = new CruiseDesign.Reports.ReportViewer();
         // create sale table
         reportForm.setSaleInfo(mySale.SaleNumber, mySale.Name);
         string sPlots = "";
         string insur = "";
         string supp = "";
         // loop by stratum
         foreach (StratumStatsDO myStr in cdStratumStats)
         {
            string Meth = myStr.Method;
            List<SampleGroupStatsDO> mySgStats = new List<SampleGroupStatsDO>(cdDAL.Read<SampleGroupStatsDO>("SampleGroupStats", "Where StratumStats_CN = ?", myStr.StratumStats_CN));

            if (Meth == "P3P" || Meth == "PCM" || Meth == "3PPNT" || Meth == "F3P" || Meth == "FCM")
            {
               //find number of supplimental plots
               foreach (SampleGroupStatsDO thisSg in mySgStats)
               {
                  sPlots = thisSg.ReconPlots.ToString();

                  long newPlots = thisSg.SampleSize1;
                  long oldPlots = thisSg.ReconPlots;

                  supp = (newPlots - oldPlots).ToString();
                  
                  break;
               }


            }
               //create sale table
            myStr.Stratum.CuttingUnits.Populate();
            string units = "";
            foreach (CuttingUnitDO myCU in myStr.Stratum.CuttingUnits)
               units = units + myCU.Code + ", ";

            reportForm.createStratumTable(myStr.Code, Meth, units, sPlots, supp);
            // create sample group table

            int sgCnt = mySgStats.Count();
            int rowcnt = 0;
         
            reportForm.createSgTable(sgCnt, Meth);

            foreach (SampleGroupStatsDO thisSg in mySgStats)
            {
               string sizeT = thisSg.ReconTrees.ToString();
               string sizeP = thisSg.ReconPlots.ToString();
               insur = thisSg.InsuranceFrequency.ToString();

               rowcnt++;
               switch (Meth)
               {
                  case "100":
                  case "STR":
                  case "3P":
                     // samplesize1 and reconT to find supp
                     // insur

                     newTree = thisSg.SampleSize1;
                     oldTree = thisSg.ReconTrees;

                     supp = (newTree - oldTree).ToString();
                     reportForm.createAddSgRow(rowcnt, thisSg.Code, sizeT, supp, insur);
                     break;

                  case "PNT":
                  case "FIX":

                     newPlot = thisSg.SampleSize1;
                     oldPlot = thisSg.ReconPlots;

                     supp = (newPlot - oldPlot).ToString();
                     reportForm.createAddSgRow(rowcnt, thisSg.Code, sizeP, supp, insur);
                     break;
                  case "S3P":
                  case "F3P":
                  case "P3P":
                  case "FCM":
                  case "PCM":
                  case "3PPNT":
                     newTree = thisSg.SampleSize2;
                     oldTree = thisSg.ReconTrees;

                     supp = (newTree - oldTree).ToString();
                     reportForm.createAddSgRow(rowcnt, thisSg.Code, sizeT, supp, insur);
                     break;
               }
            }
            reportForm.AddTable2();
         }
         reportForm.ShowDialog();

      }

   }
}
