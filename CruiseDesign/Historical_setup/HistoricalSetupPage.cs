using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using CruiseDAL;
using CruiseDAL.DataObjects;

namespace CruiseDesign.Historical_setup
{
   public partial class HistoricalSetupPage : UserControl
   {
      //private int rowCount;
      private int n1, n2, stage, measTrees, sumKpi, talliedTrees, freq, kz;
      private double st1x, st1x2, st2x, st2x2, cv1, cv2, sampErr, sumExpFac, sumNetVol;
      string strDescrip;
      float totalAcres;

      public ArrayList sgVolume = new ArrayList();
      public ArrayList sgError = new ArrayList();
      public ArrayList sgCV = new ArrayList();

      public HistoricalSetupPage(HistoricalSetupWizard Owner)
      {
         this.Owner = Owner;
         InitializeComponent();

         //InitializeDataBindings();
      }

      #region Intialize
      //public DAL hDAL { get; set; }


      public TreeDefaultValueDO newTreeDefaults;
      
      public HistoricalSetupWizard Owner { get; set; }
     

      private void InitializeDataBindings()
      {
          // bindingSourceCurrentStratumStats.DataSource = Owner.currentStratumStats;
         bindingSourceTDV.DataSource = Owner.histTreeDefaults;
         bindingSourceSG.DataSource = Owner.histSampleGroup;
         bindingSourceStratum.DataSource = Owner.histStratum;
      }

      #endregion

      #region Methods


      #endregion
        

      #region Click Events

      private void buttonFile_Click(object sender, EventArgs e)
      {
         if (openFileDialog1.ShowDialog() == DialogResult.OK)
         {
            Owner.histFile = openFileDialog1.FileName;
            //set title bar with file name
            textBoxFile.Text = openFileDialog1.SafeFileName;
            //open new cruise DAL
            if (Owner.histFile.Length > 0)
            {
               try
               {
                  Owner.hDAL = new DAL(Owner.histFile);
               }
               catch (System.IO.IOException ie)
               {
                  //Logger.Log.E(ie);
               }
               catch (System.Exception ie)
               {
                  //Logger.Log.E(ie);
               }
            }
            else
            {
               return;
            }
            Owner.Sale = new SaleDO(Owner.hDAL.From<SaleDO>().Read().FirstOrDefault());
            string sUOM = Owner.Sale.DefaultUOM;
            if (sUOM != Owner.UOM)
            {
               MessageBox.Show("Cruise does not have same UOM.\nCannot import data.", "Warning");
               return;
            }
                //set binding list for stratum
                //            Owner.histStratum = new BindingList<StratumDO>(Owner.hDAL.Read<StratumDO>("Stratum", null, null));
                Owner.histStratum = new BindingList<StratumDO>(Owner.hDAL.From<StratumDO>().Read().ToList());
                bindingSourceStratum.DataSource = Owner.histStratum;
         }
      }
      private void buttonNext_Click(object sender, EventArgs e)
      {
         //check for selected stratum
         //rowCount = selectedItemsGridViewStratum.SelectedItems.Count;
         if (Owner.selectedStratum.Code.Length <= 0)
         {
            MessageBox.Show("Please select a stratum.", "Information");
            return;
         }

         // create historical data
         WaitForm waitFrm = new WaitForm();

         Cursor.Current = Cursors.WaitCursor;
         waitFrm.Show();

         createHistoricalData();
         
         waitFrm.Close();
         Cursor.Current = this.Cursor;


         Owner.GoToUnitPage();
      }

      private void buttonCancel_Click(object sender, EventArgs e)
      {
         Owner.GoToUnitPage();
      }
      #endregion

      private void bindingSourceStratum_CurrentChanged(object sender, EventArgs e)
      {
         Owner.selectedStratum = bindingSourceStratum.Current as StratumDO;

            // Owner.histSampleGroup = new BindingList<SampleGroupDO>(Owner.hDAL.Read<SampleGroupDO>("SampleGroup", "Where Stratum_CN = ?", Owner.selectedStratum.Stratum_CN));
            Owner.histSampleGroup = new BindingList<SampleGroupDO>(Owner.hDAL.From<SampleGroupDO>().Where("Stratum_CN = @p1").Read(Owner.selectedStratum.Stratum_CN).ToList());
          
            bindingSourceSG.DataSource = Owner.histSampleGroup;
      }

//*************************************************************************
      private void bindingSourceSG_CurrentChanged(object sender, EventArgs e)
      {
         // display the TDV selected items
         Owner.selectedSampleGroup = bindingSourceSG.Current as SampleGroupDO;
         if (Owner.selectedSampleGroup != null)
         {
            Owner.selectedSampleGroup.TreeDefaultValues.Populate();
            bindingSourceTDV.DataSource = Owner.selectedSampleGroup.TreeDefaultValues;
         }
      }

//*************************************************************************
      private void createHistoricalData()
      {
         strDescrip = Owner.selectedStratum.Method;
         strDescrip += " - ";
         strDescrip += textBoxFile.Text;

         // copy stratum row to design
         Owner.currentStratum.BasalAreaFactor = Owner.selectedStratum.BasalAreaFactor;
         Owner.currentStratum.Method = Owner.selectedStratum.Method;
         Owner.currentStratum.FixedPlotSize = Owner.selectedStratum.FixedPlotSize;
         Owner.currentStratum.FBSCode = Owner.selectedStratum.FBSCode;
         Owner.currentStratum.Description = strDescrip;
         Owner.currentStratum.Save();
         
         // copy stratumstats to design
//         Owner.currentStratumStats = new StratumStatsDO(Owner.cdDAL);
//         Owner.currentStratumStats.Stratum = Owner.currentStratum;
         Owner.currentStratumStats.Method = Owner.currentStratum.Method;
         Owner.currentStratumStats.BasalAreaFactor = Owner.selectedStratum.BasalAreaFactor;
         Owner.currentStratumStats.FixedPlotSize = Owner.selectedStratum.FixedPlotSize;
         Owner.currentStratumStats.SgSetDescription = "Comparison Cruise";
         Owner.currentStratumStats.Used = 1;
         Owner.currentStratumStats.Save();
         //check for tree vs plot and one vs two stage methods
         
         getTotalAcres();
         
         getSampleGroupStats();
         
         getStratumStats(Owner.currentStratumStats.StratumStats_CN);
      }
//*************************************************************************
      private void getTotalAcres()
      {
         
         Owner.selectedStratum.CuttingUnits.Populate();
         // get total strata acres

         totalAcres = 0;
         foreach (CuttingUnitDO cu in Owner.selectedStratum.CuttingUnits)
         {
            float acres = cu.Area;
            totalAcres += acres;
         }
      }

//*************************************************************************
      private void getSampleGroupStats()
      {
         calcStats statClass = new calcStats();
         stage = statClass.isTwoStage(Owner.currentStratum.Method);
         
         double comberr2 = 0;
         double totalVolume = 0;
         // for each sample group
         foreach (SampleGroupDO sg in Owner.histSampleGroup)
         {
            // create samplegroupstats
            Owner.currentSgStats = new SampleGroupStatsDO(Owner.cdDAL);
            //set foriegn key
            Owner.currentSgStats.StratumStats = Owner.currentStratumStats;
            Owner.currentSgStats.SgSet = 1;
            Owner.currentSgStats.Code = sg.Code;
            Owner.currentSgStats.CutLeave = sg.CutLeave;
            Owner.currentSgStats.UOM = sg.UOM;
            Owner.currentSgStats.PrimaryProduct = sg.PrimaryProduct;
            Owner.currentSgStats.SecondaryProduct = sg.SecondaryProduct;
            Owner.currentSgStats.DefaultLiveDead = sg.DefaultLiveDead;
            Owner.currentSgStats.Description = sg.Description;
            // get POP data
            //Owner.selectedPOP = new POPDO(Owner.hDAL);
            Owner.selectedPOP = Owner.hDAL.From<POPDO>().Where("Stratum = @p1 AND SampleGroup = @p2").Read(Owner.selectedStratum.Code, sg.Code).FirstOrDefault();
            // calculate statistics (based on method)
            if (Owner.selectedPOP == null)
            {
               MessageBox.Show("Cruise Not Processed. Cannot Import Data.", "Warning");
               return;
            }
            n1 = Convert.ToInt32(Owner.selectedPOP.StageOneSamples);
            n2 = Convert.ToInt32(Owner.selectedPOP.StageTwoSamples);
            measTrees = Convert.ToInt32(Owner.selectedPOP.MeasuredTrees);
            talliedTrees = Convert.ToInt32(Owner.selectedPOP.TalliedTrees);
            sumKpi = Convert.ToInt32(Owner.selectedPOP.SumKPI);
            st1x = Owner.selectedPOP.Stg1NetXPP;
            st1x2 = Owner.selectedPOP.Stg1NetXsqrdPP;
            st2x = Owner.selectedPOP.Stg2NetXPP;
            st2x2 = Owner.selectedPOP.Stg2NetXsqrdPP;
            // trees per plot
            // find CVs
            cv1 = statClass.getCV(st1x, st1x2, n1);
            if (stage == 12 || stage == 22)
               cv2 = statClass.getCV(st2x, st2x2, n2);
            else
               cv2 = 0;
            // find errors stage 11=tree,single 12=tree,2 stage 21=plot,single 22=plot,2 stage
            if (stage == 11 || stage == 21)
               sampErr = statClass.getSampleError(cv1, n1, 0);
            else if (stage == 12 || stage == 22)
               sampErr = statClass.getTwoStageError(cv1, cv2, n1, n2);
            else
               sampErr = 0;
            Owner.currentSgStats.CV1 = Convert.ToSingle(cv1);
            Owner.currentSgStats.CV2 = Convert.ToSingle(cv2);
            Owner.currentSgStats.SampleSize1 = n1;
            Owner.currentSgStats.SampleSize2 = n2;
            // calculate frequency
            
            Owner.currentSgStats.SgError = Convert.ToSingle(sampErr);

                // get LCD data
                //Owner.selectedLCD = Owner.hDAL.Read<LCDDO>("LCD", "WHERE Stratum = ? AND SampleGroup = ?", Owner.selectedStratum.Code, sg.Code);
            Owner.selectedLCD = Owner.hDAL.From<LCDDO>().Where("Stratum = @p1 AND SampleGroup = @p2").Read(Owner.selectedStratum.Code, sg.Code).ToList();
            sumExpFac = 0;
            sumNetVol = 0;
            //foreach (SampleGroupDO sg in Owner.histSampleGroup)
            foreach (LCDDO lcd in Owner.selectedLCD)
            {
               // sum volume
               double expFac = lcd.SumExpanFactor;
               sumExpFac += expFac;
               // sum trees
               double netVol = lcd.SumNCUFT;
               sumNetVol += netVol;
            }
            comberr2 += (sampErr * sumNetVol) * (sampErr * sumNetVol);
            totalVolume += sumNetVol;
            // find volume/acre and trees/acre
            if (stage < 20)
            {
               Owner.currentSgStats.TreesPerAcre = Convert.ToSingle(Math.Round((sumExpFac / totalAcres), 2));
               Owner.currentSgStats.VolumePerAcre = Convert.ToSingle(Math.Round((sumNetVol / totalAcres), 2));
            }
            else 
            {
               Owner.currentSgStats.TreesPerAcre = Convert.ToSingle(Math.Round((sumExpFac), 2));
               Owner.currentSgStats.VolumePerAcre = Convert.ToSingle(Math.Round((sumNetVol), 2));
               if(stage == 21)   
                  Owner.currentSgStats.TreesPerPlot = Convert.ToSingle(Math.Round((Convert.ToSingle((float)measTrees / (float)n1)), 1));
               else
                  Owner.currentSgStats.TreesPerPlot = Convert.ToSingle(Math.Round((Convert.ToSingle((float)talliedTrees / (float) n1)), 1));
            }
            // find frequency/KZ/BigBAF values
            if ((stage == 11 || stage == 12 || stage == 22) && measTrees > 0)
            {
               freq = Convert.ToInt32((talliedTrees / measTrees));
               kz = Convert.ToInt32((sumKpi / measTrees));
               
               if (Owner.currentStratum.Method == "S3P")
               {
                  Owner.currentSgStats.SamplingFrequency = Convert.ToInt32((talliedTrees / n1));
                  Owner.currentSgStats.KZ = kz;
               }
               else
               {
                  Owner.currentSgStats.SamplingFrequency = freq;
                  Owner.currentSgStats.KZ = kz;
               }
            }

            // save samplegroupstats row
            Owner.currentSgStats.Save();
            // loop through TDV information
            sg.TreeDefaultValues.Populate();
            foreach (TreeDefaultValueDO tdv in sg.TreeDefaultValues)
            {
               // check with current TDV values
               Owner.currentTreeDefaults = Owner.cdDAL.From<TreeDefaultValueDO>().Where("Species = @p1 AND PrimaryProduct = @p2 AND LiveDead = @p3")
                                    .Read(tdv.Species,tdv.PrimaryProduct,tdv.LiveDead).FirstOrDefault();
               if (Owner.currentTreeDefaults == null)
               {
                  // if null, add tdv to list then create link
                  TreeDefaultValueDO newTdv = new TreeDefaultValueDO(Owner.cdDAL);
                  newTdv.SetValues(tdv);
                  newTdv.Save();
                  Owner.currentSgStats.TreeDefaultValueStats.Add(newTdv);
               }
               else
               {
                  // if exists, create link
                  Owner.currentSgStats.TreeDefaultValueStats.Add(Owner.currentTreeDefaults);
               }
            }

            Owner.currentSgStats.Save();
            Owner.currentSgStats.TreeDefaultValueStats.Save();

         }
         // calculate stats for stratum
        // double stratumError = Math.Sqrt(comberr2) / totalVolume;
         // wted CVs
         //Owner.currentStratumStats.StrError = Convert.ToSingle(stratumError);
      
         //Owner.currentStratumStats.Save();

      }

//*************************************************************************
      public void getStratumStats(long? stratumStatsCN)
      {
         StratumStatsDO thisStrStats;
         List<SampleGroupStatsDO> mySgStats;
         float totalVolumeAcre, totalVolume, wtCV1, wtCv2, volumeAcre;
         float cv1, cv2, wtErr, sgErr, treesAcre;
         long sampleSize1, sampleSize2;
         string _UOM = "";
            //loop through SampleGroupStats

            //thisStrStats = (Owner.cdDAL.ReadSingleRow<StratumStatsDO>("StratumStats", "WHERE StratumStats_CN = ?", stratumStatsCN));
            thisStrStats = Owner.cdDAL.From<StratumStatsDO>().Where("StratumStats_CN = ?").Read(stratumStatsCN).FirstOrDefault();

            //mySgStats = new List<SampleGroupStatsDO>(Owner.cdDAL.Read<SampleGroupStatsDO>("SampleGroupStats", "Where StratumStats_CN = ? AND CutLeave = 'C'", stratumStatsCN));
            mySgStats = new List<SampleGroupStatsDO>(Owner.cdDAL.From<SampleGroupStatsDO>().Where("StratumStats_CN = @p1 AND CutLeave = 'C'").Read(stratumStatsCN).ToList());
            // loop through sample groups
            //totalVolumeAcre = getTotals(mySgStats);
            totalVolumeAcre = mySgStats.Sum(P => P.VolumePerAcre);
         totalVolume = totalVolumeAcre * thisStrStats.TotalAcres;
         treesAcre = 0;
         sampleSize1 = 0;
         sampleSize2 = 0;
         wtCV1 = 0;
         wtCv2 = 0;
         wtErr = 0;
         foreach (SampleGroupStatsDO thisSgStats in mySgStats)
         {
            //sum volumes, trees/acre, sample sizes
            treesAcre += thisSgStats.TreesPerAcre;
            sampleSize1 += thisSgStats.SampleSize1;
            sampleSize2 += thisSgStats.SampleSize2;
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

            wtErr += (float)Math.Pow((sgErr * (volumeAcre * thisStrStats.TotalAcres)), 2);
            _UOM = thisSgStats.UOM;
         }
         //save calculated values
         if (totalVolume > 0)
            thisStrStats.StrError = (float)Math.Sqrt(wtErr) / totalVolume;
         else
            thisStrStats.StrError = 0;
         thisStrStats.SampleSize1 = sampleSize1;
         thisStrStats.SampleSize2 = sampleSize2;
         thisStrStats.WeightedCV1 = wtCV1;
         thisStrStats.WeightedCV2 = wtCv2;
         thisStrStats.TreesPerAcre = treesAcre;
         thisStrStats.VolumePerAcre = totalVolumeAcre;
         //Uom Check
         if(_UOM == "01")
            thisStrStats.TotalVolume = (float)(totalVolume / 1000.0);
         else
            thisStrStats.TotalVolume = (float)(totalVolume / 100.0);
         if (thisStrStats.Method == "FIX" || thisStrStats.Method == "FCM" || thisStrStats.Method == "F3P" || thisStrStats.Method == "PNT" || thisStrStats.Method == "PCM" || thisStrStats.Method == "P3P" || thisStrStats.Method == "3PPNT")
         {
            if (sampleSize1 > 0)
               thisStrStats.PlotSpacing = (int)Math.Floor(Math.Sqrt((thisStrStats.TotalAcres * 43560) / sampleSize1));
         }
         else
         {
            if (thisStrStats.TreesPerAcre > 0)
               thisStrStats.PlotSpacing = (int)Math.Floor(Math.Sqrt((43560) / thisStrStats.TreesPerAcre));
         }

         thisStrStats.Save();
      }  
   }
}
