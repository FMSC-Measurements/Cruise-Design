using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CruiseDesign.Strata_setup;
using CruiseDesign.Historical_setup;
using CruiseDesign.Stats;
using CruiseDesign.ProductionDesign;
using CruiseDAL;
using System.Reflection;

namespace CruiseDesign
{
    public partial class CruiseDesignMain : Form
    {
        public DAL cdDAL { get; set; }
        public bool IsUsingV3File { get; set; }

        int ButtonSelect = 1;
        public string dalPathCruise;
        public string dalPathDesign;
        public string dalPathProcess;
        bool reconExists = true;
        bool prodFile = false;
        public bool canCreateNew = false;
        //       bool openDAL = false;
        public int errFlag;

        public CruiseDesignMain(string[] args)
        {
            InitializeComponent();

            // to update version change the Version property in the project file
            var version = Assembly.GetEntryAssembly().GetName().Version?.ToString(3);
            this.Text = $"Cruise Design {version}";

            if (args.Length != 0)
            {
                Text = Convert.ToString(args[0]);
                dalPathDesign = Convert.ToString(args[0]);
                // does .cruise file exist
                reconExists = doesReconFileExist();

                canCreateNew = false;

                if (openDesignFile())
                {
                    buttonSetup.Enabled = true;
                    buttonDesign.Enabled = true;
                    buttonTools.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Unable to open the design file", "Information");
                }
            }
        }

        private void CruiseDesignMain_Load(object sender, EventArgs e)
        {

        }
        //Row One  -------------------------------------------------------------------------------
        private void buttonRowOne_Click(object sender, EventArgs e)
        {
            throw new Exception();


            if (ButtonSelect == 0)
            {
                //MessageBox.Show("Deleted", "Information");
            }
            // FILE TAB  Open Existing Cruise Design ++++++++++
            else if (ButtonSelect == 1)
            {
                // open recon file
                if (openFileDialogDesign.ShowDialog() == DialogResult.OK)
                {
                    //set title bar with file name
                    Text = openFileDialogDesign.SafeFileName;
                    dalPathDesign = openFileDialogDesign.FileName;
                    var fileExtention = Path.GetExtension(dalPathDesign);
                    // check for design file vs production cruise
                    if (fileExtention.Equals(".cruise", StringComparison.OrdinalIgnoreCase))
                    {
                        //test comment
                        prodFile = true;
                        reconExists = false;
                        canCreateNew = false;
                    }
                    else if (fileExtention.Equals(".crz3", StringComparison.OrdinalIgnoreCase))
                    {
                        if (doesProcessFileExistD())
                        {
                            dalPathDesign = dalPathProcess;
                            IsUsingV3File = true;
                            prodFile = true;
                            reconExists = false;
                            canCreateNew = false;
                        }
                        else
                        {
                            MessageBox.Show("Cruise file not processed. Cannot continue.", "Warning");
                            return;
                        }
                    }
                    else
                    {
                        reconExists = doesReconFileExist();
                        canCreateNew = false;
                        prodFile = false;
                    }
                    if (openDesignFile())
                    {
                        buttonSetup.Enabled = true;
                        buttonDesign.Enabled = true;
                        buttonTools.Enabled = true;
                    }
                    else
                        MessageBox.Show("Unable to open the file.", "Information");

                }
            }
            // SETUP TAB    Setup Using Recon  +++++++++++++       
            else if (ButtonSelect == 2)
            {
                if (prodFile)
                {
                    MessageBox.Show("Production File Found.\nNo changes to the current design allowed.\n", "Warning", MessageBoxButtons.OK);
                    return;
                }

                StrataSetupWizard strDlg = new StrataSetupWizard(this, dalPathCruise, reconExists, prodFile, canCreateNew);
                strDlg.Owner = this;

                //        strDlg.dalFile = dalPath;
                strDlg.ShowDialog(this);

                if (canCreateNew)
                {
                    this.cdDAL = new DAL(dalPathDesign, false);
                }


            }
            //DESIGN TAB    Design Cruise +++++++++++++++            
            else if (ButtonSelect == 3)
            {
                if (prodFile)
                {
                    MessageBox.Show("Production File Found.\nCannot modify selection frequencies.\n", "Warning", MessageBoxButtons.OK);
                    return;
                }
                //MessageBox.Show("Design Cruise Selected", "Information");
                Cursor.Current = Cursors.WaitCursor;

                Design_Pages.Processing pDlg = new CruiseDesign.Design_Pages.Processing(this, dalPathCruise, reconExists, prodFile);
                pDlg.ShowDialog();

                Cursor.Current = this.Cursor;

                Design_Pages.DesignMain dmDlg = new CruiseDesign.Design_Pages.DesignMain(this, dalPathCruise);
                dmDlg.ShowDialog(this);



                // call DesignCruiseForm

            }
            //TOOLS TAB  Compare with Production Cruise +++++++++++++           
            else if (ButtonSelect == 4)
                MessageBox.Show("Compare Production Cruise with Design file./nFuture Enhancement.", "Information");

        }

        //Row Two  -------------------------------------------------------------------------------
        private void buttonRowTwo_Click(object sender, EventArgs e)
        {
            // Main form  
            if (ButtonSelect == 0)
            {

            }
            // FILE TAB - Open production cruise ++++++++++++++
            else if (ButtonSelect == 1)
            {
                //   Create new design database +++++++++++++++++
                //MessageBox.Show("Open Production Not Completed at this Time", "Information");
            }

            // SETUP TAB   Historical Setup +++++++++++++++++
            else if (ButtonSelect == 2)
            {

                if (prodFile)
                {
                    MessageBox.Show("Production File Found.\nCannot modify cruise design.\n", "Warning", MessageBoxButtons.OK);
                    return;
                }
                //MessageBox.Show("Historical Setup", "Information");
                //  Pass in dalPathDesign
                HistoricalSetupWizard hsDlg = new HistoricalSetupWizard(this, canCreateNew);
                hsDlg.Owner = this;
                hsDlg.ShowDialog(this);

                if (canCreateNew)
                {
                    this.cdDAL = new DAL(dalPathDesign, false);
                }

                buttonSetup.Enabled = true;
                buttonDesign.Enabled = true;
                buttonTools.Enabled = true;

            }
            //DESIGN TAB +++++++++++++++++
            else if (ButtonSelect == 3)
            {
                //MessageBox.Show("Add Additional Samples Selected", "Information");
                Cursor.Current = Cursors.WaitCursor;

                Design_Pages.Processing pDlg = new CruiseDesign.Design_Pages.Processing(this, dalPathCruise, reconExists, prodFile);

                if (errFlag == 1)
                {
                    Cursor.Current = this.Cursor;
                    return;
                }

                pDlg.ShowDialog();

                Cursor.Current = this.Cursor;


                ProductionDesign.ProductionDesignMain dmDlg = new CruiseDesign.ProductionDesign.ProductionDesignMain(this);
                dmDlg.ShowDialog(this);

            }
            //TOOLS TAB ++++++++++++++++++
            else if (ButtonSelect == 4)
                MessageBox.Show("Set Default Directories Selected", "Information");
        }

        //Row Three  -------------------------------------------------------------------------------
        private void buttonRowThree_Click(object sender, EventArgs e)
        {
            if (ButtonSelect == 0)
                MessageBox.Show("Deleted", "Information");
            //FILE TAB  Create New Cruise from Recon +++++++++++++++
            else if (ButtonSelect == 1)
            {
                WaitForm waitFrm = new WaitForm();

                if (openFileDialogCruise.ShowDialog() == DialogResult.OK)
                {
                    // check for cruise design database
                    Text = openFileDialogCruise.SafeFileName;
                    dalPathCruise = openFileDialogCruise.FileName;
                    //check version 3
                    // if crz3, look for process file
                    if (Path.GetExtension(dalPathCruise).Equals(".crz3", StringComparison.OrdinalIgnoreCase))
                    {
                        // if process file, change dalPathCruise to process file
                        if (doesProcessFileExistC())
                        {
                            dalPathCruise = dalPathProcess;
                            IsUsingV3File = true;
                        }
                        // if no process file, warning message and exit
                        else
                        {
                            MessageBox.Show("Cruise file not processed. Cannot continue.", "Warning");
                            buttonSetup.Enabled = false;
                            buttonDesign.Enabled = false;
                            buttonTools.Enabled = false;
                            return;
                        }
                    }
                    else
                    {
                        IsUsingV3File = false;
                    }
                    reconExists = true;
                    // create design database for recon
                    // does filename with .design extension exist
                    if (doesDesignFileExist())
                    {
                        canCreateNew = false;
                        if (!openDesignFile())
                        {
                            MessageBox.Show("Design file exists but unable to open the file", "Information");
                            buttonSetup.Enabled = false;
                            buttonDesign.Enabled = false;
                            buttonTools.Enabled = false;
                            return;
                        }
                        else
                        {
                            //MessageBox.Show("Design File Already Exists.", "Information");
                            buttonSetup.Enabled = true;
                            buttonDesign.Enabled = true;
                            buttonTools.Enabled = true;
                            return;
                        }
                    }
                    else
                    {
                        //Cursor.Current = Cursors.WaitCursor;
                        Cursor.Current = Cursors.WaitCursor;
                        waitFrm.Show();

                        canCreateNew = true;
                        //if (!openDesignFile())
                        //{
                        //   MessageBox.Show("Unable to create the design file", "Information");
                        //   return;
                        //}

                        Working wDlg = new Working(this, dalPathCruise, reconExists);
                        waitFrm.Close();
                        Cursor.Current = this.Cursor;

                        wDlg.ShowDialog();

                        buttonSetup.Enabled = true;
                        buttonDesign.Enabled = true;
                        buttonTools.Enabled = true;
                    }

                }
            }
            //SETUP TAB - Cost Setup ++++++++++++++++
            else if (ButtonSelect == 2)
            {
                // MessageBox.Show("Setup Costs Selected", "Information");
                CostSetupForm costDlg = new CostSetupForm(this);
                costDlg.ShowDialog();

            }
            // DESIGN TAB
            else if (ButtonSelect == 3)
                MessageBox.Show("", "Information");
            // TOOLS TAB
            else if (ButtonSelect == 4)
                MessageBox.Show("", "Information");

        }

        //Row Four -------------------------------------------------------------------------------
        private void buttonRowFour_Click(object sender, EventArgs e)
        {
            if (ButtonSelect == 0)
                MessageBox.Show("Deleted");
            //FILE TAB Create Cruise From Historical Data ++++++++++++
            else if (ButtonSelect == 1)
            {
                //MessageBox.Show("Create New Historical");
                if (saveFileDialogDesign.ShowDialog() == DialogResult.OK)
                {
                    //set title bar with file name
                    Text = saveFileDialogDesign.FileName;
                    dalPathDesign = saveFileDialogDesign.FileName;
                    canCreateNew = true;
                    if (!openDesignFile())
                    {
                        MessageBox.Show("Unable to create the design file", "Information");
                        return;
                    }

                    // open SaleSetupPage
                    SaleSetupPage sDlg = new SaleSetupPage(this);
                    sDlg.ShowDialog();

                    canCreateNew = false;
                    reconExists = false;
                    buttonSetup.Enabled = true;
                    buttonDesign.Enabled = true;
                    buttonTools.Enabled = true;
                }
            }
            //SETUP TAB ++++++++++
            else if (ButtonSelect == 2)
                MessageBox.Show("", "Information");
            //DESIGN TAB +++++++++++
            else if (ButtonSelect == 3)
                MessageBox.Show("", "Information");
            //TOOLS TAB +++++++++++
            else if (ButtonSelect == 4)
                MessageBox.Show("", "Information");

        }

        private void openFileDialogCruise_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void buttonIcon_Click(object sender, EventArgs e)
        {

        }

        //FILE TAB
        private void buttonFile_Click(object sender, EventArgs e)
        {
            TitleLabel.Text = "Cruise Design Program";
            labelRowOne.Text = "Open Existing File";
            labelRowOne.Visible = true;

            labelRowTwo.Text = "";
            labelRowTwo.Visible = false;

            labelRowThree.Text = "Create New From Recon File";
            labelRowThree.Visible = true;
            labelRowFour.Text = "Create New From Historical Data";
            labelRowFour.Visible = true;


            buttonRowOne.Visible = true;
            buttonRowOne.Image = Properties.Resources.Cruisedesign48;
            buttonRowTwo.Visible = false;
            buttonRowTwo.Image = Properties.Resources.opencruise48a;

            buttonRowThree.Image = Properties.Resources.openrecon48a;
            buttonRowThree.Visible = true;
            buttonRowFour.Image = Properties.Resources.opencruise48a; ;
            buttonRowFour.Visible = true;

            ButtonSelect = 1;
        }

        //SETUP TAB
        private void buttonSetup_Click(object sender, EventArgs e)
        {
            TitleLabel.Text = "Cruise Design Program - Establish";
            labelRowOne.Text = "Design Strata From Recon Data";
            labelRowOne.Visible = true;
            labelRowTwo.Text = "Design Strata From Historical Data";
            labelRowTwo.Visible = true;
            labelRowThree.Text = "Setup Costs";
            labelRowThree.Visible = true;
            labelRowFour.Text = "";
            labelRowFour.Visible = false;

            buttonRowOne.Visible = true;
            buttonRowOne.Image = Properties.Resources.recon48;
            //if (reconExists)
            buttonRowOne.Enabled = true;
            //else
            //    buttonRowOne.Enabled = false;
            buttonRowTwo.Visible = true;
            buttonRowTwo.Image = Properties.Resources.woodtree48;
            buttonRowThree.Visible = true;
            buttonRowThree.Image = Properties.Resources.dollar48;
            buttonRowFour.Visible = false;

            ButtonSelect = 2;
        }

        //DESIGN TAB
        private void buttonDesign_Click(object sender, EventArgs e)
        {
            TitleLabel.Text = "Cruise Design Program - Design";
            labelRowOne.Text = "Design Cruise";
            labelRowOne.Visible = true;
            labelRowTwo.Text = "Determine Additional Samples";
            labelRowTwo.Visible = true;
            labelRowThree.Text = "";
            labelRowThree.Visible = false;
            labelRowFour.Text = "";
            labelRowFour.Visible = false;

            buttonRowOne.Visible = true;
            buttonRowOne.Image = Properties.Resources.Design48;
            buttonRowTwo.Visible = true;
            buttonRowTwo.Image = Properties.Resources.AddForm;
            buttonRowThree.Visible = false;
            buttonRowFour.Visible = false;

            ButtonSelect = 3;
        }

        //TOOLS TAB
        private void buttonTools_Click(object sender, EventArgs e)
        {
            TitleLabel.Text = "Cruise Design Program - Tools";

            labelRowOne.Text = "Compare Design with Production Cruise";
            labelRowOne.Visible = true;
            labelRowTwo.Text = "";
            labelRowTwo.Visible = false;
            labelRowThree.Text = "";
            labelRowThree.Visible = false;
            labelRowFour.Text = "";
            labelRowFour.Visible = false;

            buttonRowOne.Visible = true;
            buttonRowOne.Image = Properties.Resources.compare48;
            buttonRowTwo.Visible = false;
            buttonRowThree.Visible = false;
            buttonRowFour.Visible = false;

            ButtonSelect = 4;
        }

        private bool doesReconFileExist()
        {
            //opened a design file, does the recon file exist? 
            dalPathCruise = dalPathDesign;
            dalPathCruise = Path.ChangeExtension(dalPathCruise, ".cruise");
            if (File.Exists(dalPathCruise))
            {
                return (true);
            }
            else
            {
                dalPathCruise = Path.ChangeExtension(dalPathCruise, ".process");
                if (File.Exists(dalPathDesign))
                    return (true);
                else
                    return (false);
            }
        }
        private bool doesDesignFileExist()
        {
            //opened a cruise file, does the design file exist? 
            dalPathDesign = dalPathCruise;
            dalPathDesign = Path.ChangeExtension(dalPathDesign, ".design");
            return File.Exists(dalPathDesign);

        }
        private bool doesProcessFileExistC()
        {
            dalPathProcess = dalPathCruise;
            dalPathProcess = Path.ChangeExtension(dalPathCruise, ".process");
            return File.Exists(dalPathProcess);
        }
        private bool doesProcessFileExistD()
        {
            dalPathProcess = dalPathDesign;
            dalPathProcess = Path.ChangeExtension(dalPathDesign, ".process");
            return File.Exists(dalPathProcess);
        }

        public bool openDesignFile()
        {
            //MessageBox.Show("create design file", "Information");

            // create or open DAL
            try
            {
                this.cdDAL = new DAL(dalPathDesign, canCreateNew);
                return (true);
            }
            catch (System.IO.IOException e1)
            {
                //Logger.Log.E(e1);
                return (false);
            }
            catch (System.Exception e1)
            {
                //Logger.Log.E(e1);
                return (false);
            }

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tableLayoutPanelFile_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
