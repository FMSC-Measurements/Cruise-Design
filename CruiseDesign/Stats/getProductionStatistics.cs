using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CruiseDAL;
using CruiseDAL.DataObjects;
using System.Data;
using System.Collections;
using System.Windows.Forms;

namespace CruiseDesign.Stats
{
   class getProductionStatistics
   {
      //private float totalAcres;
      public double sumExpFac;
      public int stage;
      public calcStats statClass = new calcStats();
      public DAL cdDAL { get; set; }

      public SaleDO mySale { get; set; }
      public List<StratumDO> stratum;
      //public List<StratumStatsDO> stratumStats;
      //public List<SampleGroupStatsDO> sgStats;
      public List<TreeDO> myTrees;
      public List<SampleGroupDO> sampleGroups;
      public StratumStatsDO strStats;
      public SampleGroupStatsDO currentSgStats;
      public List<SampleGroupStatsTreeDefaultValueDO> sgtdv;
      public POPDO selectedPOP;
      public List<LCDDO> selectedLCD { get; set; } 

      public void getProductionStats(DAL cDAL, int err)
      {
         cdDAL = cDAL;

         // set up StratumStats, SampleGroupStats
         
         // get stratum definitions
         stratum = new List<StratumDO>(cdDAL.Read<StratumDO>("Stratum", null, null));
         removePopulations();

         // loop through stratum
         foreach (StratumDO curStr in stratum)
         {

            getPopulations(curStr);

         }
      }

      public int getPopulations(StratumDO currentStratum)
      {
         // for each stratum
         stage = statClass.isTwoStage(currentStratum.Method);
         strStats = new StratumStatsDO(cdDAL);
         strStats.Stratum = currentStratum;
         strStats.Code = currentStratum.Code;
         strStats.Description = currentStratum.Description;
         strStats.SgSet = 1;
         strStats.SgSetDescription = "";

         float totalAcres = 0;
         currentStratum.CuttingUnits.Populate();

         foreach (CuttingUnitDO cu in currentStratum.CuttingUnits)
         {
            float acres = cu.Area;
            totalAcres += acres;
         }

         strStats.TotalAcres = totalAcres;
         strStats.Method = currentStratum.Method;
         strStats.BasalAreaFactor = currentStratum.BasalAreaFactor;
         strStats.FixedPlotSize = currentStratum.FixedPlotSize;
         strStats.Used = 1;
         strStats.Save();

         if (getSampleGroupStats(currentStratum, strStats, totalAcres) < 0)
            return(-1);

         getStratumStats(strStats);
         // create stratum sets;
         // create sample group sets
         // calculate stats
         return (0);
      }

      
//---------------------------------------------------------------------------------------------------------      
      private int getSampleGroupStats(StratumDO currentStratum, StratumStatsDO currentStratumStats, float totalAcres)
      {
         int n1, n2, measTrees, sumKpi, talliedTrees, freq, kz, insTrees;
         double st1x, st1x2, st2x, st2x2, cv1, cv2, sampErr, sumNetVol;
         float treesPerAcre;
         double comberr2 = 0;
         double totalVolume = 0;
         // for each sample group
         sampleGroups = new List<SampleGroupDO>(cdDAL.Read<SampleGroupDO>("SampleGroup", "Where Stratum_CN = ?", currentStratum.Stratum_CN));

         foreach (SampleGroupDO sg in sampleGroups)
         {
            // create samplegroupstats
            currentSgStats = new SampleGroupStatsDO(cdDAL);
            //set foriegn key
            currentSgStats.StratumStats = currentStratumStats;
            currentSgStats.SgSet = 1;
            currentSgStats.Code = sg.Code;
            currentSgStats.CutLeave = sg.CutLeave;
            currentSgStats.UOM = sg.UOM;
            currentSgStats.PrimaryProduct = sg.PrimaryProduct;
            currentSgStats.SecondaryProduct = sg.SecondaryProduct;
            currentSgStats.DefaultLiveDead = sg.DefaultLiveDead;
            currentSgStats.Description = sg.Description;
            // get POP data
            selectedPOP = cdDAL.ReadSingleRow<POPDO>("POP", "WHERE Stratum = ? AND SampleGroup = ?", currentStratum.Code, sg.Code);
            
            // calculate statistics (based on method)
            if (selectedPOP == null)
            {
               MessageBox.Show("Cruise Not Processed. Please Process Cruise Before Continuing.", "Warning");
               return(-1);
            }
            n1 = Convert.ToInt32(selectedPOP.StageOneSamples);
            n2 = Convert.ToInt32(selectedPOP.StageTwoSamples);
            measTrees = Convert.ToInt32(selectedPOP.MeasuredTrees);
            talliedTrees = Convert.ToInt32(selectedPOP.TalliedTrees);
            sumKpi = Convert.ToInt32(selectedPOP.SumKPI);
            st1x = selectedPOP.Stg1NetXPP;
            st1x2 = selectedPOP.Stg1NetXsqrdPP;
            st2x = selectedPOP.Stg2NetXPP;
            st2x2 = selectedPOP.Stg2NetXsqrdPP;
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
            currentSgStats.CV1 = Convert.ToSingle(cv1);
            currentSgStats.CV2 = Convert.ToSingle(cv2);
            currentSgStats.SampleSize1 = n1;
            currentSgStats.SampleSize2 = n2;
            if (stage == 11 || stage == 10)
            {
               currentSgStats.ReconTrees = n1;
               currentSgStats.ReconPlots = 0;
            }
            else if (stage == 12)
            {
               currentSgStats.ReconPlots = n1;
               currentSgStats.ReconTrees = n2;
            }
            else if (stage == 21 || stage == 22)
            {
               currentSgStats.ReconTrees = measTrees;
               currentSgStats.ReconPlots = n1;
            }
            // calculate frequency

            currentSgStats.SgError = Convert.ToSingle(sampErr);

            // get LCD data
            selectedLCD = cdDAL.Read<LCDDO>("LCD", "WHERE Stratum = ? AND SampleGroup = ?", currentStratum.Code, sg.Code);
            sumExpFac = 0;
            sumNetVol = 0;
            //foreach (SampleGroupDO sg in Owner.histSampleGroup)
            foreach (LCDDO lcd in selectedLCD)
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
               treesPerAcre = Convert.ToSingle(Math.Round((sumExpFac / totalAcres), 2));
               currentSgStats.TreesPerAcre = treesPerAcre;
               currentSgStats.VolumePerAcre = Convert.ToSingle(Math.Round((sumNetVol / totalAcres), 2));
            }
            else
            {
               treesPerAcre = Convert.ToSingle(Math.Round((sumExpFac), 2));
               currentSgStats.TreesPerAcre = treesPerAcre;
               currentSgStats.VolumePerAcre = Convert.ToSingle(Math.Round((sumNetVol), 2));
               if (stage == 21)
                  currentSgStats.TreesPerPlot = Convert.ToSingle(Math.Round((Convert.ToSingle((float)measTrees / (float)n1)), 1));
               else
                  currentSgStats.TreesPerPlot = Convert.ToSingle(Math.Round((Convert.ToSingle((float)talliedTrees / (float)n1)), 1));
            }
            currentSgStats.TPA_Def = (long)(treesPerAcre * totalAcres);
            // find frequency/KZ/BigBAF values
            if ((stage == 11 || stage == 12 || stage == 22) && measTrees > 0)
            {
               freq = Convert.ToInt32((talliedTrees / measTrees));
               kz = Convert.ToInt32((sumKpi / measTrees));

               if (currentStratum.Method == "S3P")
               {
                  currentSgStats.SamplingFrequency = Convert.ToInt32((talliedTrees / n1));
                  currentSgStats.KZ = kz;
               }
               else
               {
                  currentSgStats.SamplingFrequency = freq;
                  currentSgStats.KZ = kz;
               }
            }
            // find insurance trees
            if (stage == 11 || stage == 12)
               insTrees = getInsuranceTrees(sg);
            else
               insTrees = 0;
            currentSgStats.InsuranceFrequency = insTrees;
            // save samplegroupstats row
            currentSgStats.Save();
            // loop through TDV information
            sg.TreeDefaultValues.Populate();
            foreach (TreeDefaultValueDO tdv in sg.TreeDefaultValues)
            {
                currentSgStats.TreeDefaultValueStats.Add(tdv);
            }

            currentSgStats.Save();
            currentSgStats.TreeDefaultValueStats.Save();
            
         }
         return (0);
      }
//*************************************************************************
      public int getInsuranceTrees(SampleGroupDO currentSg)
      {
         int insTrees;
         myTrees = new List<TreeDO>(cdDAL.Read<TreeDO>("Tree", "Where SampleGroup_CN = ? AND CountOrMeasure = ?", currentSg.SampleGroup_CN, "I"));

         insTrees = myTrees.Count();

         return (insTrees);
      }
//*************************************************************************
      public void getStratumStats(StratumStatsDO thisStrStats)
      {
         List<SampleGroupStatsDO> mySgStats;
         float totalVolumeAcre, totalVolume, wtCV1, wtCv2, volumeAcre;
         float cv1, cv2, wtErr, sgErr, treesAcre;
         long sampleSize1, sampleSize2;
         string _UOM = "";
         //loop through SampleGroupStats
         
         mySgStats = new List<SampleGroupStatsDO>(cdDAL.Read<SampleGroupStatsDO>("SampleGroupStats", "Where StratumStats_CN = ?", thisStrStats.StratumStats_CN));
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
            if(stage > 20)
               sampleSize1 = thisSgStats.SampleSize1;
            else
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

      public void removePopulations()
      {
         // loop through stratumstats and remove all rows for methods != 100

         List<StratumStatsDO> strataStats;

         strataStats = new List<StratumStatsDO>(cdDAL.Read<StratumStatsDO>("StratumStats", null, null));
         // loop by stratumstats for multiple SgSets
         foreach (StratumStatsDO curStrStats in strataStats)
         {
               curStrStats.DeleteStratumStats(cdDAL, curStrStats.StratumStats_CN);
         }
      }

   }
}
