using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FMSC.Controls;
using CruiseDAL;
using CruiseDAL.DataObjects;

namespace CruiseDesign.Strata_setup
{
    public partial class UnitSetupPage : UserControl
    {
        int unitRowCnt;

        public UnitSetupPage(StrataSetupWizard Owner)
        {
            this.Owner = Owner;

            InitializeComponent();
            //Owner.currentStratum = new StratumDO(Owner.cdDAL);
            //bindingSourceCurrentStratum.DataSourceChanged += new EventHandler(bindingSourceCurrentStratum_DataSourceChanged);
            // initialize data bindings
            InitializeDataBindings();
            // initialize combo box
            textBoxUOM.Text = Owner.sUOM;
        }


        #region Intialize

        private void InitializeDataBindings()
        {
            bindingSourceUnit.DataSource = Owner.cdCuttingUnits;
            //bindingSourceCurrentStratum.DataSource = Owner.currentStratum;
            bindingSourceStratum.DataSource = Owner.cdStratum;
        }

        public StrataSetupWizard Owner { get; set; }

        #endregion


        #region Button Click Events

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Owner.Cancel();
        }

        private void buttonStrata_Click(object sender, EventArgs e)
        {
            if (saveStratum(true) < 0)
                return;
            //list of selected units
            Owner.currentStratumStats = Owner.cdDAL.From<StratumStatsDO>().Where("Stratum_CN = @p1 AND SgSet = 1").Read(Owner.currentStratum.Stratum_CN).FirstOrDefault();

            if (Owner.currentStratumStats != null)
            {
                if (Owner.currentStratumStats.SgSetDescription == "Comparison Cruise")
                {
                    MessageBox.Show("Stratum Created From Comparison Cruise.\nCannot Edit Design.", "Warning", MessageBoxButtons.OK);
                    return;
                }
            }
            checkUOMfield();

            Owner.GoToStrataPage(1);
        }

        private void buttonAddStratum_Click(object sender, EventArgs e)
        {
            if (dataGridViewStratum.RowCount > 0)
            {
                if (saveStratum(true) < 0)
                    return;
            }

            Owner.newStratum = new StratumDO(Owner.cdDAL);
            
            Owner.newStratum.Method = "100";
            Owner.newStratum.Code = "";
            Owner.cdStratum.Add(Owner.newStratum);

            bindingSourceStratum.DataSource = Owner.cdStratum;
            buttonStrata.Enabled = true;
        }

        private void buttonViewStratum_Click(object sender, EventArgs e)
        {
            if (saveStratum(true) < 0)
                return;
            //list of selected units
            Owner.currentStratumStats = Owner.cdDAL.From<StratumStatsDO>().Where("Stratum_CN = @p1 AND SgSet = 1").Read(Owner.currentStratum.Stratum_CN).FirstOrDefault();

            checkUOMfield();

            // check for comparison cruise
            if (Owner.currentStratumStats.SgSetDescription == "Comparison Cruise")
                Owner.GoToSgPage(1);
            else
                Owner.GoToSgPage(0);
            //call SelectSGset Page

        }
        private void checkUOMfield()
        {

            if (textBoxUOM.Text.Length <= 0)
            {
                textBoxUOM.Text = "03";
                Owner.sUOM = "03";
            }
            textBoxUOM.ReadOnly = true;


        }
        private void buttonDeleteStratum_Click(object sender, EventArgs e)
        {
            if (StratumDO.DeleteStratum(Owner.currentStratum.DAL, Owner.currentStratum) < 0)
            {
                MessageBox.Show("Stratum has data. Cannot delete.", "Warning", MessageBoxButtons.OK);
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                Owner.cdStratum.Remove(Owner.currentStratum);
                Cursor.Current = this.Cursor;
            }

        }

        private void buttonAddUnit_Click(object sender, EventArgs e)
        {
            CuttingUnitDO newUnit = new CuttingUnitDO(Owner.cdDAL);
            Owner.cdCuttingUnits.Add(newUnit);
            bindingSourceUnit.DataSource = Owner.cdCuttingUnits;
        }
        #endregion

        #region Context Menu
        private void useReconStrataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //check for existing strata
            if (checkStrata())
                addReconStratum(false);
            //add stratum

            buttonStrata.Enabled = true;
        }

        private void useReconStrataAndSampleGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //check for existing strata
            if (checkStrata())
                addReconStratum(true);
            //add stratum

            buttonStrata.Enabled = true;

        }

        private bool checkStrata()
        {
            //check for existing strata
            if (!Owner.reconExists)
            {
                MessageBox.Show("No Recon File", "Warning", MessageBoxButtons.OK);
                return (false);
            }

            if (dataGridViewStratum.RowCount > 0)
            {
                MessageBox.Show("Stratum already exists.\nPlease delete all existing Strata first.", "Warning", MessageBoxButtons.OK);
                return (false);
            }
            return (true);
        }

        private bool checkNestedPlots(StratumDO myRSt)
        {
            int nestNum = 0;
            string myMeth = "";
            float myBAF = 0;
            float myFIX = 0;
            //loop through each unit
            myRSt.CuttingUnits.Populate();
            foreach (CuttingUnitDO rcu in myRSt.CuttingUnits)
            {
                rcu.Strata.Populate();
                if (rcu.Strata.Count() > 1)
                {
                    // check for different method/plot sizes
                    foreach (StratumDO mySt in rcu.Strata)
                    {
                        if(mySt.Method == "FIX" || mySt.Method == "PNT")
                        {
                            if (nestNum == 0)
                            {   
                                myMeth = mySt.Method;
                                myBAF = mySt.BasalAreaFactor;
                                myFIX = mySt.FixedPlotSize;
                                nestNum++;
                            }
                            else
                            {
                                if (myMeth != mySt.Method)
                                    return (true);
                                float diff = myBAF - mySt.BasalAreaFactor;
                                if (diff > 1 || diff < -1)
                                    return (true);
                                diff = myFIX - mySt.FixedPlotSize;
                                if (diff > 1 || diff < -1)
                                    return (true);
                            }
                        }
                    }
                }
            }
            return (false);
        }

        private void addReconStratum(bool addSG)
        {
            // use historical data logic
            WaitForm waitFrm = new WaitForm();
            Cursor.Current = Cursors.WaitCursor;
            waitFrm.Show();
            int nestPlots = 0;
            bool chkFCNT = false;
            // open recon file
            List<StratumDO> reconStratum = Owner.rDAL.From<StratumDO>().Read().ToList();
            foreach (StratumDO myRSt in reconStratum)
            {
                // add stratum
                Owner.newStratum = new StratumDO(Owner.cdDAL);

                // set unit codes
                if (!checkNestedPlots(myRSt))
                {
                    if (myRSt.Method == "FIXCNT")
                        chkFCNT = true;
                    else
                        chkFCNT = false;

                    myRSt.CuttingUnits.Populate();
                    float totalAcres = 0;
                    foreach (CuttingUnitDO rcu in myRSt.CuttingUnits)
                    {
                        Owner.myCuttingUnit = Owner.cdDAL.From<CuttingUnitDO>().Where("Code = @p1").Read(rcu.Code).FirstOrDefault();
                        Owner.newStratum.CuttingUnits.Add(Owner.myCuttingUnit);
                        float acres = Owner.myCuttingUnit.Area;
                        totalAcres += acres;
                    }
                    // copy stratum from recon
                    Owner.newStratum.Code = myRSt.Code;
                    if (chkFCNT)
                    {
                       Owner.newStratum.Method = "FIXCNT";
                       Owner.newStratum.FixedPlotSize = myRSt.FixedPlotSize;
                    }
                    else
                       Owner.newStratum.Method = "100";
                    Owner.newStratum.Description = myRSt.Description;

                    Owner.newStratum.Save();
                    Owner.newStratum.CuttingUnits.Save();

                    //check for tree vs plot and one vs two stage methods
                    // copy stratumstats to design
                    Owner.newStratumStats = new StratumStatsDO(Owner.cdDAL);
                    //         Owner.currentStratumStats.Stratum = Owner.currentStratum;
                    Owner.newStratumStats.Stratum_CN = Owner.newStratum.Stratum_CN;
                    Owner.newStratumStats.Code = Owner.newStratum.Code;
                    Owner.newStratumStats.Description = Owner.newStratum.Description;
                    if (chkFCNT)
                    {
                        Owner.newStratumStats.Method = "FIXCNT";
                        Owner.newStratumStats.Used = 2;
                        Owner.newStratumStats.FixedPlotSize = myRSt.FixedPlotSize;
                    }
                    else
                    {
                        Owner.newStratumStats.Method = "100";
                        Owner.newStratumStats.Used = 2;
                    }
                    Owner.newStratumStats.SgSetDescription = "";
                    Owner.newStratumStats.SgSet = 1;
                    Owner.newStratumStats.TotalAcres = totalAcres;
                    Owner.newStratumStats.Save();

                    Owner.cdStratum.Add(Owner.newStratum);
                    if (addSG || chkFCNT)
                    {
                       // List<SampleGroupDO> reconSG = new List<SampleGroupDO>(Owner.rDAL.Read<SampleGroupDO>("SampleGroup", "WHERE Stratum_CN = ?", myRSt.Stratum_CN));
                        List<SampleGroupDO> reconSG = Owner.rDAL.From<SampleGroupDO>().Where("Stratum_CN = @p1").Read(myRSt.Stratum_CN).ToList();

                        //           getSampleGroupStats();
                        foreach (SampleGroupDO myRsg in reconSG)
                        {
                            // create samplegroupstats
                            Owner.newSgStats = new SampleGroupStatsDO(Owner.cdDAL);
                            //set foriegn key
                            Owner.newSgStats.StratumStats = Owner.newStratumStats;
                            Owner.newSgStats.SgSet = 1;
                            Owner.newSgStats.Code = myRsg.Code;
                            Owner.newSgStats.CutLeave = myRsg.CutLeave;
                            Owner.newSgStats.UOM = myRsg.UOM;
                            Owner.newSgStats.PrimaryProduct = myRsg.PrimaryProduct;
                            Owner.newSgStats.SecondaryProduct = myRsg.SecondaryProduct;
                            Owner.newSgStats.DefaultLiveDead = myRsg.DefaultLiveDead;
                            Owner.newSgStats.Description = myRsg.Description;
                            
                            // pull in stats for FIXCNT
                            if(chkFCNT)
                                getSgStats(myRsg, myRSt);

                            Owner.newSgStats.Save();

                            // loop through TDV information

                            myRsg.TreeDefaultValues.Populate();
                            foreach (TreeDefaultValueDO tdv in myRsg.TreeDefaultValues)
                            {
                                // check with current TDV values
                                Owner.newTreeDefault = Owner.cdDAL.From<TreeDefaultValueDO>().Where("Species = @p1 AND PrimaryProduct = @p2 AND LiveDead = @p3").Read(tdv.Species, tdv.PrimaryProduct, tdv.LiveDead).FirstOrDefault();
                                // if exists, create link
                                Owner.newSgStats.TreeDefaultValueStats.Add(Owner.newTreeDefault);
                            }

                            Owner.newSgStats.Save();
                            Owner.newSgStats.TreeDefaultValueStats.Save();

                        }
                    }

                }
                else
                {
                    nestPlots++;
                }
            }
            waitFrm.Close();
            Cursor.Current = this.Cursor;
            if (nestPlots > 0)
                MessageBox.Show("Some units had nested plots with different cruise methods or plot sizes.\n Those strata could not be created.", "Information");
        }

        private void getSgStats(SampleGroupDO myRsg, StratumDO myRSt)
        {
            POPDO selectedPOP;
            List<LCDDO> selectedLCD;
            int n1, n2, stage, measTrees, sumKpi, talliedTrees, freq, kz;
            double st1x, st1x2, st2x, st2x2, cv1, cv2, sampErr, sumExpFac, sumNetVol;
            calcStats statClass = new calcStats();

            selectedPOP = Owner.rDAL.From<POPDO>().Where("Stratum = @p1 AND SampleGroup = @p2").Read(myRSt.Code, myRsg.Code).FirstOrDefault();
            // calculate statistics (based on method)
            if (selectedPOP == null)
            {
                MessageBox.Show("Cruise Not Processed. Cannot Import Data.", "Warning");
                return;
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
            cv2 = 0;
            // find errors stage 11=tree,single 12=tree,2 stage 21=plot,single 22=plot,2 stage
            sampErr = statClass.getSampleError(cv1, n1, 0);

            Owner.newSgStats.CV1 = Convert.ToSingle(cv1);
            Owner.newSgStats.CV2 = Convert.ToSingle(cv2);
            Owner.newSgStats.SampleSize1 = n1;
            Owner.newSgStats.SampleSize2 = n2;
            // calculate frequency

            Owner.newSgStats.SgError = Convert.ToSingle(sampErr);

            // get LCD data
//            selectedLCD = Owner.rDAL.Read<LCDDO>("LCD", "WHERE Stratum = ? AND SampleGroup = ?", myRSt.Code, myRsg.Code);
//            sumExpFac = 0;
//            sumNetVol = 0;
            //foreach (SampleGroupDO sg in Owner.histSampleGroup)
//            foreach (LCDDO lcd in selectedLCD)
//            {
                // sum volume
//                double expFac = lcd.SumExpanFactor;
//                sumExpFac += expFac;
                // sum trees
//                double netVol = lcd.SumNCUFT;
//                sumNetVol += netVol;
//            }
            // find volume/acre and trees/acre
            Owner.newSgStats.TreesPerAcre = Convert.ToSingle(Math.Round((st1x), 2));
            Owner.newSgStats.VolumePerAcre = 0;
            Owner.newSgStats.TreesPerPlot = Convert.ToSingle(Math.Round((Convert.ToSingle((float)talliedTrees / (float)n1)), 1));
            Owner.newSgStats.ReconPlots = n1;
            Owner.newSgStats.ReconTrees = talliedTrees;
        }

        #endregion

        private void UnitSetupPage_Load(object sender, EventArgs e)
        {
            //bindingSourceCurrentStratum.DataSource = Owner.currentStratum;
            if (dataGridViewStratum.RowCount > 0)
                buttonStrata.Enabled = true;
        }

        private void bindingSourceStratum_CurrentChanged(object sender, EventArgs e)
        {

            // display the Unit selected items
            Owner.currentStratum = bindingSourceStratum.Current as StratumDO;
            if (Owner.currentStratum != null)
            {
                Owner.currentStratum.CuttingUnits.Populate();
                selectedItemsGridViewUnits.SelectedItems = Owner.currentStratum.CuttingUnits;
            }
        }

        private int saveStratum(bool chkUnit)
        {
            // determine the units selected
            if (chkUnit)
            {
                unitRowCnt = selectedItemsGridViewUnits.SelectedItems.Count;
                if (unitRowCnt <= 0)
                {
                    MessageBox.Show("Need to select at least one unit", "Information");
                    return (-1);
                }
            }

            // determine if Stratum Code is entered
            if (Owner.currentStratum.Code.Length <= 0 || Owner.currentStratum.Code == null)
            {
                MessageBox.Show("Enter Stratum Code", "Information");
                return (-1);
            }
            // check for Historical Cruise Setup

            Owner.currentStratum.Save();

            foreach (CuttingUnitDO cu in Owner.cdCuttingUnits)
            {
                cu.Save();
            }

            Owner.currentStratum.CuttingUnits.Save();

            Owner.currentStratumStats = (Owner.cdDAL.From<StratumStatsDO>().Where("Stratum_CN = @p1 AND SgSet = 1")
                .Read(Owner.currentStratum.Stratum_CN).FirstOrDefault());

            if (Owner.currentStratumStats == null)
            {
                makeNewStratumStats();
            }

            return (0);
        }

        public void makeNewStratumStats()
        {
            Owner.currentStratumStats = new StratumStatsDO(Owner.cdDAL);
            //set foriegn keys
            Owner.currentStratumStats.Stratum_CN = Owner.currentStratum.Stratum_CN;
            Owner.currentStratumStats.Method = "100";
            Owner.currentStratumStats.Code = Owner.currentStratum.Code;
            Owner.currentStratumStats.Description = Owner.currentStratum.Description;
            Owner.currentStratumStats.SgSet = 1;
            Owner.currentStratumStats.SgSetDescription = "";
            Owner.currentStratumStats.Used = 2;
            float totalAcres = 0;
            foreach (CuttingUnitDO cu in Owner.currentStratum.CuttingUnits)
            {
                float acres = cu.Area;
                totalAcres += acres;
            }
            Owner.currentStratumStats.TotalAcres = totalAcres;

            Owner.currentStratumStats.Save();
            Owner.setUsed(Owner.currentStratumStats.Stratum_CN);

        }

        private void dataGridViewStratum_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            // if (saveStratum(false) < 0)
            //    return;
        }

        private void textBoxUOM_TextChanged(object sender, EventArgs e)
        {
            Owner.sUOM = textBoxUOM.Text;

        }

        private void useReconFixCnt_Click(object sender, EventArgs e)
        {
            WaitForm waitFrm = new WaitForm();
            bool chkFCNT = false;
            // open recon file
            //check to see if stratum exists **************************
            if (dataGridViewStratum.RowCount > 0)
            {
               //see if stratum FIXCNT already exists
               StratumDO checkSTR = Owner.cdDAL.From<StratumDO>().Where("Method = @p1").Read("FIXCNT").FirstOrDefault();
               if (checkSTR != null)
                  MessageBox.Show("FIXCNT Stratum already exists.\nPlease delete existing Stratum first.", "Warning", MessageBoxButtons.OK);
               return;
            }
            Cursor.Current = Cursors.WaitCursor;
            waitFrm.Show();

            List<StratumDO> reconStratum = new List<StratumDO>(Owner.rDAL.From<StratumDO>().Where("Method = @p1").Read("FIXCNT").ToList());
            foreach (StratumDO myRSt in reconStratum)
            {

            // add stratum
            Owner.newStratum = new StratumDO(Owner.cdDAL);

                // set unit codes
                myRSt.CuttingUnits.Populate();
                float totalAcres = 0;
                foreach (CuttingUnitDO rcu in myRSt.CuttingUnits)
                {
                    Owner.myCuttingUnit = Owner.cdDAL.From<CuttingUnitDO>().Where("Code = @p1").Read(rcu.Code).FirstOrDefault();
                    Owner.newStratum.CuttingUnits.Add(Owner.myCuttingUnit);
                    float acres = Owner.myCuttingUnit.Area;
                    totalAcres += acres;
                }
                    // copy stratum from recon
                Owner.newStratum.Code = myRSt.Code;
                Owner.newStratum.Method = "FIXCNT";
                Owner.newStratum.Description = myRSt.Description;
                Owner.newStratum.FixedPlotSize = myRSt.FixedPlotSize;

                Owner.newStratum.Save();
                Owner.newStratum.CuttingUnits.Save();

                //check for tree vs plot and one vs two stage methods
               // copy stratumstats to design
               Owner.newStratumStats = new StratumStatsDO(Owner.cdDAL);
               //         Owner.currentStratumStats.Stratum = Owner.currentStratum;
               Owner.newStratumStats.Stratum_CN = Owner.newStratum.Stratum_CN;
               Owner.newStratumStats.Code = Owner.newStratum.Code;
               Owner.newStratumStats.Description = Owner.newStratum.Description;

                Owner.newStratumStats.Method = "FIXCNT";
                Owner.newStratumStats.FixedPlotSize = Owner.newStratum.FixedPlotSize;
                Owner.newStratumStats.Used = 1;

                Owner.newStratumStats.SgSetDescription = "";
                Owner.newStratumStats.SgSet = 1;
                Owner.newStratumStats.TotalAcres = totalAcres;
                Owner.newStratumStats.Save();

                Owner.cdStratum.Add(Owner.newStratum);

                List<SampleGroupDO> reconSG = new List<SampleGroupDO>(Owner.rDAL.From<SampleGroupDO>().Where("Stratum_CN = @p1").Read(myRSt.Stratum_CN).ToList());

                //           getSampleGroupStats();
                foreach (SampleGroupDO myRsg in reconSG)
                {
                    // create samplegroupstats
                    Owner.newSgStats = new SampleGroupStatsDO(Owner.cdDAL);
                    //set foriegn key
                    Owner.newSgStats.StratumStats = Owner.newStratumStats;
                    Owner.newSgStats.SgSet = 1;
                    Owner.newSgStats.Code = myRsg.Code;
                    Owner.newSgStats.CutLeave = myRsg.CutLeave;
                    Owner.newSgStats.UOM = myRsg.UOM;
                    Owner.newSgStats.PrimaryProduct = myRsg.PrimaryProduct;
                    Owner.newSgStats.SecondaryProduct = myRsg.SecondaryProduct;
                    Owner.newSgStats.DefaultLiveDead = myRsg.DefaultLiveDead;
                    Owner.newSgStats.Description = myRsg.Description;

                    // pull in stats for FIXCNT
                    getSgStats(myRsg, myRSt);

                    Owner.newSgStats.Save();

                    // loop through TDV information

                    myRsg.TreeDefaultValues.Populate();
                    foreach (TreeDefaultValueDO tdv in myRsg.TreeDefaultValues)
                    {
                        // check with current TDV values
                        Owner.newTreeDefault = Owner.cdDAL.From<TreeDefaultValueDO>()
                            .Where("Species = @p1 AND PrimaryProduct = @p2 AND LiveDead = @p3")
                            .Read(tdv.Species, tdv.PrimaryProduct, tdv.LiveDead).FirstOrDefault();
                        // if exists, create link
                        Owner.newSgStats.TreeDefaultValueStats.Add(Owner.newTreeDefault);
                    }

                    Owner.newSgStats.Save();
                    Owner.newSgStats.TreeDefaultValueStats.Save();

                }
            }
            waitFrm.Close();
            Cursor.Current = this.Cursor;
        }
    }
}

