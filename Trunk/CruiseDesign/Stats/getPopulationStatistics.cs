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
   class getPopulationStatistics
   {
      private float sgPlotSize,totalAcres;
      private float curSgPlotSize;
      public float curPlotFixSize;
      public float curPlotPntSize;
      public bool reconExists = new bool();
      public bool useDefaultFix = new bool();
      public bool useDefaultPnt = new bool();
      public bool useDefaultTree = new bool();
      public bool useDefaultValues = new bool();
      public double plotVolume, plotVolume2, pltVol, treeVolumes, treeVolumes2, treeHeights;
      public double sumExpFac, vExpFac, vBarSum, vBarSum2, pntFac, vBarPlot, vBarPlot2;
      public int plotCount, treeCount, treePlot, treePlot2, errFlag;

      public DAL rDAL { get; set; }
      public DAL cdDAL { get; set; }

      public SaleDO mySale { get; set; }
      public List<StratumDO> stratum;
      //public List<StratumDO> rStratum;
      //public List<StratumDO> rCurStratum;
      public List<StratumStatsDO> selectStratumStats;
      public List<SampleGroupStatsDO> selectSgStats;
      public List<TreeCalculatedValuesDO> rTreeCalcJoin;
      public List<PlotDO> myPlots;
      //      public List<TreeDO> currentTreeList;
      //      public SampleGroupDO sgStats;
      public StratumStatsDO strStats;

      public void getPopulationStats(string dalPathRecon, DAL cDAL, bool recExists, int err)
      {
         // check recon - open rDAL
         cdDAL = cDAL;

         errFlag = 0;
         reconExists = recExists;
         if (reconExists)
         {
            try
            {
               this.rDAL = new DAL(dalPathRecon);
            }
            catch (System.IO.IOException e)
            {
               Logger.Log.E(e);
            }
            catch (System.Exception e)
            {
               Logger.Log.E(e);
            }
         }
         else
         {
            
         }

        /* try
         {
            this.cdDAL = new DAL(dalPathDesign, false);
         }
         catch (System.IO.IOException e)
         {
            Logger.Log.E(e);
            //TODO display error message to user
         }
         catch (System.Exception e)
         {
            Logger.Log.E(e);
         }
         */
         // open cdDAL
//         checkSalePurpose();
         getPopulations();
         err = errFlag;
         Finish();
      }

      //*******                                                                  getPopulations
      private void checkSalePurpose()
      {
         //String remark = mySale.Remarks;
         //if (remark == "Historical Design")
         //   reconExists = false;
      }
      
      public void getPopulations()
      {
         //MessageBox.Show("getPopulations");
         mySale = new SaleDO(cdDAL.ReadSingleRow<SaleDO>("Sale", null, null));
         if (reconExists)
         {
            // check for TreeDefaultValues - processed cruise
            //List<TreeCalculatedValuesDO> chkValues = new List<TreeCalculatedValuesDO>(rDAL.Read<TreeCalculatedValuesDO>("TreeCalculatedValues", null, null));
            rTreeCalcJoin = new List<TreeCalculatedValuesDO>(rDAL.Read<TreeCalculatedValuesDO>("TreeCalculatedValues", "JOIN Tree T JOIN SampleGroup G WHERE TreeCalculatedValues.Tree_CN = T.Tree_CN AND T.SampleGroup_CN = G.SampleGroup_CN", null));
            if (rTreeCalcJoin.Count() <= 0)
            {
               errFlag = 1;
               getDefaultData();
               // default values calc
               return;
            }
         }
         else
         {
            getDefaultData();
            return;
         }
         //get recon tree calculated list
         myPlots = new List<PlotDO>(rDAL.Read<PlotDO>("Plot", null, null));

         // get stratum definitions
         stratum = new List<StratumDO>(cdDAL.Read<StratumDO>("Stratum", null, null));


         // loop through stratum
         foreach (StratumDO curStr in stratum)
         {
            // check if Stratum needs to be re-calculated (if method = 100, then it needs to be recalculated)
            strStats = (cdDAL.ReadSingleRow<StratumStatsDO>("StratumStats", "WHERE Stratum_CN = ? AND SgSet = 1 AND Method = ?", curStr.Stratum_CN, "100"));
            if (curStr.Method == "100" && strStats.Used == 2)
            {
               curStr.CuttingUnits.Populate();
               // get total strata acres

               removePopulations(curStr.Stratum_CN);

               totalAcres = 0;
               foreach (CuttingUnitDO cu in curStr.CuttingUnits)
               {
                  float acres = cu.Area;
                  totalAcres += acres;
               }


               selectStratumStats = new List<StratumStatsDO>(cdDAL.Read<StratumStatsDO>("StratumStats", "WHERE Stratum_CN = ?", curStr.Stratum_CN));
               // loop by stratumstats for multiple SgSets
               foreach (StratumStatsDO curStrStats in selectStratumStats)
               {
                  useDefaultFix = false;
                  useDefaultTree = false;
                  useDefaultPnt = false;
                  curPlotFixSize = 0;
                  curPlotPntSize = 0;

                  // add methods to the StratumStats table (FIX,FCM,F3P,STR,3P,S3P,PNT,P3P,PCM,3PPNT)
                  //setupPopulations(curStrStats.Stratum_CN, curStrStats.Code, curStrStats.Description, curStrStats.SgSet, curStrStats.SgSetDescription, curStrStats.TotalAcres);

                  selectSgStats = new List<SampleGroupStatsDO>(cdDAL.Read<SampleGroupStatsDO>("SampleGroupStats", "Where StratumStats_CN = ?", curStrStats.StratumStats_CN));
                  // loop through sample groups
                  foreach (SampleGroupStatsDO curSgStats in selectSgStats)
                  {
                     //Get all statistics from FIX plots  **************
                     sgPlotSize = 0;
                     //find data from Recon file
                     int iReturn = getSgReconData(curStrStats, curSgStats, "FIX");

                     if (iReturn > 0)
                     {
                        useDefaultFix = true;
                        useDefaultTree = true;
                     }

                     if (curPlotFixSize == 0) curPlotFixSize = sgPlotSize;
                     // check plot sizes are not different across sample groups
                     if (curPlotFixSize != sgPlotSize)
                     {
                        // plot sizes are different across sample groups, cannot calculate Fix statistics
                        useDefaultFix = true;
                     }

                     // calc Fixed
                     getFixSgStats(curStrStats, curSgStats, useDefaultFix);

                     //Get all statistics from PNT plots  **************
                     curPlotFixSize = 0;
                     sgPlotSize = 0;
                     //find data from Recon file
                     iReturn = getSgReconData(curStrStats, curSgStats, "PNT");

                     if (iReturn > 0)
                     {
                        useDefaultPnt = true;
                     }

                     if (curPlotPntSize == 0) curPlotPntSize = sgPlotSize;
                     // check plot sizes are not different across sample groups
                     if (curPlotPntSize != sgPlotSize)
                     {
                        // plot sizes are different across sample groups, cannot calculate Fix statistics
                        useDefaultPnt = true;
                     }
                     getPntSgStats(curStrStats, curSgStats, useDefaultPnt);
                  }
               }
               //calculate StratumStats from the SampleGroupStats
               getStratumStats(curStr.Stratum_CN);

            }
            else
            {
              // selectStratumStats = new List<StratumStatsDO>(cdDAL.Read<StratumStatsDO>("StratumStats", "WHERE Stratum_CN = ?", curStr.Stratum_CN));
               // check for historical data
              // foreach (StratumStatsDO curStrStats in selectStratumStats)
              // {
                  // check for error equal to zero
              //    if (curStrStats.StrError == 0)
              //    {
              //       selectSgStats = new List<SampleGroupStatsDO>(cdDAL.Read<SampleGroupStatsDO>("SampleGroupStats", "Where StratumStats_CN = ?", curStrStats.StratumStats_CN));
                     // loop through sample groups
              //       foreach (SampleGroupStatsDO curSgStats in selectSgStats)
              //       {
              //          getDefaultStats(curStrStats, curSgStats);
              //       }
               //      getStratumStats(curStr.Stratum_CN);
               //   }
               //}
            }
         }
      }
      //*********                                                                           Get Default Data
      public void getDefaultData()
      {
         // get stratum definitions
         stratum = new List<StratumDO>(cdDAL.Read<StratumDO>("Stratum", null, null));

         // loop through stratum
         foreach (StratumDO curStr in stratum)
         {
            strStats = (cdDAL.ReadSingleRow<StratumStatsDO>("StratumStats", "WHERE Stratum_CN = ? AND SgSet = 1 AND Method = ?", curStr.Stratum_CN, "100"));
            if (curStr.Method == "100" && strStats.Used == 2)
            {
               curStr.CuttingUnits.Populate();
               // get total strata acres

               removePopulations(curStr.Stratum_CN);

               totalAcres = 0;
               foreach (CuttingUnitDO cu in curStr.CuttingUnits)
               {
                  float acres = cu.Area;
                  totalAcres += acres;
               }
               
               selectStratumStats = new List<StratumStatsDO>(cdDAL.Read<StratumStatsDO>("StratumStats", "WHERE Stratum_CN = ?", curStr.Stratum_CN));

               foreach (StratumStatsDO curStrStats in selectStratumStats)
               {
                  // check for error equal to zero
                  if (curStrStats.StrError == 0)
                  {
                     selectSgStats = new List<SampleGroupStatsDO>(cdDAL.Read<SampleGroupStatsDO>("SampleGroupStats", "Where StratumStats_CN = ?", curStrStats.StratumStats_CN));
                     // loop through sample groups
                     foreach (SampleGroupStatsDO curSgStats in selectSgStats)
                     {
                        getFixSgStats(curStrStats, curSgStats, true);
                        getPntSgStats(curStrStats, curSgStats, true);
                     }
                     getStratumStats(curStr.Stratum_CN);
                  }
               }
            }
         }
      }

      //*********                                                                           setupPopulations
      public void getFixSgStats(StratumStatsDO curStrStats, SampleGroupStatsDO curSgStats, bool useDefault)
      {
         //sgStats = new SampleGroupDO(cdDAL);
         float treePerAcre = 1;
         float volumePerAcre = 1;
         float treePerPlot = 1;
         if (!useDefault)
         {
            // calculate Expansion Factor
            //expFac = curSgPlotSize / plotCount;
            // calculate TPA
  //          float xtreePerAcre = treeCount * expFac;
            pntFac = curSgPlotSize / plotCount;
            treePerAcre = (float) (pntFac * treeCount);
            // calculate VPA
//            float xvolumePerAcre = plotVolume * expFac;
            volumePerAcre = (float)(plotVolume * pntFac);
            // calculate TPP
            //treePerPlot = (float) (treeCount / plotCount);
            treePerPlot = (float)((float)treeCount / (float)plotCount);
            if (treePerPlot < .1 && treePerPlot > 0) treePerPlot = (float)(0.1);
         }
         // calculate statistics
         calc100(curStrStats, curSgStats, useDefault, treePerAcre, volumePerAcre);
         calculateStats(curStrStats, curSgStats, useDefault, treePerAcre, volumePerAcre, treePerPlot, "FIX");
         calculateStats(curStrStats, curSgStats, useDefault, treePerAcre, volumePerAcre, treePerPlot, "F3P");
         calculateStats(curStrStats, curSgStats, useDefault, treePerAcre, volumePerAcre, treePerPlot, "FCM");
         calculateStats(curStrStats, curSgStats, useDefault, treePerAcre, volumePerAcre, treePerPlot, "STR");
         calculateStats(curStrStats, curSgStats, useDefault, treePerAcre, volumePerAcre, treePerPlot, "3P");
         calculateStats(curStrStats, curSgStats, useDefault, treePerAcre, volumePerAcre, treePerPlot, "S3P");
      }

      public void getPntSgStats(StratumStatsDO curStrStats, SampleGroupStatsDO curSgStats, bool useDefault)
      {
         //sgStats = new SampleGroupDO(cdDAL);
         float treePerAcre = 1;
         float volumePerAcre = 1;
         float treePerPlot = 1;
         if (!useDefault)
         {
            // calculate TPA
            treePerAcre = (float)(sumExpFac / plotCount);
            // calculate VPA
            pntFac = curSgPlotSize / plotCount;
            volumePerAcre = (float) (vBarSum * pntFac);
            // calculate TPP
            treePerPlot = (float)((float)treeCount / (float)plotCount);
            if (treePerPlot < .1 && treePerPlot > 0) treePerPlot = (float)(0.1);
         }
         // calculate statistics
         calculateStats(curStrStats, curSgStats, useDefault, treePerAcre, volumePerAcre, treePerPlot, "PNT");
         calculateStats(curStrStats, curSgStats, useDefault, treePerAcre, volumePerAcre, treePerPlot, "P3P");
         calculateStats(curStrStats, curSgStats, useDefault, treePerAcre, volumePerAcre, treePerPlot, "PCM");
         calculateStats(curStrStats, curSgStats, useDefault, treePerAcre, volumePerAcre, treePerPlot, "3PPNT");
      }

      public void calc100(StratumStatsDO curStrStats, SampleGroupStatsDO curSgStats, bool useDefault, float treePerAcre, float volumePerAcre)
      {

         double sgCV = 100;
         double sgError = 0;

         calcStats cStat = new calcStats();
         if (!useDefault)
         {
            sgCV = cStat.getCV(treeVolumes, treeVolumes2, treeCount);
            curSgStats.CV_Def = 0;
            curSgStats.CV2_Def = 0;
         }
         else
         {
            curSgStats.CV_Def = 1;
            curSgStats.CV2_Def = 1;
         }
         curSgStats.TreesPerAcre = treePerAcre;
         curSgStats.VolumePerAcre = volumePerAcre;
         curSgStats.TreesPerPlot = 0;
         curSgStats.AverageHeight = 0;
         curSgStats.ReconPlots = plotCount;
         curSgStats.ReconTrees = treeCount;

         //copy calculated stuff
         curSgStats.CV1 = (float)sgCV;
         curSgStats.SampleSize1 = (long)(treePerAcre * totalAcres);
         curSgStats.TPA_Def = (long)(treePerAcre * totalAcres);
         curSgStats.VPA_Def = (long)(volumePerAcre * totalAcres);
         curSgStats.SgError = (float)sgError;

         curSgStats.Save();
      }

      public void calculateStats(StratumStatsDO curStrStats, SampleGroupStatsDO curSgStats, bool useDefault, float treePerAcre, float volumePerAcre, float treePerPlot, String method)
      {
         double sgCV = 100;
         double sgCV2 = 0;
         double sgError = 40;
         int n = 0;
         int n2 = 0;
         int KZ = 0;
         int Freq = 0;

         calcStats cStat = new calcStats();
         if (!useDefault)
         {
            if (method == "FIX")
            {
               sgCV = cStat.getPntCV(plotVolume, plotVolume2, pntFac, plotCount);
               sgError = cStat.getSampleError(sgCV, plotCount, 0);
               n = plotCount;
               n2 = treePlot;
            }
            else if (method == "PNT")
            {
               sgCV = cStat.getPntCV(vBarSum, vBarPlot2, pntFac, plotCount);
               sgError = cStat.getSampleError(sgCV, plotCount, 0);
               n = plotCount;
               n2 = treePlot;
            }
            else if (method == "F3P")
            {
               sgCV = cStat.getPntCV(plotVolume, plotVolume2, pntFac, plotCount);
               sgCV2 = 30;
               sgError = cStat.getTwoStageError(sgCV, sgCV2, plotCount, treePlot);
               n = plotCount;
               n2 = treePlot;
               KZ = 1;
            }
            else if (method == "P3P" || method == "3PPNT")
            {
               sgCV = cStat.getPntCV(vBarSum, vBarPlot2, pntFac, plotCount);
               sgCV2 = 30;
               sgError = cStat.getTwoStageError(sgCV, sgCV2, plotCount, treePlot);
               n = plotCount;
               n2 = treePlot;
               if (method == "3PPNT") n2 = plotCount;
               KZ = 1;
            }
            else if (method == "FCM")
            {
               sgCV = cStat.getCV(treePlot, treePlot2, plotCount);
               sgCV2 = cStat.getCV(treeVolumes, treeVolumes2, treePlot);
               sgError = cStat.getTwoStageError(sgCV, sgCV2, plotCount, treePlot);
               n = plotCount;
               n2 = treePlot;
               Freq = 1;
            }
            else if (method == "PCM")
            {
               sgCV = cStat.getCV(treePlot, treePlot2, plotCount);
               sgCV2 = cStat.getCV(vBarSum, vBarSum2, treePlot);
               sgError = cStat.getTwoStageError(sgCV, sgCV2, plotCount, treePlot);
               n = plotCount;
               n2 = treePlot;
               Freq = 1;
            }
            else if (method == "STR")
            {
               sgCV = cStat.getCV(treeVolumes, treeVolumes2, treeCount);
               sgError = cStat.getSampleError(sgCV, treeCount, 0);
               n = treePlot;
               if (treePlot > 0)
                  Freq = (int)Math.Floor((treePerAcre * totalAcres) / treeCount);
               else
                  Freq = 0;
            }
            else if (method == "3P")
            {
               sgCV = 35;
               sgError = cStat.getSampleError(sgCV, treeCount, 0);
               n = treePlot;
               if (treeVolumes > 0)
                  KZ = (int)Math.Floor((volumePerAcre * totalAcres) / treeVolumes);
               else
                  KZ = 0;
            }
            else if (method == "S3P")
            {
               sgCV = cStat.getCV(treeVolumes, treeVolumes2, treeCount);
               sgCV2 = 35;
               sgError = cStat.getTwoStageError(sgCV, sgCV2, treeCount, treeCount);
               if (treeVolumes > 0)
                  KZ = (int)Math.Floor((volumePerAcre * totalAcres) / treeVolumes);
               else
                  KZ = 0;
               Freq = 1;
            }
         }
         else
         {
            if (method == "FCM" || method == "PCM")
            {
               sgCV2 = 100;
               cStat.getTwoStageSampleSize(sgCV, sgCV2, sgError);
               n = (int)cStat.sampleSize1;
               n2 = (int)cStat.sampleSize2;
            }
            else if (method == "F3P" || method == "P3P" || method == "S3P" || method == "3PPNT")
            {
               sgCV2 = 35;
               cStat.getTwoStageSampleSize(sgCV, sgCV2, sgError);
               n = (int)cStat.sampleSize1;
               n2 = (int)cStat.sampleSize2;
            }
            else
            {
               n = cStat.getSampleSize(sgError, sgCV);
            }
         }

         // Find the strataStats_CN  
         long? strStatsCN = GetStratumStatsCN(curStrStats, method);
         // Create SampleGroupStats record with StratumStats CN and save stats
         SampleGroupStatsDO newSgStats = new SampleGroupStatsDO(cdDAL);
         //copy standard info
         newSgStats.StratumStats_CN = strStatsCN;
         newSgStats.Code = curSgStats.Code;
         newSgStats.Description = curSgStats.Description;
         newSgStats.SgSet = curSgStats.SgSet;
         newSgStats.CutLeave = curSgStats.CutLeave;
         newSgStats.UOM = curSgStats.UOM;
         newSgStats.PrimaryProduct = curSgStats.PrimaryProduct;
         newSgStats.SecondaryProduct = curSgStats.SecondaryProduct;
         newSgStats.DefaultLiveDead = curSgStats.DefaultLiveDead;
         newSgStats.MinDbh = curSgStats.MinDbh;
         newSgStats.MaxDbh = curSgStats.MaxDbh;
         // general stuff
         newSgStats.TreesPerAcre = treePerAcre;
         newSgStats.VolumePerAcre = volumePerAcre;
         newSgStats.TreesPerPlot = treePerPlot;
         newSgStats.TPA_Def = (long)(treePerAcre * totalAcres);
         newSgStats.VPA_Def = (long)(volumePerAcre * totalAcres);
         if (treeCount > 0)
            newSgStats.AverageHeight = (float)(treeHeights / treeCount);
         else
            newSgStats.AverageHeight = 0;
         newSgStats.KZ = KZ;
         newSgStats.BigBAF = 0;
         newSgStats.BigFIX = 0;
         newSgStats.SamplingFrequency = Freq;
         newSgStats.InsuranceFrequency = 0;
         //copy calculated stuff
         newSgStats.CV1 = (float)sgCV;
         newSgStats.CV2 = (float)sgCV2;
         newSgStats.SampleSize1 = n;
         newSgStats.SampleSize2 = n2;
         newSgStats.SgError = (float)sgError;
         if (useDefault)
         {
            newSgStats.CV_Def = 1;
            newSgStats.CV2_Def = 1;
         }
         else
         {
            newSgStats.CV_Def = 0;
            newSgStats.CV2_Def = 0;
         }
         newSgStats.ReconPlots = plotCount;
         newSgStats.ReconTrees = treeCount;

         //selectSgStats.Add(newSgStats);
         newSgStats.Save();
         //long? sgcn = newSgStats.SampleGroupStats_CN;
         curSgStats.TreeDefaultValueStats.Populate();
         foreach (TreeDefaultValueDO myTDV in curSgStats.TreeDefaultValueStats)
         {
            SampleGroupStatsTreeDefaultValueDO mySgTDV = new SampleGroupStatsTreeDefaultValueDO(cdDAL);
            mySgTDV.TreeDefaultValue_CN = myTDV.TreeDefaultValue_CN;
            mySgTDV.SampleGroupStats = newSgStats;
            mySgTDV.Save();
         }
      }

 
      public long? GetStratumStatsCN(StratumStatsDO curStrStats, string method)
      {
         //find StratumStats_CN for population with Stratum_CN, Code, SgSet, method are the same
         StratumStatsDO thisStrStats = (cdDAL.ReadSingleRow<StratumStatsDO>("StratumStats", "WHERE Stratum_CN = ? AND SgSet = ? AND Method = ?", curStrStats.Stratum_CN, curStrStats.SgSet, method));
         //         currentStratumStats = (cdDAL.ReadSingleRow<StratumStatsDO>("StratumStats", "WHERE Stratum_CN = ? AND SgSet = 1", currentStratum.Stratum_CN));
         if (thisStrStats != null)
         {
            if(method == "FIX" || method == "FCM" || method == "F3P")
               thisStrStats.FixedPlotSize = curPlotFixSize;
            else if(method == "PNT" || method == "PCM" || method == "P3P" || method == "3PPNT")
               thisStrStats.BasalAreaFactor = curPlotPntSize;
            thisStrStats.TotalAcres = totalAcres;
            thisStrStats.Used = 0;
            thisStrStats.Save();
            return (thisStrStats.StratumStats_CN);
         }
         else
         {
            // add new record
            StratumStatsDO newStrStats = new StratumStatsDO(cdDAL);
            newStrStats.Stratum_CN = curStrStats.Stratum_CN;
            newStrStats.Code = curStrStats.Code;
            newStrStats.Description = curStrStats.Description;
            newStrStats.SgSet = curStrStats.SgSet;
            newStrStats.SgSetDescription = curStrStats.SgSetDescription;
            newStrStats.TotalAcres = totalAcres;
            newStrStats.Method = method;
            newStrStats.Used = 0;
            if (method == "FIX" || method == "FCM" || method == "F3P")
               newStrStats.FixedPlotSize = curPlotFixSize;
            else if (method == "PNT" || method == "PCM" || method == "P3P" || method == "3PPNT")
               newStrStats.BasalAreaFactor = curPlotPntSize;
            newStrStats.Save();
            // return StratumStats_CN
            return (newStrStats.StratumStats_CN);
         }
      }

      //********************                                                                      getSgReconData
      public int getSgReconData(StratumStatsDO curStrStats, SampleGroupStatsDO curSgStats, string method)
      {
         CuttingUnitDO rUnit;
         SampleGroupDO rSampleGroup;
         long? myStratumCN = 0;
         //long?[] stratumArray = new long?[10];
         
         //get search variables
         if (!reconExists)
            return (1);
         double vBar;
         int unitCnt = 0;
         treeHeights = 0;
         plotCount = 0;
         plotVolume = 0;
         plotVolume2 = 0;
         treePlot = 0;
         treePlot2 = 0;
         vExpFac = 0;
         sumExpFac = 0;
         treeCount = 0;
         treeVolumes = 0;
         treeVolumes2 = 0;
         vBarSum = 0;
         vBarSum2 = 0;
         vBarPlot2 = 0;
         //         currentTreeList.Clear();
         curSgStats.TreeDefaultValueStats.Populate();
         // loop through design units
         foreach (CuttingUnitDO curUnit in curStrStats.Stratum.CuttingUnits)
         {
            rUnit = rDAL.ReadSingleRow<CuttingUnitDO>("CuttingUnit", "Where Code = ?", curUnit.Code);
            if (rUnit != null)
            {
               unitCnt++;
               rUnit.Strata.Populate();
               int cnt = 0;
               foreach (StratumDO myStratum in rUnit.Strata)
               {
                  if (myStratum.Method == method)
                  {
                     // check sample group product code
                     rSampleGroup = rDAL.ReadSingleRow<SampleGroupDO>("SampleGroup", "Where Stratum_CN = ? AND PrimaryProduct = ?", myStratum.Stratum_CN, curSgStats.PrimaryProduct);
                     if (rSampleGroup != null)
                     {
                        myStratumCN = myStratum.Stratum_CN;
                        if (method == "FIX")
                           curSgPlotSize = myStratum.FixedPlotSize;
                        else if (method == "PNT")
                           curSgPlotSize = myStratum.BasalAreaFactor;
                        cnt++;
                     }
                  }
               }
               if (cnt == 0)
               {
                  return (1);
               }
               else if (cnt > 1)
               {
                  // loop through stratumArray

                  // popup to select correct stratum
               }
               //            else
               //              myStratumCN = stratumArray[0];

               // get number of plots for stratum and cutting unit
               var myPlotList = (from plt in myPlots
                                 where plt.CuttingUnit_CN == rUnit.CuttingUnit_CN
                                 && plt.Stratum_CN == myStratumCN
                                 select plt).ToList();
               //List<PlotDO> myPlots = new List<PlotDO>(rDAL.Read<PlotDO>("Plot", "WHERE Plot.CuttingUnit_CN = ? AND Plot.Stratum_CN = ?", rUnit.CuttingUnit_CN, myStratumCN));
               plotCount += myPlotList.Count();
               // loop through plots
               foreach (PlotDO curPlot in myPlotList)
               {
                  pltVol = 0;
                  vBarPlot = 0;
                  int plotTreeCount = 0;
                  vExpFac = 0;
                  foreach (TreeDefaultValueDO curTdv in curSgStats.TreeDefaultValueStats)
                  {
                     var myTreeList = (from tcv in rTreeCalcJoin
                                       where tcv.Tree.SampleGroup.PrimaryProduct == curSgStats.PrimaryProduct
                                       && tcv.Tree.Plot_CN == curPlot.Plot_CN
                                       && tcv.Tree.Species == curTdv.Species
                                       && tcv.Tree.LiveDead == curTdv.LiveDead
                                       select tcv).ToList();

                     if (curSgStats.MinDbh > 0 && curSgStats.MaxDbh > 0)
                     {
                        double maxDbh = curSgStats.MaxDbh + 0.0499;
                        double minDbh = curSgStats.MinDbh - 0.0500;
                        myTreeList = (from tcv in rTreeCalcJoin
                                      where tcv.Tree.SampleGroup.PrimaryProduct == curSgStats.PrimaryProduct
                                      && tcv.Tree.Plot_CN == curPlot.Plot_CN
                                      && tcv.Tree.Species == curTdv.Species
                                      && tcv.Tree.LiveDead == curTdv.LiveDead
                                      && tcv.Tree.DBH >= minDbh
                                      && tcv.Tree.DBH <= maxDbh
                                      select tcv).ToList();
                     }

                     // load Unit, Plot, Tree, DBH, THT, Volume into list
                     //check MinDbh and MaxDbh
                     foreach (TreeCalculatedValuesDO myTree in myTreeList)
                     {
                        plotTreeCount++;
                        treeHeights += myTree.Tree.TotalHeight;

                        if (myTree.Tree.DBH > 0 && method == "PNT")
                        {
                           if (curSgStats.UOM == "01")
                              vBar = myTree.NetBDFTPP / (0.005454 * myTree.Tree.DBH * myTree.Tree.DBH);
                           else
                              vBar = myTree.NetCUFTPP / (0.005454 * myTree.Tree.DBH * myTree.Tree.DBH);

                           vBarSum += vBar;
                           vBarSum2 += (vBar * vBar);
                           vBarPlot += vBar;

                           vExpFac += curSgPlotSize / (0.005454 * myTree.Tree.DBH * myTree.Tree.DBH);
                        }

                        if (curSgStats.UOM == "01")
                        {
                           treeVolumes += myTree.NetBDFTPP;
                           treeVolumes2 += myTree.NetBDFTPP * myTree.NetBDFTPP;
                           pltVol += myTree.NetBDFTPP;
                        }
                        else
                        {
                           treeVolumes += myTree.NetCUFTPP;
                           treeVolumes2 += myTree.NetCUFTPP * myTree.NetCUFTPP;
                           pltVol += myTree.NetCUFTPP;
                        }
                     }
                  }
                  // save plot level information
                  treeCount += plotTreeCount;
                  treePlot += plotTreeCount;
                  treePlot2 += plotTreeCount * plotTreeCount;
                  plotVolume += pltVol;
                  plotVolume2 += pltVol * pltVol;
                  vBarPlot2 += vBarPlot * vBarPlot;
                  sumExpFac += vExpFac;

               }

               // !!!check for different fixed plot sizes across units!!!
               if (sgPlotSize == 0) sgPlotSize = curSgPlotSize;
               if (sgPlotSize != curSgPlotSize)
               {
                  // cannot calculate statistics
                  if (method == "FIX")
                  {
                     useDefaultFix = true;
                     useDefaultTree = true;
                  }
                  else if (method == "PNT")
                  {
                     useDefaultPnt = true;
                  }
               }
            }
         }
         if(unitCnt == 0) return (1);
         
         return (0);

      }


      public long? getReconStratum(SampleGroupStatsDO curSgStats, long? UnitCN, string method)
      {

         // find strata assigned with Unit Code where Method = FIX

         List<StratumDO> rStratum = new List<StratumDO>(rDAL.Read<StratumDO>("Stratum", "WHERE CuttingUnit_CN = ? AND Stratum.Method = ?", UnitCN, method));
         //List<StratumDO> rStratum = new List<StratumDO>(rDAL.Read<StratumDO>("Stratum", "JOIN CuttingUnitStratum JOIN CuttingUnit WHERE Stratum.Stratum_CN = CuttingUnitStratum.Stratum_CN AND CuttingUnitStratum.CuttingUnit_CN = CuttingUnit.CuttingUnit_CN AND CuttingUnit.Code = ? AND Stratum.Method = ?", curUnit.Code, method));
         // find stratum with correct method (If multiple, ask for correct to use)
         if (rStratum.Count() > 0)
         {
            if (rStratum.Count() > 1)
            {
               // check sample group for curSgStats product code
               List<StratumDO> chkStratum = new List<StratumDO>(rDAL.Read<StratumDO>("Stratum", "Join SampleGroup WHERE Stratum.Stratum_CN = SampleGroup.Stratum_CN AND Stratum.CuttingUnit_CN = ? AND Stratum.Method = ? AND SampleGroup.PrimaryProduct = ?", UnitCN, method, curSgStats.PrimaryProduct));

               //List<StratumDO> chkStratum = new List<StratumDO>(rDAL.Read<StratumDO>("Stratum", "JOIN CuttingUnitStratum JOIN CuttingUnit JOIN SampleGroup WHERE Stratum.Stratum_CN = CuttingUnitStratum.Stratum_CN AND CuttingUnitStratum.CuttingUnit_CN = CuttingUnit.CuttingUnit_CN AND Stratum.Stratum_CN = SampleGroup.Stratum_CN AND CuttingUnit.Code = ? AND Stratum.Method = ? AND SampleGroup.PrimaryProduct = ?", curUnit.Code, method, curSgStats.PrimaryProduct));
               // if only one stratum, return as a single row
               if (chkStratum.Count() == 1)
               {
                  if (method == "FIX")
                     curSgPlotSize = chkStratum[0].FixedPlotSize;
                  else if (method == "PNT")
                     curSgPlotSize = chkStratum[0].BasalAreaFactor;
                  return (chkStratum[0].Stratum_CN);
               }
               else
               {
                  if (method == "FIX")
                     curSgPlotSize = 0;
                  else if (method == "PNT")
                     curSgPlotSize = 0;

                  // if two or more strata, check species codes (?)
                  // user selects the stratum to use
                  // save the stratum_cn to use for rest of design stratum
               }
            }
            else
            {
               // return rStratum as single row
               if (method == "FIX")
                  curSgPlotSize = rStratum[0].FixedPlotSize;
               else if (method == "PNT")
                  curSgPlotSize = rStratum[0].BasalAreaFactor;
               return (rStratum[0].Stratum_CN);
            }
         }
         return (null);
      }

      public void removePopulations(long? stratum_cn)
      {
         // loop through stratumstats and remove all rows for methods != 100

         List<StratumStatsDO> strataStats;

         strataStats = new List<StratumStatsDO>(cdDAL.Read<StratumStatsDO>("StratumStats", "WHERE Stratum_CN = ?", stratum_cn));
         // loop by stratumstats for multiple SgSets
         foreach (StratumStatsDO curStrStats in strataStats)
         {
            if (curStrStats.Method != "100")
            {
               curStrStats.DeleteStratumStats(cdDAL, curStrStats.StratumStats_CN);
            }
         }
      }
      
      public void getStratumStats(long? stratumCN)
      {
         List<StratumStatsDO> myStratumStats;
         List<SampleGroupStatsDO> mySgStats;
         float totalVolumeAcre, totalVolume, wtCV1, wtCv2, volumeAcre, strTpp;
         float cv1, cv2, wtErr, sgErr, treesAcre, totCost;
         long sampleSize1, sampleSize2;
         
         //loop through SampleGroupStats
         myStratumStats = new List<StratumStatsDO>(cdDAL.Read<StratumStatsDO>("StratumStats", "WHERE Stratum_CN = ?", stratumCN));
         foreach (StratumStatsDO thisStrStats in myStratumStats)
         {
            mySgStats = new List<SampleGroupStatsDO>(cdDAL.Read<SampleGroupStatsDO>("SampleGroupStats", "Where StratumStats_CN = ?", thisStrStats.StratumStats_CN));
            // loop through sample groups
            //totalVolumeAcre = getTotals(mySgStats);
            totalVolumeAcre = mySgStats.Sum(P => P.VolumePerAcre);
            totalVolume = totalVolumeAcre * totalAcres;
            strTpp = mySgStats.Sum(P => P.TreesPerPlot);
            treesAcre = mySgStats.Sum(P => P.TreesPerAcre);
            sampleSize1 = mySgStats.Sum(P => P.SampleSize1);
            sampleSize2 = mySgStats.Sum(P => P.SampleSize2); 
            wtCV1 = 0;
            wtCv2 = 0;
            wtErr = 0;
            foreach (SampleGroupStatsDO thisSgStats in mySgStats)
            {
               //sum volumes, trees/acre, sample sizes
               //sampleSize1 += thisSgStats.SampleSize1;
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

               wtErr += (float)Math.Pow((sgErr * (volumeAcre * totalAcres)), 2);

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
            thisStrStats.TotalAcres = totalAcres;

            if(mySale.DefaultUOM == "01")
               thisStrStats.TotalVolume = (float)(totalVolume / 1000.0);
            else
               thisStrStats.TotalVolume = (float)(totalVolume/100.0);
            
            if (thisStrStats.Method == "FIX" || thisStrStats.Method == "FCM" || thisStrStats.Method == "F3P" || thisStrStats.Method == "PNT" || thisStrStats.Method == "PCM" || thisStrStats.Method == "P3P" || thisStrStats.Method == "3PPNT")
            {
               if (sampleSize1 > 0)
                  thisStrStats.PlotSpacing = (int)Math.Floor(Math.Sqrt((totalAcres * 43560) / sampleSize1));
               // get costs

            }
            else
            {
               if(thisStrStats.TreesPerAcre > 0)
                  thisStrStats.PlotSpacing = (int)Math.Floor(Math.Sqrt((43560) / thisStrStats.TreesPerAcre));
            }

            if (thisStrStats.Method == "100")
            {
               if(thisStrStats.SgSet == 1)
                  thisStrStats.Used = 1;
               else
                  thisStrStats.Used = 0;
            }

            thisStrStats.Save();
         }
      }


      private void Finish()
      {
         if(rDAL != null)
            rDAL.Dispose();
         //if(cdDAL != null)
         //   cdDAL.Dispose();
      
      }
   }
}
      
