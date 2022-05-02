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
using CruiseDAL;

namespace CruiseDesign
{
    public partial class CruiseDesignMain : Form
    {
        public DAL cdDAL { get; set; }
        int ButtonSelect = 1;
        public string dalPathCruise;
        public string dalPathDesign;
        bool reconExists = true;
        bool canCreateNew = false;

        public CruiseDesignMain(string[] args)
        {
            InitializeComponent();
            if (args.Length != 0)
            {
               Text = Convert.ToString(args[0]);
               dalPathDesign = Convert.ToString(args[0]);
               // does .cruise file exist
               if (doesFileExist("cruise"))
               {
                  // check for historical design in Sale - Purpose
                  reconExists = true;
               }
               else
                  reconExists = false;
               
               canCreateNew = false;

               if (openDesignFile())
               {
                  buttonSetup.Enabled = true;
                  buttonDesign.Enabled = true;
                  buttonTools.Enabled = true;
               }
               else
                  MessageBox.Show("Unable to open the design file", "Information");
            }
        }

        private void CruiseDesignMain_Load(object sender, EventArgs e)
        {
        
        }
//Row One  -------------------------------------------------------------------------------
        private void buttonRowOne_Click(object sender, EventArgs e)
        {
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
                  // check for design file vs production cruise
                  if(Text.Contains(".cruise"))
                  {
                     MessageBox.Show("Open Production Not Completed at this Time", "Information");
                     return;
                  }

                  // does .cruise file exist
                  if (doesFileExist("cruise"))
                  {
                     // check for historical design in Sale - Purpose
                     reconExists = true;
                  }
                  else
                     reconExists = false;
                  canCreateNew = false;

                  if (openDesignFile())
                  {
                     buttonSetup.Enabled = true;
                     buttonDesign.Enabled = true;
                     buttonTools.Enabled = true;
                  }
                  else
                     MessageBox.Show("Unable to open the design file", "Information");

               }
            }
// SETUP TAB    Setup Using Recon  +++++++++++++       
            else if (ButtonSelect == 2)
            {
               StrataSetupWizard strDlg = new StrataSetupWizard(this, dalPathCruise, reconExists);
               strDlg.Owner = this;

               //        strDlg.dalFile = dalPath;
               strDlg.ShowDialog(this);
            }
//DESIGN TAB    Design Cruise +++++++++++++++            
            else if (ButtonSelect == 3)
            {
               //MessageBox.Show("Design Cruise Selected", "Information");
               Cursor.Current = Cursors.WaitCursor;

               Design_Pages.Processing pDlg = new CruiseDesign.Design_Pages.Processing(this, dalPathCruise, reconExists);
               pDlg.ShowDialog();

               Cursor.Current = this.Cursor;

               Design_Pages.DesignMain dmDlg = new CruiseDesign.Design_Pages.DesignMain(this, dalPathCruise);
               dmDlg.ShowDialog(this);

               // call DesignCruiseForm

            }
//TOOLS TAB  Compare with Production Cruise +++++++++++++           
            else if (ButtonSelect == 4)
               MessageBox.Show("Compare with Production Selected", "Information");

        }

//Row Two  -------------------------------------------------------------------------------
        private void buttonRowTwo_Click(object sender, EventArgs e)
        {
           // Main form  
           if (ButtonSelect == 0)
            {
              // MessageBox.Show("Deleted", "Information");
              //      Open Historical File +++++++++++++
            }
// FILE TAB - Open production cruise ++++++++++++++
           else if (ButtonSelect == 1)
            {
               //   Create new design database +++++++++++++++++
               MessageBox.Show("Open Production Not Completed at this Time", "Information");
               //buttonSetup.Enabled = true;
               //buttonDesign.Enabled = true;
               //buttonTools.Enabled = true;
            }  

// SETUP TAB   Historical Setup +++++++++++++++++
            else if (ButtonSelect == 2)
            {
               //MessageBox.Show("Historical Setup", "Information");
               //  Pass in dalPathDesign
               HistoricalSetupWizard hsDlg = new HistoricalSetupWizard(this,canCreateNew);
               hsDlg.Owner = this; 
               hsDlg.ShowDialog(this);
               buttonSetup.Enabled = true;
               buttonDesign.Enabled = true;
               buttonTools.Enabled = true;
               
            }
//DESIGN TAB +++++++++++++++++
           else if (ButtonSelect == 3)
                MessageBox.Show("Add Additional Samples Selected", "Information");
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
               if (openFileDialogCruise.ShowDialog() == DialogResult.OK)
               {
                  // check for cruise design database
                  Text = openFileDialogCruise.SafeFileName;
                  dalPathCruise = openFileDialogCruise.FileName;
                  reconExists = true;
                  // create design database for recon
                  // does filename with .design extension exist
                  if (doesFileExist("design"))
                  {
                     canCreateNew = false;
                     if (!openDesignFile())
                     {
                        MessageBox.Show("Unable to open the design file", "Information");
                        return;
                     }
                  }
                  else
                  {
                     //Cursor.Current = Cursors.WaitCursor;
                     canCreateNew = true;
                     if (!openDesignFile())
                     {
                        MessageBox.Show("Unable to create the design file", "Information");
                        return;
                     }
                     
                     Working wDlg = new Working(this, dalPathCruise, reconExists);
                     wDlg.ShowDialog();

                     buttonSetup.Enabled = true;
                     buttonDesign.Enabled = true;
                     buttonTools.Enabled = true;
                     //Cursor.Current = this.Cursor;
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
    
        private bool doesFileExist(string fileType)
        {
           //opened a design file, does the recon file exist? 
           if (fileType == "cruise")
            {
                
                dalPathCruise = dalPathDesign;
                dalPathCruise = dalPathCruise.Replace(".design", ".cruise");
                if (File.Exists(dalPathCruise))
                    return (true);
                else
                    return (false);
            }
           // opened a cruise file, does design file already exist?
            else if (fileType == "design")
            {
                dalPathDesign = dalPathCruise;
                dalPathDesign = dalPathDesign.Replace(".cruise",".design");
                if (File.Exists(dalPathDesign))
                    return (true);
                else
                    return (false);
            }
            else
                return(false);
        }

        private bool openDesignFile()
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
               Logger.Log.E(e1);
               return (false);
            }
            catch (System.Exception e1)
            {
               Logger.Log.E(e1);
               return (false);
            }

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
           Application.Exit();
        }

    }
}
