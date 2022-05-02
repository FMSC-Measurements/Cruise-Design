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


namespace CruiseDesign.Design_Pages
{
   public partial class DesignMain : Form
   {
      public bool reconExists = new bool();

      #region Constructor
      public DesignMain(CruiseDesignMain Main, string dalPathRecon)
      {
         InitializeComponent();

         cdDAL = Main.cdDAL;
         InitializeDatabaseTables();
         InitializeDataBindings();
         getCostData();

         double saleError = getSaleError();
         
         textBoxError.Text = (Math.Round(saleError, 2)).ToString();
         double totVolume = cdStratumStats.Sum(P => P.TotalVolume);
         textBoxVolume.Text = (Math.Round(totVolume, 0)).ToString();
         textBoxCost.Text = (Math.Round(saleCost, 0)).ToString();
         //textBoxCost.Text = "0";
         dalPathR = dalPathRecon;
      }
      #endregion

      #region Properties

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
      double saleCost;
      struct costData
      {
         public int crewCost;
         public int crewSize;
         public int costPaint;
         public int paintTrees;
         public int travelTime;
         public int timeTree;
         public int timePlot;
         public int walkRate;
      };
      costData _cData;

      bool optimized = false;

        private void InitializeDatabaseTables()
        {
             //            cdStratumStats = new BindingList<StratumStatsDO>(cdDAL.Read<StratumStatsDO>("StratumStats", "JOIN Stratum ON StratumStats.Stratum_CN = Stratum.Stratum_CN AND StratumStats.Method = Stratum.Method AND StratumStats.Used = 1 ORDER BY Stratum.Code", null));
            cdStratumStats = new BindingList<StratumStatsDO>(cdDAL.From<StratumStatsDO>()
                .Join("Stratum AS s", "USING (Stratum_CN)")
                .Where("StratumStats.Used = 1").OrderBy("s.Code").Read().ToList());
            mySale = cdDAL.From<SaleDO>().Read().FirstOrDefault();
            myGlobals = cdDAL.From<GlobalsDO>().Where("Block = 'CruiseDesign'").Read().ToList();

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
//            cdSgStats = new BindingList<SampleGroupStatsDO>(cdDAL.Read<SampleGroupStatsDO>("SampleGroupStats", "Where StratumStats_CN = ? AND CutLeave = 'C'", currentStratumStats.StratumStats_CN));
            cdSgStats = new BindingList<SampleGroupStatsDO>(cdDAL.From<SampleGroupStatsDO>()
                .Where("StratumStats_CN = @p1 AND CutLeave = 'C'").Read(currentStratumStats.StratumStats_CN).ToList());
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
      private void setGridFonts()
      {
         // check for red fonts
         // loop through cdSgStats
      /*   foreach (SampleGroupStatsDO thisSgStats in cdSgStats)
         {
            if (thisSgStats.CV1 <= 0)
               cV1DataGridViewTextBoxColumn.DefaultCellStyle.ForeColor = Color.Red;
            else
               cV1DataGridViewTextBoxColumn.DefaultCellStyle.ForeColor = Color.Black;

            if (thisSgStats.CV2 <= 1)
               cV2DataGridViewTextBoxColumn.DefaultCellStyle.ForeColor = Color.Red;
            else
               cV2DataGridViewTextBoxColumn.DefaultCellStyle.ForeColor = Color.Black;
         }
         */
      }
     
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

         if(cStat.isFreq)
            samplingFrequencyDataGridViewTextBoxColumn.Visible = true;
         else
            samplingFrequencyDataGridViewTextBoxColumn.Visible = false;

         if ((cStat.isFreq || cStat.isKz) && stage < 20)
            insuranceFrequencyDataGridViewTextBoxColumn.Visible = true;
         else
            insuranceFrequencyDataGridViewTextBoxColumn.Visible = false;

         if (stage == 12 || stage == 22)
         {
            cV2DataGridViewTextBoxColumn.Visible = true;
            sampleSize2DataGridViewTextBoxColumn1.Visible = true;
            sampleSize2DataGridViewTextBoxColumn1.ToolTipText = "Number of Measure Trees";
         }
         else
         {
            cV2DataGridViewTextBoxColumn.Visible = false;
            sampleSize2DataGridViewTextBoxColumn1.Visible = false;
         }

         if (stage == 21 || stage == 22)
         {
            treesPerPlotDataGridViewTextBoxColumn.Visible = true;
            ReconPlotsDataGridViewTextBoxColumn.Visible = true;
            sampleSize1DataGridViewTextBoxColumn1.ToolTipText = "Number of Plots";
         }
         else
         {
            treesPerPlotDataGridViewTextBoxColumn.Visible = false;
            ReconPlotsDataGridViewTextBoxColumn.Visible = false;
            sampleSize1DataGridViewTextBoxColumn1.ToolTipText = "Number of Trees";
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
      private void checkRedFonts()
      {
      
      }

      private void plotSampleSizeCheck()
      {
         // check for plot cruise, if yes, all SampleSize1 is changed and errors recomputed            
         if (meth == "FIX" || meth == "FCM" || meth == "F3P" || meth == "PNT" || meth == "PCM" || meth == "P3P" || meth == "3PPNT")
            setFirstStagePlots(currentSgStats.SampleSize1);

      }

      #endregion


      # region Grid Control

      private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
      {
         // popup methods view
         string columnName = this.dataGridViewStratumStats.Columns[e.ColumnIndex].HeaderText;
         if (columnName == "Method")
         {

            //set binding list using currentStratum_cn
            
            currentStratumStats.Used = 0;
            currentStratumStats.Save();

                msStratumStats = new BindingList<StratumStatsDO>(cdDAL.From<StratumStatsDO>().Where("Stratum_CN = @p1").OrderBy("Method").Read(currentStratumStats.Stratum_CN).ToList());
            
               currentStratum = cdDAL.From<StratumDO>().Where("Stratum_CN = @p1").Read(currentStratumStats.Stratum_CN).FirstOrDefault();
            
            MethodSelect mDlg = new MethodSelect(this);
            //mDlg.Owner = this;
            //        strDlg.dalFile = dalPath;
            mDlg.ShowDialog(this);
            // recreate stratum list/SampleGroup list with new selection
            currentStratumStats.Save();
            
            currentStratum.Save();

            InitializeDatabaseTables();
            bindingSourceStratumStats.DataSource = cdStratumStats;

            getSaleError();
            optimized = false;

         }

      }

      private void dataGridViewSgStats_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
      {
         //sColEdit = this.dataGridViewStratumStats.Columns[e.ColumnIndex].DataPropertyName;
      }

      private void dataGridViewSgStats_CellEndEdit(object sender, DataGridViewCellEventArgs e)
      {
         long n, n2;
         float totalTrees;
         double sgCV, sgCV2, sgErr;
         //string meth = currentStratumStats.Method;
         // check for sgError, SampleSize1, SampleSize2, CV1, CV2, TreesPerAcre, VolumePerAcre, TreesPerPlot, AverageHeight, Sampling Frequency, KZ
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
               long N = (long)Math.Ceiling(tpa* totAcres);
               
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
            if (cStats.isFreq)
               getFreq();
            if (cStats.isKz)
               getKZ();
         }
         else if (sColEdit == "SampleSize1")
         {
            if (stage == 11 || stage == 12)
            {
               getSgErr(stage);

               if (cStats.isFreq)
                  getFreq();
               if (cStats.isKz)
                  getKZ();
            }
            else if (stage == 21 || stage == 22)
            {
               // check for plot cruise, if yes, all SampleSize1 is changed and errors recomputed            
               setFirstStagePlots(currentSgStats.SampleSize1);
           }
         }
         else if (sColEdit == "SampleSize2" || sColEdit == "CV2")
         {
            // calculate sgError
            getSgErr(stage);
            // change freq, KZ
            if (cStats.isFreq)
               getFreq();
            if (cStats.isKz)
               getKZ();
         }
         else if (sColEdit == "CV1")
         {
            // calculate sgError
            getSgErr(stage);
         }
         else if (sColEdit == "TreesPerAcre")
         {
            if (cStats.isFreq)
               getFreq();
            if (cStats.isKz)
               getKZ();

            getEstTree();
         }
         else if (sColEdit == "VolumePerAcre")
         {
            if (cStats.isKz)
               getKZ();

            getEstVol();
         }
         else if (sColEdit == "TreesPerPlot")
         {
            // calculate Frequency/KZ/BigBAF for PCM, P3P, FCM, F3P 
            if (meth == "PNT" || meth == "FIX")
               getN2();
            if (cStats.isFreq)
               getFreq();
            if (cStats.isKz)
               getKZ();
         }
         else if (sColEdit == "AverageHeight")
         {
            float tpp = currentSgStats.TreesPerPlot;
            float avgHt = currentSgStats.AverageHeight;
            n = currentSgStats.SampleSize1;
            n2 = currentSgStats.SampleSize2;
            if (n2 > 0)
               currentSgStats.KZ = Convert.ToInt32((tpp * n * avgHt) / n2);
            else
               currentSgStats.KZ = 1;
         }
         else if (sColEdit == "SamplingFrequency")
         {
            // calculate sample size, sgError
            long sFreq = currentSgStats.SamplingFrequency;
            if (sFreq < 1) return;

            float tpa = currentSgStats.TreesPerAcre;
            float totAcres = currentStratumStats.TotalAcres;
            float tpp = currentSgStats.TreesPerPlot;
            if (meth == "STR" || meth == "S3P")
            {
               totalTrees = tpa * totAcres;
               if (sFreq > totalTrees)
               {
                  currentSgStats.SampleSize1 = 1;
                  currentSgStats.SamplingFrequency = Convert.ToInt32(totalTrees);
               }
               else if (sFreq == 0)
                  currentSgStats.SampleSize1 = Convert.ToInt32(totalTrees);
               else
                  currentSgStats.SampleSize1 = Convert.ToInt32((totalTrees / sFreq));
            }
            else if (meth == "FCM" || meth == "PCM")
            {
               totalTrees = Convert.ToInt32(tpp * currentSgStats.SampleSize1);
               if (sFreq > totalTrees)
               {
                  currentSgStats.SampleSize2 = 1;
                  currentSgStats.SamplingFrequency = Convert.ToInt32(totalTrees);
                  currentSgStats.BigBAF = Convert.ToSingle(sFreq * currentStratumStats.BasalAreaFactor);
                  currentSgStats.BigFIX = Convert.ToInt32(sFreq * currentStratumStats.FixedPlotSize);
               }
               else if (sFreq == 0)
               {
                  currentSgStats.SampleSize2 = Convert.ToInt32(totalTrees);
                  currentSgStats.BigFIX = Convert.ToInt32(currentStratumStats.FixedPlotSize);
                  currentSgStats.BigBAF = currentStratumStats.BasalAreaFactor;
               }
               else
               {
                  currentSgStats.SampleSize2 = Convert.ToInt32(totalTrees / sFreq);
                  currentSgStats.BigBAF = Convert.ToSingle(sFreq * currentStratumStats.BasalAreaFactor);
                  currentSgStats.BigFIX = Convert.ToInt32(sFreq * currentStratumStats.FixedPlotSize);
               }

            }
            getSgErr(stage);
         }

         else if (sColEdit == "KZ")
         {
            // calculate sample size, sgError
            long kz = currentSgStats.KZ;
            if (kz < 1) return;

            float tpp = currentSgStats.TreesPerPlot;
            float vpa = currentSgStats.VolumePerAcre;
            float tpa = currentSgStats.TreesPerAcre;
            if (tpa <= 0) tpa = 1;
            n = currentSgStats.SampleSize1;

            if (meth == "3P")
            {
               totalTrees = tpa * totAcres;
               float totalVol = vpa * totAcres;
               
               if (kz > totalVol)
               {
                  currentSgStats.SampleSize1 = 1;
                  currentSgStats.KZ = Convert.ToInt32(totalVol);
               }
               else if (kz == 0 || kz == 1)
                  currentSgStats.SampleSize1 = Convert.ToInt32(totalTrees);
               else
               {
                  currentSgStats.SampleSize1 = Convert.ToInt32((totalVol / kz));
               }
            }
            else if (meth == "S3P")
            {
               float totalVol = vpa / tpa * n;

               if (kz > totalVol)
               {
                  currentSgStats.SampleSize2 = 1;
                  currentSgStats.KZ = Convert.ToInt32(totalVol);
               }
               else if (kz == 0 || kz == 1)
                  currentSgStats.SampleSize2 = n;
               else
               {
                  n2 = Convert.ToInt32(totalVol / kz);
                  if (n2 > n)
                  {
                     currentSgStats.SampleSize2 = n;
                     if(n > 0)
                        currentSgStats.KZ = Convert.ToInt32(totalVol / n);
                     else
                        currentSgStats.KZ = 1;
                  }
                  else
                     currentSgStats.SampleSize2 = n2;
               }
            }
            else if (meth == "P3P")
            {
               float avgHt = currentSgStats.AverageHeight;
               currentSgStats.SampleSize2 = Convert.ToInt32((tpp * n * avgHt) / kz);
            }
            else if (meth == "F3P")
            {
               totalTrees = tpp * n;
               float totalVol = (vpa / tpa) * totalTrees;

               if (kz > totalVol)
               {
                  currentSgStats.SampleSize2 = 1;
                  currentSgStats.KZ = Convert.ToInt32(totalVol);
               }
               else if (kz <= 1)
                  currentSgStats.SampleSize2 = Convert.ToInt32(totalTrees);
               else
               {

                  n2 = Convert.ToInt32(totalVol / kz);
                  if (n2 > totalTrees)
                  {
                     currentSgStats.SampleSize2 = Convert.ToInt32(totalTrees);
                     if (totalTrees > 0)
                        currentSgStats.KZ = Convert.ToInt32(totalVol / totalTrees);
                     else
                        currentSgStats.KZ = 1;
                  }
                  else
                     currentSgStats.SampleSize2 = n2;
               
               }
            }
            else if (meth == "3PPNT")
            {
               n2 = Convert.ToInt32((tpp * n * (vpa / tpa)) / kz);
               if (n2 > n)
               {
                  currentSgStats.SampleSize2 = n;
                  if (tpa > 0)
                     currentSgStats.KZ = Convert.ToInt32((tpp * n * (vpa / tpa)) / n);
                  else
                     currentSgStats.KZ = 1;
               }
               else
                  currentSgStats.SampleSize2 = n2;
            }
            getSgErr(stage);
         
         }
         else if (sColEdit == "BigBAF")
         {
            // calcuate sample size2, sgError
            // divide bigbaf by baf to fing freq (int)
            long sFreq = Convert.ToInt32(currentSgStats.BigBAF / currentStratumStats.BasalAreaFactor);
            totalTrees = Convert.ToInt32(currentSgStats.TreesPerPlot * currentSgStats.SampleSize1);
            if (sFreq > totalTrees)
            {
               currentSgStats.SampleSize2 = 1;
               currentSgStats.SamplingFrequency = Convert.ToInt32(totalTrees);
            }
            else if (sFreq == 0)
            {
               currentSgStats.SampleSize2 = Convert.ToInt32(totalTrees);
            }
            else
            {
               currentSgStats.SampleSize2 = Convert.ToInt32(totalTrees / sFreq);
            }
            currentSgStats.SamplingFrequency = sFreq;
            
            // use freq to find size2

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
         saleCost = 0;
         //crewCost, crewSize, costPaint, paintTrees, travelTime, timeTree, timePlot, walkRate;
         //loop through myStrata
         foreach (StratumStatsDO thisStrStats in cdStratumStats)
         {
            if (thisStrStats.Method != "FIXCNT")
            {
               // get total volume, stratum error
               if (mySale.DefaultUOM == "01")
                  totVol = thisStrStats.TotalVolume * 1000.0;
               else
                  totVol = thisStrStats.TotalVolume * 100.0;

               strError = thisStrStats.StrError;

               sumError += (totVol * strError) * (totVol * strError);
               sumVol += totVol;

               double strCost = getSaleCosts(thisStrStats.Method, thisStrStats.SampleSize1, thisStrStats.SampleSize2, thisStrStats.PlotSpacing, thisStrStats.TreesPerAcre, thisStrStats.TotalAcres);

               saleCost += strCost;
            }
         }
         if (sumVol <= 0) return (0);
         
         saleError = Math.Sqrt(sumError) / sumVol;
         
         textBoxError.Text = (Math.Round(saleError, 2)).ToString();
         double totVolume = cdStratumStats.Sum(P => P.TotalVolume);
         textBoxVolume.Text = (Math.Round(totVolume, 0)).ToString();
         textBoxCost.Text = (Math.Round(saleCost, 0)).ToString();
         return (saleError);
      }


      //*******************************
      private void getSgErr(int stage)
      {
         double sgCV = currentSgStats.CV1;
         long n = currentSgStats.SampleSize1;
         calcStats cStat = new calcStats();
         // check for plot cruise, if yes, all SampleSize1 is changed and errors recomputed            
         if (stage == 11)
         {
            float tpa = currentSgStats.TreesPerAcre;
            long N = (long)Math.Ceiling(tpa * totAcres);

            currentSgStats.SgError = Convert.ToSingle(Math.Round(cStat.getSampleError(sgCV, n, 0, N), 2));
         }
         else if (stage == 21)
         {
            currentSgStats.SgError = Convert.ToSingle(Math.Round(cStat.getSampleError(sgCV, n, 0), 2));
         }
         else if (stage == 12 || stage == 22)
         {
            currentSgStats.SgError = Convert.ToSingle(Math.Round(cStat.getTwoStageError(sgCV, currentSgStats.CV2, n, currentSgStats.SampleSize2), 2));
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
      //*******************************
      private void getFreq()
      {
         float tpp = currentSgStats.TreesPerPlot;
         float tpa = currentSgStats.TreesPerAcre;
         float totalTrees = tpa * totAcres;
         long n = currentSgStats.SampleSize1;
         long n2 = currentSgStats.SampleSize2;
         // calculate Frequency (str,s3p)
         if (meth == "STR" || meth == "S3P")
         {
            if (n > 0)
               currentSgStats.SamplingFrequency = Convert.ToInt32((totalTrees / n));
            else
               currentSgStats.SamplingFrequency = 0;
         }
         else if (meth == "100")
         {
            //calculate sample size for 100
            currentSgStats.SampleSize1 = Convert.ToInt32(totalTrees);
         }
         else if (meth == "FCM")
         {
            if (n2 > 0)
               currentSgStats.SamplingFrequency = Convert.ToInt32((tpp * n) / n2);
            else
               currentSgStats.SamplingFrequency = 0;
         }
         else if (meth == "PCM")
         {
            if (n2 > 0)
            {
               currentSgStats.SamplingFrequency = Convert.ToInt32((tpp * n) / n2);
               currentSgStats.BigBAF = Convert.ToSingle(currentSgStats.SamplingFrequency * currentStratumStats.BasalAreaFactor);
            }
            else
               currentSgStats.SamplingFrequency = 0;
         }
            
      }
      //*****************************
      private void getKZ()
      {
         float tpp = currentSgStats.TreesPerPlot;
         float vpa = currentSgStats.VolumePerAcre;
         float tpa = currentSgStats.TreesPerAcre;
         if (tpa <= 0) tpa = 1;
         float totalVol = vpa * totAcres;
         long n = currentSgStats.SampleSize1;
         long n2 = currentSgStats.SampleSize2;
         if (meth == "3P")
         {
            if (n > 0)
               currentSgStats.KZ = Convert.ToInt32((totalVol / n));
            else
               currentSgStats.KZ = 1;
         }
         else if (meth == "S3P")
         {
            if (n2 > 0)
               currentSgStats.KZ = Convert.ToInt32(((vpa / tpa * n) / n2));
            else
               currentSgStats.KZ = 1;
         }
         else if (meth == "P3P")
         {
            float avgHt = currentSgStats.AverageHeight;
            if (n2 > 0)
                  currentSgStats.KZ = Convert.ToInt32((tpp * n * avgHt) / n2);
            else
               currentSgStats.KZ = 1;
         }
         else if (meth == "F3P")
         {
            if (n2 > 0)
               currentSgStats.KZ = Convert.ToInt32((tpp * n * (vpa / tpa)) / n2);
            else
               currentSgStats.KZ = 1;
         }
         else if (meth == "3PPNT")
         {
            if (n2 > 0)
               currentSgStats.KZ = Convert.ToInt32((tpp * n * (vpa / tpa)) / n2);
            else
               currentSgStats.KZ = 1;
         }
      }
      //********************************************************
      private void setFirstStagePlots(long sampSize)
      {
         // loop through all sample groups
         long _freq;
         calcStats cStat = new calcStats();
         foreach (SampleGroupStatsDO thisSgStats in cdSgStats)
         {
            // change first stage sample size
            thisSgStats.SampleSize1 = sampSize;
            // update Freq, KZ, sgErr
            
            
            if (meth == "PNT" || meth == "FIX" || meth == "FIXCNT")
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
                  else
                  {
                     thisSgStats.SamplingFrequency = Convert.ToInt32((tpp * sampSize) / n2);
                  }
                  if (meth == "PCM")
                     thisSgStats.BigBAF = Convert.ToSingle(thisSgStats.SamplingFrequency * currentStratumStats.BasalAreaFactor);
               }
               else if (meth == "F3P")
               {
                  if (tpa > 0)
                     thisSgStats.KZ = Convert.ToInt32((tpp * sampSize * (vpa / tpa)) / n2);
                  else
                     thisSgStats.KZ = 1;
               }
               else if (meth == "P3P")
               {
                  if (n2 > 0)
                     thisSgStats.KZ = Convert.ToInt32((tpp * sampSize * thisSgStats.AverageHeight) / n2);
                  else
                     thisSgStats.KZ = 1;
               }
               else if (meth == "3PPNT")
               {
                  if (tpa > 0)
                     thisSgStats.KZ = Convert.ToInt32((tpp * sampSize * (vpa / tpa)) / n2);
                  else
                     thisSgStats.KZ = 1;
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
         float totalVolumeAcre, totalTreesAcre, totalVolume, wtCV1, wtCv2, volumeAcre, totalTrees;
         float cv1, cv2, wtErr, sgErr, treesAcre;
         long sampleSize1, sampleSize2;
         //loop through SampleGroupStats

         //mySgStats = new List<SampleGroupStatsDO>(cdDAL.Read<SampleGroupStatsDO>("SampleGroupStats", "Where StratumStats_CN = ?", thisStrStats.StratumStats_CN));
         // loop through sample groups

         totalVolumeAcre = cdSgStats.Sum(P => P.VolumePerAcre);
         totalVolume = (float)((totalVolumeAcre * currentStratumStats.TotalAcres));

         totalTreesAcre = cdSgStats.Sum(P => P.TreesPerAcre);
         totalTrees = totalTreesAcre * currentStratumStats.TotalAcres;
         sampleSize1 = 0;
         sampleSize2 = cdSgStats.Sum(P => P.SampleSize2);
         wtCV1 = 0;
         wtCv2 = 0;
         wtErr = 0;
         foreach (SampleGroupStatsDO thisSgStats in cdSgStats)
         {
            //sum volumes, trees/acre, sample sizes
            //treesAcre += thisSgStats.TreesPerAcre;

            if (meth == "FIX" || meth == "FCM" || meth == "F3P" || meth == "PNT" || meth == "PCM" || meth == "P3P" || meth == "3PPNT" || meth == "FIXCNT")
               sampleSize1 = thisSgStats.SampleSize1;
            else
               sampleSize1 += thisSgStats.SampleSize1;
            //sampleSize2 += thisSgStats.SampleSize2;
            volumeAcre = thisSgStats.VolumePerAcre;
            treesAcre = thisSgStats.TreesPerAcre;

            cv1 = thisSgStats.CV1;
            cv2 = thisSgStats.CV2;
            sgErr = thisSgStats.SgError;
            //calculate weighted CVs
            if (meth == "FIXCNT")
            {
               if (totalTreesAcre > 0)
               {
                  wtCV1 += cv1 * (treesAcre / totalTreesAcre);
               }

               wtErr += (float)Math.Pow((sgErr * (treesAcre * currentStratumStats.TotalAcres)), 2);
            }
            else
            {
               if (totalVolumeAcre > 0)
               {
                  wtCV1 += cv1 * (volumeAcre / totalVolumeAcre);
                  wtCv2 += cv2 * (volumeAcre / totalVolumeAcre);
               }

               wtErr += (float)Math.Pow((sgErr * (volumeAcre * currentStratumStats.TotalAcres)), 2);
            }
         }
         //save calculated values
         if (meth == "FIXCNT")
         {
            if (totalTrees > 0)
               currentStratumStats.StrError = (float)Math.Sqrt(wtErr) / totalTrees;
            else
               currentStratumStats.StrError = 0;
         }
         else
         {
            if (totalVolume > 0)
               currentStratumStats.StrError = (float)Math.Sqrt(wtErr) / totalVolume;
            else
               currentStratumStats.StrError = 0;
         }
         currentStratumStats.SampleSize1 = sampleSize1;
         currentStratumStats.SampleSize2 = sampleSize2;
         currentStratumStats.WeightedCV1 = wtCV1;
         currentStratumStats.WeightedCV2 = wtCv2;
         currentStratumStats.TreesPerAcre = totalTreesAcre;
         currentStratumStats.VolumePerAcre = totalVolumeAcre;
         if(mySale.DefaultUOM == "01")
            currentStratumStats.TotalVolume = (float)(totalVolume / 1000.0);
         else
            currentStratumStats.TotalVolume = (float)(totalVolume / 100.0);
         
         if (meth == "FIX" || meth == "FCM" || meth == "F3P" || meth == "PNT" || meth == "PCM" || meth == "P3P" || meth == "3PPNT" || meth == "FIXCNT")
         {
            if (sampleSize1 > 0)
               currentStratumStats.PlotSpacing = (int)Math.Floor(Math.Sqrt((currentStratumStats.TotalAcres * 43560) / sampleSize1));
            else
               currentStratumStats.PlotSpacing = 0;
         }
         else
         {
            if (currentStratumStats.TreesPerAcre > 0)
               currentStratumStats.PlotSpacing = (int)Math.Floor(Math.Sqrt((43560) / currentStratumStats.TreesPerAcre));
            else
               currentStratumStats.PlotSpacing = 0;
         }
         currentStratumStats.Save();
      }

      public double getSaleCosts(string Method, long n1, long n2, long plotSpace, float treesAcre, float strAcres)
      {
         double strCost = 0;
         double timeInWoods, minPerDay, numDays, totCrewCost, totPaintCost, totStrCost;

         if (_cData.walkRate == 0 || _cData.crewSize == 0 || _cData.paintTrees == 0)
         {
            strCost = 0;
         }
         else
         {
            if (Method == "100" || Method == "3P" || Method == "STR")
            {
               // timeInWoods =   time to measure trees + time to walk to each count tree + time to paint each tree (assume 12 seconds)
               timeInWoods = (float)((n1 * _cData.timeTree) / _cData.crewSize) + (float)((((treesAcre * strAcres) * plotSpace) / _cData.crewSize) / _cData.walkRate) + (float)((treesAcre * strAcres * 0.2));
               totPaintCost = (float)((treesAcre * strAcres) / _cData.paintTrees) * _cData.costPaint;
            }
            else if (Method == "S3P")
            {
               // timeInWoods =   time to measure trees + time to walk to each count tree + time to kpi and paint each tree (assume 18 seconds)
               timeInWoods = (float)((n2 * _cData.timeTree) / _cData.crewSize) + (float)((((treesAcre * strAcres) * plotSpace) / _cData.crewSize) / _cData.walkRate) + (float)((treesAcre * strAcres * 0.3));
               totPaintCost = (float)((treesAcre * strAcres) / _cData.paintTrees) * _cData.costPaint;
            }
            else
            {
               // timeInWoods =   time to measure trees + time to walk to each plot + time to establish each plot + time to paint each tree (assume 30 seconds)
               timeInWoods = (float)((n2 * _cData.timeTree) / _cData.crewSize) + (float)((n1 * plotSpace) / _cData.walkRate) + (float)(n1 * _cData.timePlot) + (float)(n2 * 0.5);
               totPaintCost = (float)((float)(n2) / (float)(_cData.paintTrees)) * _cData.costPaint;
            }

            minPerDay = 510 - (_cData.travelTime * 2);
            numDays = timeInWoods / minPerDay;
            totCrewCost = (_cData.crewCost * 10.0) * numDays;

            strCost = (totPaintCost + totCrewCost);
         }

         return (strCost);
      }

      #endregion

      private void buttonReturn_Click(object sender, EventArgs e)
      {
         if(currentStratumStats != null)
            currentStratumStats.Save();
         if(currentSgStats != null)
            currentSgStats.Save();
         
         //cdDAL.Dispose();

         Close();
      }

      private void buttonOptimize_Click(object sender, EventArgs e)
      {
         if (optimized)
         {
            var result = MessageBox.Show("Design has already been optimized.\nIf you continue, you will lose all your changes to the current design.\nContinue?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
               return;
         }
         Cursor.Current = Cursors.WaitCursor;
         optimized = true;

         if (checkForErrors())
            return;
         calcStats cStat = new calcStats();
         long strSamp, sgSamp, sgSamp1, sgSamp2;
         double combWtCV, strCV, strCalcError, sgCV, sgCalcError, totVolume;
    
         // get sale volume
         double tVolume = cdStratumStats.Sum(P => P.TotalVolume);
         if (tVolume == 0)
         {
            MessageBox.Show("No volume. Cannot calculate sample sizes.", "Warning");
            return;
         }
         if (mySale.DefaultUOM == "01")
            totVolume = tVolume * 1000.0;
         else
            totVolume = tVolume * 100.0;
         // get weighted sale cv
         double saleCV = 0;
         long strSamp1 = 0;
         long strSamp2 = 0;
         double sumError = 0;
         double sumVolume = 0;
         double tStrVol;
         foreach (StratumStatsDO thisStrStats in cdStratumStats)
         {
            
            if (thisStrStats.Method != "FIXCNT")
            {
               if (mySale.DefaultUOM == "01")
                  tStrVol = thisStrStats.TotalVolume * 1000.0;
               else
                  tStrVol = thisStrStats.TotalVolume * 100.0;

               int stage = cStat.isTwoStage(thisStrStats.Method);
               // single stage wted CV
               if (stage == 10 || stage == 11 || stage == 21)
               {
                  saleCV += thisStrStats.WeightedCV1 * (tStrVol / totVolume);
               }
               // 2 stage wted CV
               else
               {
                  combWtCV = (thisStrStats.WeightedCV1 + thisStrStats.WeightedCV2) / 2.0;
                  saleCV += combWtCV * (tStrVol / totVolume);
               }
            }
         }
         if (saleCV == 0)
         {
            MessageBox.Show("Cannot calculate weighted sale CV.", "Warning");
            return;
         }
         
         //float saleCV = cdStratumStats.Sum(P => ((P.WeightedCV1 * P.TotalVolume)/totVolume));
         double saleOptError = Convert.ToSingle(numericUpDown1.Value);
         // calculate sale level sample size
         long saleCalcSamples = cStat.getSampleSize(saleOptError, saleCV);
         // calculate sale error using t-value of 2
         double saleCalcError = cStat.getSampleError(saleCV, saleCalcSamples, 2.0); 
         // correct sample size using correct t-value
         double saleSamples = cStat.checkTValueError(saleCV, saleCalcSamples, saleCalcError);

         // prorate to strata
         foreach (StratumStatsDO thisStrStats in cdStratumStats)
         {
            if (thisStrStats.Method != "FIXCNT")
            {
               int stage = cStat.isTwoStage(thisStrStats.Method);
               long strSample1 = 0;
               long strSample2 = 0;
               double wtErr = 0;
               float acres = thisStrStats.TotalAcres;
               float stTpa = thisStrStats.TreesPerAcre;
               long stN = (long)Math.Ceiling(stTpa * acres);
               // single stage wted CV
               if (mySale.DefaultUOM == "01")
                  tStrVol = thisStrStats.TotalVolume * 1000.0;
               else
                  tStrVol = thisStrStats.TotalVolume * 100.0;

               if (stage == 10 || stage == 21)
               {
                  strCV = thisStrStats.WeightedCV1 * (tStrVol / totVolume);
                  strSamp = Convert.ToInt32((strCV / saleCV) * saleSamples);
                  strCalcError = cStat.getSampleError(thisStrStats.WeightedCV1, strSamp, 2.0);
                  // correct for t-value
                  strSamp1 = cStat.checkTValueError(thisStrStats.WeightedCV1, strSamp, strCalcError);
               }
               else if (stage == 11)
               {
                  strCV = thisStrStats.WeightedCV1 * (tStrVol / totVolume);
                  strSamp = Convert.ToInt32((strCV / saleCV) * saleSamples);
                  strCalcError = cStat.getSampleError(thisStrStats.WeightedCV1, strSamp, 2.0);
                  // correct for t-value
                  strSamp1 = cStat.checkTValueError(thisStrStats.WeightedCV1, strSamp, strCalcError, stN);
               }
               // 2 stage wted CV
               else
               {
                  combWtCV = (thisStrStats.WeightedCV1 + thisStrStats.WeightedCV2) / 2.0;
                  strCV = combWtCV * (tStrVol / totVolume);
                  strSamp = Convert.ToInt32((strCV / saleCV) * saleSamples);
                  strCalcError = cStat.getSampleError(combWtCV, strSamp, 2.0);
                  cStat.getTwoStageSampleSize(thisStrStats.WeightedCV1, thisStrStats.WeightedCV2, strCalcError);
                  // correct for t-value
                  strSamp2 = cStat.sampleSize2;
                  strSamp1 = cStat.checkTValueError2Stage(thisStrStats.WeightedCV1, thisStrStats.WeightedCV2, cStat.sampleSize1, cStat.sampleSize2, strCalcError);
               }
//                    List<SampleGroupStatsDO> mySgStats = new List<SampleGroupStatsDO>(cdDAL.Read<SampleGroupStatsDO>("SampleGroupStats", "Where StratumStats_CN = ? AND CutLeave = 'C'", thisStrStats.StratumStats_CN));
               List<SampleGroupStatsDO> mySgStats = cdDAL.From<SampleGroupStatsDO>().Where("StratumStats_CN = @p1 AND CutLeave = 'C'").Read(thisStrStats.StratumStats_CN).ToList();
               long sgSamp2Stage1 = 0;
               long sgSamp2Stage2 = 0;
               foreach (SampleGroupStatsDO thisSgStats in mySgStats)
               {
                  float sgTpa = thisSgStats.TreesPerAcre;
                  long sgN = (long)Math.Ceiling(sgTpa * acres);
                  // prorate to sample groups
                  if (stage == 11)
                  {
                     sgCV = thisSgStats.CV1 * (thisSgStats.VolumePerAcre / thisStrStats.VolumePerAcre);
                     if (thisStrStats.WeightedCV1 <= 0) sgSamp = 1;
                     else sgSamp = Convert.ToInt32((sgCV / thisStrStats.WeightedCV1) * strSamp1);
                     sgCalcError = cStat.getSampleError(thisSgStats.CV1, sgSamp, 2.0);
                     // correct for t-value
                     sgSamp1 = cStat.checkTValueError(thisSgStats.CV1, sgSamp, sgCalcError, sgN);
                     if (sgSamp1 < 3) sgSamp1 = 3;
                     sgSamp2 = 0;
                     sgCalcError = cStat.getSampleError(thisSgStats.CV1, sgSamp1, 0, sgN);
                  }
                  else if (stage == 21)
                  {
                     sgCV = thisSgStats.CV1 * (thisSgStats.VolumePerAcre / thisStrStats.VolumePerAcre);
                     if (thisStrStats.WeightedCV1 <= 0) sgSamp = 1;
                     else sgSamp = Convert.ToInt32((sgCV / thisStrStats.WeightedCV1) * strSamp1);
                     sgCalcError = cStat.getSampleError(thisSgStats.CV1, sgSamp, 2.0);
                     // correct for t-value
                     sgSamp1 = cStat.checkTValueError(thisSgStats.CV1, sgSamp, sgCalcError);
                     if (sgSamp1 < 3) sgSamp1 = 3;
                     sgSamp2 = (long)(thisSgStats.TreesPerPlot * sgSamp1);

                     if (sgSamp1 > sgSamp2Stage1) sgSamp2Stage1 = sgSamp1;

                     sgCalcError = cStat.getSampleError(thisSgStats.CV1, sgSamp1, 0);
                  }
                  else if (stage == 10)
                  {
                     sgSamp1 = Convert.ToInt32(thisSgStats.TreesPerAcre * thisStrStats.TotalAcres);
                     sgSamp2 = 0;
                     sgCalcError = 0;
                  }
                  else
                  {
                     sgCV = thisSgStats.CV2 * (thisSgStats.VolumePerAcre / thisStrStats.VolumePerAcre);
                     if (thisStrStats.WeightedCV2 <= 0) sgSamp2 = 3;
                     else sgSamp2 = Convert.ToInt32((sgCV / thisStrStats.WeightedCV2) * strSamp2);
                     if (sgSamp2 < 3) sgSamp2 = 3;
                     sgCV = thisSgStats.CV1 * (thisSgStats.VolumePerAcre / thisStrStats.VolumePerAcre);
                     if (thisStrStats.WeightedCV1 <= 0) sgSamp1 = 3;
                     else sgSamp1 = Convert.ToInt32((sgCV / thisStrStats.WeightedCV1) * strSamp1);
                     if (sgSamp1 < 3) sgSamp1 = 3;

                     if (stage == 12)
                     {
                        if (sgSamp2 > sgSamp1) sgSamp1 = sgSamp2;
                     }
                     else
                     {
                        if (sgSamp1 > sgSamp2Stage1) sgSamp2Stage1 = sgSamp1;
                     }

                     thisSgStats.SampleSize1 = sgSamp1;
                     thisSgStats.SampleSize2 = sgSamp2;
                     sgCalcError = cStat.getTwoStageError(thisSgStats.CV1, thisSgStats.CV2, sgSamp1, sgSamp2);
                  }
                  thisSgStats.SampleSize1 = sgSamp1;
                  thisSgStats.SampleSize2 = sgSamp2;
                  // update errors, frequency, KZ, BigBAF
                  if (thisStrStats.Method == "STR")
                  {
                     thisSgStats.SamplingFrequency = Convert.ToInt32((thisSgStats.TreesPerAcre * thisStrStats.TotalAcres) / sgSamp1);
                     strSample1 += sgSamp1;
                  }
                  else if (thisStrStats.Method == "S3P")
                  {
                     thisSgStats.SamplingFrequency = Convert.ToInt32((thisSgStats.TreesPerAcre * thisStrStats.TotalAcres) / sgSamp1);
                     strSample1 += sgSamp1;
                     strSample2 += sgSamp2;
                  }
                  else if (thisStrStats.Method == "100")
                  {
                     thisSgStats.SamplingFrequency = 1;
                     strSample1 += sgSamp1;
                  }
                  else if (thisStrStats.Method == "3P")
                  {
                     thisSgStats.KZ = Convert.ToInt32((thisSgStats.VolumePerAcre * thisStrStats.TotalAcres) / sgSamp1);
                     strSample1 += sgSamp1;
                  }
                  else if (thisStrStats.Method == "S3P")
                  {
                     if (thisSgStats.TreesPerAcre > 0)
                        thisSgStats.KZ = Convert.ToInt32(((thisSgStats.VolumePerAcre / thisSgStats.TreesPerAcre * sgSamp1) / sgSamp2));
                     else
                        thisSgStats.KZ = 1;
                     strSample1 += sgSamp1;
                     strSample2 += sgSamp2;
                  }
                  else if (thisStrStats.Method == "PNT" || thisStrStats.Method == "FIX")
                  {
                     strSample1 = sgSamp1;
                     strSample2 = sgSamp2;

                  }
                  // calc sg error
                  if (stage != 22)
                  {
                     if (stage == 11)
                     {
                        thisSgStats.SgError = Convert.ToSingle(Math.Round(cStat.getSampleError(thisSgStats.CV1, sgSamp1, 0, sgN), 2));
                     }
                     else if (stage == 21)
                     {
                        thisSgStats.SgError = Convert.ToSingle(Math.Round(cStat.getSampleError(thisSgStats.CV1, sgSamp1, 0), 2));
                     }
                     else if (stage == 12 || stage == 22)
                     {
                        thisSgStats.SgError = Convert.ToSingle(Math.Round(cStat.getTwoStageError(thisSgStats.CV1, thisSgStats.CV2, sgSamp1, sgSamp2), 2));
                     }
                     // calc combined stratum error
                     wtErr += (double)Math.Pow((thisSgStats.SgError * (thisSgStats.VolumePerAcre * thisStrStats.TotalAcres)), 2);
                  }
                  thisSgStats.Save();
               }
               if (stage > 20)
               {
                  wtErr = 0;
                  //loop back through
                  foreach (SampleGroupStatsDO thisSgStats in mySgStats)
                  {
                     // set each sample size to sgSamp2Stage
                     strSample1 = sgSamp2Stage1;

                     sgSamp2Stage2 = thisSgStats.SampleSize2;
                     strSample2 += sgSamp2Stage2;
                     // set frequencies
                     if (thisStrStats.Method == "FCM")
                     {
                        thisSgStats.SamplingFrequency = Convert.ToInt32((thisSgStats.TreesPerPlot * sgSamp2Stage1) / sgSamp2Stage2);
                     }
                     else if (thisStrStats.Method == "PCM")
                     {
                        thisSgStats.SamplingFrequency = Convert.ToInt32((thisSgStats.TreesPerPlot * sgSamp2Stage1) / sgSamp2Stage2);
                        thisSgStats.BigBAF = Convert.ToSingle(thisSgStats.SamplingFrequency * thisStrStats.BasalAreaFactor);
                     }
                     else if (thisStrStats.Method == "P3P")
                     {
                        thisSgStats.KZ = Convert.ToInt32((thisSgStats.TreesPerPlot * sgSamp2Stage1 * thisSgStats.AverageHeight) / sgSamp2Stage2);
                     }
                     else if (thisStrStats.Method == "F3P" || thisStrStats.Method == "3PPNT")
                     {
                        if (thisSgStats.TreesPerAcre > 0)
                           thisSgStats.KZ = Convert.ToInt32((thisSgStats.TreesPerPlot * sgSamp2Stage1 * (thisSgStats.VolumePerAcre / thisSgStats.TreesPerAcre)) / sgSamp2Stage2);
                        else
                           thisSgStats.KZ = 1;
                     }
                     thisSgStats.SampleSize1 = sgSamp2Stage1;
                     if (stage == 22)
                        thisSgStats.SgError = Convert.ToSingle(Math.Round(cStat.getTwoStageError(thisSgStats.CV1, thisSgStats.CV2, sgSamp2Stage1, sgSamp2Stage2), 2));
                     else
                        thisSgStats.SgError = Convert.ToSingle(Math.Round(cStat.getSampleError(thisSgStats.CV1, sgSamp2Stage1, 0), 2));

                     // calc combined stratum error
                     thisSgStats.Save();
                     wtErr += (double)Math.Pow((thisSgStats.SgError * (thisSgStats.VolumePerAcre * thisStrStats.TotalAcres)), 2);
                  }
               }

               // Update Stratum
               if (mySale.DefaultUOM == "01")
                  thisStrStats.StrError = (float)(Math.Sqrt(wtErr) / (tStrVol));
               else
                  thisStrStats.StrError = (float)(Math.Sqrt(wtErr) / (tStrVol));

               thisStrStats.SampleSize1 = strSample1;
               thisStrStats.SampleSize2 = strSample2;
               if (stage > 20)
               {
                  thisStrStats.PlotSpacing = (int)Math.Floor(Math.Sqrt((thisStrStats.TotalAcres * 43560) / strSample1));
               }
               thisStrStats.Save();
               sumError += (double)Math.Pow((tStrVol * thisStrStats.StrError), 2);
               sumVolume += tStrVol;
            }
         }
         // Update Sale Error and Cost
         getSaleError();
         //double saleError = Math.Sqrt(sumError) / sumVolume;
         //textBoxError.Text = (Math.Round(saleError, 2)).ToString();
         //textBoxVolume.Text = (Math.Round(tVolume, 0)).ToString();
         
         Cursor.Current = this.Cursor;
         return;

      }
      private bool checkForErrors()
      {
         string sMsg = "Errors Found \n\n";
         // loop through all sample groups
         foreach (StratumStatsDO thisStrStats in cdStratumStats)
         {
            int errCnt = 0;
            if(thisStrStats.Method != "FIXCNT")
            { 
 
               List<SampleGroupStatsDO> mySgStats = cdDAL.From<SampleGroupStatsDO>().Where("StratumStats_CN = @p1").Read(thisStrStats.StratumStats_CN).ToList();
               foreach (SampleGroupStatsDO thisSgStats in mySgStats)
               {
                  // check for CV1 > 0
                  if (thisSgStats.CV1 <= 0)
                  {
                     errCnt++;
                     sMsg += "Missing CV1: Stratum ";
                     sMsg += thisStrStats.Code;
                     sMsg += " Sample Group ";
                     sMsg += thisSgStats.Code;
                     sMsg += "\n";
                  }
                  // check for Trees/Acre > 0
                  if (thisSgStats.TreesPerAcre <= 0)
                  {
                     errCnt++;
                     sMsg += "Missing Trees/Acre: Stratum ";
                     sMsg += thisStrStats.Code;
                     sMsg += " Sample Group ";
                     sMsg += thisSgStats.Code;
                     sMsg += "\n";
                  }
                  // check for Volume/Acre > 0
                  if (thisSgStats.VolumePerAcre <= 0)
                  {
                     errCnt++;
                     sMsg += "Missing VolumePerAcre: Stratum ";
                     sMsg += thisStrStats.Code;
                     sMsg += " Sample Group ";
                     sMsg += thisSgStats.Code;
                     sMsg += "\n";
                  }
                  // if plot cruise
                  if (thisStrStats.Method == "PCM" || thisStrStats.Method == "P3P" || thisStrStats.Method == "F3P" || thisStrStats.Method == "FCM")
                  {
                     // check for Trees/Plot > 0
                     if (thisSgStats.TreesPerPlot <= 0)
                     {
                        errCnt++;
                        sMsg += "Missing TreesPerPlot: Stratum ";
                        sMsg += thisStrStats.Code;
                        sMsg += " Sample Group ";
                        sMsg += thisSgStats.Code;
                        sMsg += "\n";
                     }
                     // check for CV2 > 0
                     if (thisSgStats.CV2 <= 0)
                     {
                        errCnt++;
                        sMsg += "Missing CV2: Stratum ";
                        sMsg += thisStrStats.Code;
                        sMsg += " Sample Group ";
                        sMsg += thisSgStats.Code;
                        sMsg += "\n";
                     }
                  }
               }
            }
            if (errCnt > 0)
            {
               MessageBox.Show(sMsg, "Errors Found");
               return (true);
            }
         }          
         return (false);
      }
      private bool checkPlotSz()
      {
         string sMsg = "Errors Found \n\n";
         // loop through all sample groups
         int errCnt = 0;
         foreach (StratumStatsDO thisStrStats in cdStratumStats)
         {
            if (thisStrStats.Method == "PCM" || thisStrStats.Method == "P3P" || thisStrStats.Method == "PNT" || thisStrStats.Method == "3PPNT")
            {
               if (thisStrStats.BasalAreaFactor <= 0)
               {
                  errCnt++;
                  sMsg += "Missing BAF: Stratum ";
                  sMsg += thisStrStats.Code;
                  sMsg += "\n";
               }
            }
            else if (thisStrStats.Method == "FIX" || thisStrStats.Method == "F3P" || thisStrStats.Method == "FCM")
            {
               if (thisStrStats.FixedPlotSize <= 0)
               {
                  errCnt++;
                  sMsg += "Missing FPS: Stratum ";
                  sMsg += thisStrStats.Code;
                  sMsg += "\n";
               }
            }
         }
         if (errCnt > 0)
         {
            MessageBox.Show(sMsg, "Errors Found\nPlease correct before continuing.");
            return (true);
         }
         return (false);
      }

      private void buttonReport_Click(object sender, EventArgs e)
      {
         if (checkPlotSz())
            return;
         // instance of report
         Reports.ReportForm reportForm = new CruiseDesign.Reports.ReportForm(cdStratumStats.Count());
         //Reports.ReportViewer reportForm = new CruiseDesign.Reports.ReportViewer();
         // create sale table
         //mySale = cdDAL.ReadSingleRow<SaleDO>("Sale", null, null);
         reportForm.setSaleInfo(mySale.SaleNumber, mySale.Name, mySale.Region, mySale.Forest, mySale.District, textBoxError.Text, textBoxCost.Text, textBoxVolume.Text, mySale.DefaultUOM);
         string reportDescrip = mySale.Name;
         // loop by stratum
         foreach (StratumStatsDO myStr in cdStratumStats)
         {
            ///create sale table
            string strError = (Math.Round(myStr.StrError, 2)).ToString();
            string totVol = (Math.Round(myStr.TotalVolume, 2)).ToString();
            string baf = (Math.Round(myStr.BasalAreaFactor, 0)).ToString();
            string fps = (Math.Round(myStr.FixedPlotSize, 0)).ToString();
            string pSpacing = myStr.PlotSpacing.ToString();
            string tAcres = (Math.Round(myStr.TotalAcres, 1)).ToString();
            myStr.Stratum.CuttingUnits.Populate();
            string units = "";
            foreach (CuttingUnitDO myCU in myStr.Stratum.CuttingUnits)
               units = units + myCU.Code + ", ";

            reportForm.createStratumTable(myStr.Code, myStr.Method, strError, totVol, myStr.Description, baf, fps, pSpacing,tAcres,units);
            // create sample group table
            reportDescrip += ";  " + myStr.Code + " = " + myStr.Method;
            List<SampleGroupStatsDO> mySgStats = cdDAL.From<SampleGroupStatsDO>().Where("StratumStats_CN = @p1").Read(myStr.StratumStats_CN).ToList();
            int sgCnt = mySgStats.Count();
            int rowcnt = 0;
            switch (myStr.Method)
            {
               case "100":
               case "PNT":
               case "FIX":
                  reportForm.createSgTable_100(sgCnt,myStr.Method);
                  break;
               case "STR":
               case "3P":
                  reportForm.createSgTable_str(sgCnt,myStr.Method);
                  break;
               case "S3P":
                  reportForm.createSgTable_s3p(sgCnt);
                  break;
               default:
                  reportForm.createSgTable_pcm(sgCnt,myStr.Method);
                  break;
            }

            foreach (SampleGroupStatsDO thisSg in mySgStats)
            {
               string sgErr = (Math.Round(thisSg.SgError, 2)).ToString();
               string size1 = thisSg.SampleSize1.ToString();
               string size2 = thisSg.SampleSize2.ToString();
               string freq = thisSg.SamplingFrequency.ToString();
               string kz = thisSg.KZ.ToString();
               string insur = thisSg.InsuranceFrequency.ToString();
               string cv1 = (Math.Round(thisSg.CV1, 3)).ToString();
               string cv2 = (Math.Round(thisSg.CV2, 3)).ToString();
               string tpa = (Math.Round(thisSg.TreesPerAcre, 1)).ToString();
               string vpa = (Math.Round(thisSg.VolumePerAcre, 1)).ToString();
               rowcnt++;
               switch (myStr.Method)
               {
                  case "100":
                  case "PNT":
                  case "FIX":
                     reportForm.createAddSgRow_100(rowcnt,thisSg.Code,sgErr,size1,cv1,tpa,vpa,thisSg.Description);
                     break;
                  case "STR":
                     reportForm.createAddSgRow_str(rowcnt, thisSg.Code, sgErr, size1, freq, insur, cv1, tpa, vpa, thisSg.Description);
                     break;
                  case "3P":
                     reportForm.createAddSgRow_str(rowcnt, thisSg.Code, sgErr, size1, kz, insur, cv1, tpa, vpa, thisSg.Description);
                     break;
                  case "S3P":
                     reportForm.createAddSgRow_s3p(rowcnt, thisSg.Code, sgErr, size1, size2, freq, kz, insur, cv1, cv2, tpa, vpa, thisSg.Description);
                     break;
                  case "F3P":
                  case "P3P":
                  case "3PPNT":
                     reportForm.createAddSgRow_pcm(rowcnt, thisSg.Code, sgErr, size1, size2, kz, cv1, cv2, tpa, vpa, thisSg.Description);
                     break;
                  default:
                     reportForm.createAddSgRow_pcm(rowcnt, thisSg.Code, sgErr, size1, size2, freq, cv1, cv2, tpa, vpa, thisSg.Description);
                     break;
               }
            }
            reportForm.AddTable2();
         }
         reportForm.textBoxTitle.Text = reportDescrip;
         reportForm.ShowDialog();

      }
      private void getCostData()
      {
         if (myGlobals.Count == 0 || myGlobals == null)
         {
            _cData.crewCost = 50;
            _cData.crewSize = 3;
            _cData.costPaint = 15;
            _cData.paintTrees = 35;
            _cData.travelTime = 30;
            _cData.timeTree = 5;
            _cData.timePlot = 10;
            _cData.walkRate = 130;
         }
         else
         {
            foreach (GlobalsDO gDO in myGlobals)
            {
               switch (gDO.Key)
               {
                  case "CrewCost":
                     _cData.crewCost = Convert.ToInt32(gDO.Value);
                     break;
                  case "CrewSize":
                     _cData.crewSize = Convert.ToInt32(gDO.Value);
                     break;
                  case "PaintCost":
                     _cData.costPaint = Convert.ToInt32(gDO.Value);
                     break;
                  case "PaintTrees":
                     _cData.paintTrees = Convert.ToInt32(gDO.Value);
                     break;
                  case "TravelTime":
                     _cData.travelTime = Convert.ToInt32(gDO.Value);
                     break;
                  case "TimeTree":
                     _cData.timeTree = Convert.ToInt32(gDO.Value);
                     break;
                  case "TimePlot":
                     _cData.timePlot = Convert.ToInt32(gDO.Value);
                     break;
                  case "WalkRate":
                     _cData.walkRate = Convert.ToInt32(gDO.Value);
                     break;
               }
            }
         }
      }


      private void buttonFScruiser_Click(object sender, EventArgs e)
      {
         // check for errors
         if (checkPlotSz())
            return;

         CreateProduction pDlg = new CreateProduction(this);

         pDlg.ShowDialog();

         if(pDlg.rDAL != null)
            pDlg.rDAL.Dispose();
         if (pDlg.fsDAL != null)
            pDlg.fsDAL.Dispose();
       }



      #region ContextMenus
      // stratum
      private void loadDesignToolStripMenuItem_Click(object sender, EventArgs e)
      {
         MessageBox.Show("Not available at this time");
         //popup showing list of file descriptions for current folder (*.designsave)
         //close current file (dispose DAL)
         //copy selected file name to current file name
         //create new cdDAL, setup new binding lists
      }

      private void saveDesignToolStripMenuItem_Click(object sender, EventArgs e)
      {
         // popup asking for file description
         DesignSaveForm sDlg = new DesignSaveForm();
         if (sDlg.ShowDialog() == DialogResult.OK)
         {
            MessageBox.Show("Not implemented at this time.");
            
         }

         // copy current file to new file with .designsave extension
      }
      // sample group
      private void saveTreesAndVolumePerAcreToolStripMenuItem_Click(object sender, EventArgs e)
      {
         Cursor.Current = Cursors.WaitCursor;
         // for each sample group
         foreach (SampleGroupStatsDO thisSgStats in cdSgStats)
         {
            // get tpa and vpa
            float vpa = thisSgStats.VolumePerAcre;
            float tpa = thisSgStats.TreesPerAcre;

                // sql command selecting all samplegroups where Stratum_CN and SgSet and code from current Sg
//                List<SampleGroupStatsDO> _SgStats = new List<SampleGroupStatsDO>(cdDAL.Read<SampleGroupStatsDO>("SampleGroupStats", " JOIN StratumStats ON SampleGroupStats.StratumStats_CN = StratumStats.StratumStats_CN WHERE StratumStats.Stratum_CN = ? AND SampleGroupStats.SgSet = ? AND SampleGroupStats.Code = ?", thisSgStats.StratumStats.Stratum_CN, thisSgStats.SgSet, thisSgStats.Code));
            List<SampleGroupStatsDO> _SgStats = new List<SampleGroupStatsDO>(cdDAL.From<SampleGroupStatsDO>().Join("StratumStats AS ss","USING (StratumStats_CN)")
                                                                             .Where("ss.Stratum_CN = @p1 AND SampleGroupStats.SgSet = @p2 AND SampleGroupStats.Code = @p3")
                                                                             .Read(thisSgStats.StratumStats.Stratum_CN, thisSgStats.SgSet, thisSgStats.Code).ToList());
                // for each set tpa and vpa
            foreach (SampleGroupStatsDO _mySgStats in _SgStats)
            {
               _mySgStats.TreesPerAcre = tpa;
               _mySgStats.VolumePerAcre = vpa;
               _mySgStats.TPA_Def = (long)(tpa * totAcres);
               _mySgStats.VPA_Def = (long)(vpa * totAcres);
               // save
               _mySgStats.Save();
            }
         }
         Cursor.Current = this.Cursor;
         MessageBox.Show("Done");
      }

      private void saveTreesPerPlotToolStripMenuItem_Click(object sender, EventArgs e)
      {
         Cursor.Current = Cursors.WaitCursor;
         // for each sample group
         foreach (SampleGroupStatsDO thisSgStats in cdSgStats)
         {
            // get tpa and vpa
            float tpp = thisSgStats.TreesPerPlot;

            // sql command selecting all samplegroups where Stratum_CN and SgSet and code from current Sg
//            List<SampleGroupStatsDO> _SgStats = new List<SampleGroupStatsDO>(cdDAL.Read<SampleGroupStatsDO>("SampleGroupStats", " JOIN StratumStats ON SampleGroupStats.StratumStats_CN = StratumStats.StratumStats_CN WHERE StratumStats.Stratum_CN = ? AND SampleGroupStats.SgSet = ? AND SampleGroupStats.Code = ?", thisSgStats.StratumStats.Stratum_CN, thisSgStats.SgSet, thisSgStats.Code));
              List<SampleGroupStatsDO> _SgStats = cdDAL.From<SampleGroupStatsDO>().Join("StratumStats AS ss", "USING (StratumStats_CN")
                                                                                 .Where("ss.Stratum_CN = @p1 AND SampleGroupStats.SgSet = @p2 AND SampleGroupStats.Code = @p3")
                                                                                 .Read(thisSgStats.StratumStats.Stratum_CN, thisSgStats.SgSet, thisSgStats.Code).ToList();
                // for each set tpa and vpa 
                foreach (SampleGroupStatsDO _mySgStats in _SgStats)
            {
               if (thisSgStats.StratumStats.Method == "FIX" || thisSgStats.StratumStats.Method == "FCM" || thisSgStats.StratumStats.Method == "F3P")
               {
                  if (_mySgStats.StratumStats.Method == "FIX" || _mySgStats.StratumStats.Method == "FCM" || _mySgStats.StratumStats.Method == "F3P")
                     _mySgStats.TreesPerPlot = tpp;
               }
               else if (thisSgStats.StratumStats.Method == "PNT" || thisSgStats.StratumStats.Method == "PCM" || thisSgStats.StratumStats.Method == "P3P" || thisSgStats.StratumStats.Method == "3PPNT")
               {
                  if (_mySgStats.StratumStats.Method == "PNT" || _mySgStats.StratumStats.Method == "PCM" || _mySgStats.StratumStats.Method == "P3P" || _mySgStats.StratumStats.Method == "3PPNT")
                     _mySgStats.TreesPerPlot = tpp;
               }

               // save
               _mySgStats.Save();
            }
         }
         Cursor.Current = this.Cursor;
         MessageBox.Show("Done");
      }

      private void recalculateErrorsToolStripMenuItem_Click(object sender, EventArgs e)
      {
         MessageBox.Show("Not available at this time");
         // pop-up form asking for desired stratum error
         // optimize for stratum error
         // for each sample group, determine the weighted cv
         // find stratum sample size
         // prorate samples back sample group
         // check floating t-value
         // minimum number of samples

      }
      #endregion

    }
}
