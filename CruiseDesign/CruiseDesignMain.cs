using CruiseDesign.Design_Pages;
using CruiseDesign.Historical_setup;
using CruiseDesign.ProductionDesign;
using CruiseDesign.Services;
using CruiseDesign.Strata_setup;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace CruiseDesign
{
    public partial class CruiseDesignMain : Form
    {
        private ICruiseDesignFileContextProvider _fileContextProvider;

        private int ButtonSelect { get; set; } = 1;

        protected bool IsProductionFile => FileContext.IsProductionFile;
        public CruiseDesignFileContext FileContext => FileContextProvider.CurrentFileContext;
        
        public IServiceProvider ServiceProvider { get; }
        public ILogger Logger { get; }
        public ICruiseDesignFileContextProvider FileContextProvider
        {
            get => _fileContextProvider;
            protected set
            {
                if (_fileContextProvider != null)
                {
                    _fileContextProvider.FileContextChanged -= OnFileContextChanged;
                }
                _fileContextProvider = value;
                if (value != null)
                {
                    value.FileContextChanged += OnFileContextChanged;
                }
            }
        }

        private void OnFileContextChanged(object sender, EventArgs e)
        {
            var fileContextProvider = (ICruiseDesignFileContextProvider)sender;
            var newFileContext = fileContextProvider.CurrentFileContext;
            if (newFileContext != null)
            {
                if (newFileContext.DesignFilePath != null)
                {
                    Text = Path.GetFileName(newFileContext.DesignFilePath);

                    buttonSetup.Enabled = true;
                    buttonDesign.Enabled = true;
                    buttonTools.Enabled = true;
                }
                else if (newFileContext.ReconFilePath != null)
                {
                    Text = Path.GetFileName(newFileContext.ReconFilePath);

                    buttonSetup.Enabled = true;
                    buttonDesign.Enabled = true;
                    buttonTools.Enabled = true;
                }
            }
        }

        protected CruiseDesignMain()
        {
            InitializeComponent();
        }

        public CruiseDesignMain(string[] args, IServiceProvider serviceProvider, ILogger<CruiseDesignMain> logger, ICruiseDesignFileContextProvider fileContextProvider)
            : this(serviceProvider, logger, fileContextProvider)
        {

            // to update version change the Version property in the project file
            var version = Assembly.GetEntryAssembly().GetName().Version?.ToString(3);
            this.Text = $"Cruise Design {version}";

            if (args.Length != 0)
            {
                var newFileContext = new CruiseDesignFileContext()
                { DesignFilePath = Convert.ToString(args[0]) };

                newFileContext.SetReconFilePathFromDesign();

                if (newFileContext.OpenDesignFile(Logger))
                {
                    FileContextProvider.CurrentFileContext = newFileContext;
                }
                else
                {
                    MessageBox.Show("Unable to open the design file", "Information");
                }
            }

            Logger.LogInformation("Program Started");
        }

        public CruiseDesignMain(IServiceProvider serviceProvider, ILogger<CruiseDesignMain> logger, ICruiseDesignFileContextProvider fileContextProvider)
            : this()
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            FileContextProvider = fileContextProvider ?? throw new ArgumentNullException(nameof(fileContextProvider));

            var version = Assembly.GetEntryAssembly().GetName().Version?.ToString(3);
            this.Text = $"Cruise Design {version}";
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
                    OpenExistingDesignFile(openFileDialogDesign.FileName);
                }
            }
            // SETUP TAB    Setup Using Recon  +++++++++++++
            else if (ButtonSelect == 2)
            {
                if (IsProductionFile)
                {
                    MessageBox.Show("Production File Found.\nNo changes to the current design allowed.\n", "Warning", MessageBoxButtons.OK);
                    return;
                }

                using StrataSetupWizard strDlg = ServiceProvider.GetRequiredService<StrataSetupWizard>();
                strDlg.ShowDialog(this);
            }
            //DESIGN TAB    Design Cruise +++++++++++++++
            else if (ButtonSelect == 3)
            {
                if (IsProductionFile)
                {
                    MessageBox.Show("Production File Found.\nCannot modify selection frequencies.\n", "Warning", MessageBoxButtons.OK);
                    return;
                }
                //MessageBox.Show("Design Cruise Selected", "Information");
                Cursor.Current = Cursors.WaitCursor;

                Processing pDlg = ServiceProvider.GetRequiredService<Processing>();
                pDlg.Show(this);
                var success = FileContext.ProcessFile();
                pDlg.Close();

                if (!success)
                {
                    MessageBox.Show("Recon File has not been Processsed.\nStatistics set to default values.");
                }

                Cursor.Current = this.Cursor;

                using DesignMain dmDlg = ServiceProvider.GetRequiredService<DesignMain>();
                dmDlg.ShowDialog(this);
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
                if (IsProductionFile)
                {
                    MessageBox.Show("Production File Found.\nCannot modify cruise design.\n", "Warning", MessageBoxButtons.OK);
                    return;
                }
                //MessageBox.Show("Historical Setup", "Information");
                //  Pass in dalPathDesign
                HistoricalSetupWizard hsDlg = ServiceProvider.GetRequiredService<HistoricalSetupWizard>();
                hsDlg.ShowDialog(this);
            }
            //DESIGN TAB +++++++++++++++++
            else if (ButtonSelect == 3)
            {
                //MessageBox.Show("Add Additional Samples Selected", "Information");
                Cursor.Current = Cursors.WaitCursor;

                if (FileContext.IsProductionFile && !FileContext.CheckIsDesignFileProcessed())
                {
                    MessageBox.Show("Cruise Not Processed. Please Process Cruise Before Continuing.", "Warning");
                    return;
                }

                Processing pDlg = ServiceProvider.GetRequiredService<Processing>();
                pDlg.Show(this);
                var success = FileContext.ProcessFile();
                pDlg.Close();

                if (!success)
                {
                    MessageBox.Show("Recon File has not been Processsed.\nStatistics set to default values.");
                }

                Cursor.Current = this.Cursor;

                ProductionDesign.ProductionDesignMain dmDlg = ServiceProvider.GetRequiredService<ProductionDesignMain>();
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
                if (openFileDialogCruise.ShowDialog() == DialogResult.OK)
                {
                    CreateNewFileFromRecon(openFileDialogCruise.FileName);
                }
            }
            //SETUP TAB - Cost Setup ++++++++++++++++
            else if (ButtonSelect == 2)
            {
                // MessageBox.Show("Setup Costs Selected", "Information");
                using CostSetupForm costDlg = ServiceProvider.GetRequiredService<CostSetupForm>();
                costDlg.ShowDialog(this);
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
                    CreateNewFileFromHistorical(saveFileDialogDesign.FileName);
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

        public void CreateNewFileFromHistorical(string path)
        {
            var newFileContext = new CruiseDesignFileContext();

            newFileContext.DesignFilePath = path;

            if (!newFileContext.OpenDesignFile(Logger, canCreateNew: true))
            {
                MessageBox.Show("Unable to create the design file", "Information");
                return;
            }

            FileContextProvider.CurrentFileContext = newFileContext;
            using var setupPage = ServiceProvider.GetRequiredService<SaleSetupPage>();
            if (setupPage.ShowDialog(this) != DialogResult.OK)
            {
                // user exited dialog without clicking finish
                FileContextProvider.CurrentFileContext = null;
            }
        }

        public void CreateNewFileFromRecon(string path)
        {
            var newFileContext = new CruiseDesignFileContext();

            // check for cruise design database
            //check version 3
            if (Path.GetExtension(path).Equals(".crz3", StringComparison.OrdinalIgnoreCase))
            {
                newFileContext.V3FilePath = path;
                // if crz3, look for process file
                if (newFileContext.SetProcessFilePathFromV3Cruise())
                {
                    newFileContext.ReconFilePath = newFileContext.ProcessFilePath;
                }
                else
                {
                    MessageBox.Show("Cruise file not processed. Cannot continue.", "Warning");
                    return;
                }
            }
            else
            {
                newFileContext.ReconFilePath = path;
            }

            // create design database for recon
            // does filename with .design extension exist
            if (newFileContext.SetDesignFilePathFromRecon(true))
            {
                if (newFileContext.OpenDesignFile(Logger))
                {
                    FileContextProvider.CurrentFileContext = newFileContext;
                    return;
                }
                else
                {
                    MessageBox.Show("Design file exists but unable to open the file", "Information");
                    FileContextProvider.CurrentFileContext = null;
                    return;
                }
            }
            else
            {
                FileContextProvider.CurrentFileContext = newFileContext;

                using WaitForm waitFrm = new WaitForm();
                //Cursor.Current = Cursors.WaitCursor;
                Cursor.Current = Cursors.WaitCursor;
                waitFrm.Show();

                // copy limited data from cruise to design file
                Working wDlg = ServiceProvider.GetRequiredService<Working>();
                waitFrm.Close();
                Cursor.Current = this.Cursor;

                wDlg.ShowDialog();
            }
        }

        public void OpenExistingDesignFile(string path)
        {
            var fileExtention = Path.GetExtension(path).ToLower();

            var newFileContext = new CruiseDesignFileContext();
            bool canCreateNew = false;

            // check for design file vs production cruise
            if (fileExtention is ".cruise")
            {
                newFileContext.DesignFilePath = path;
                newFileContext.IsProductionFile = true;
            }
            else if (fileExtention is ".crz3")
            {
                newFileContext.V3FilePath = path;
                if (newFileContext.SetProcessFilePathFromV3Cruise())
                {

                    newFileContext.DesignFilePath = newFileContext.ProcessFilePath;
                    newFileContext.IsProductionFile = true;
                    canCreateNew = false;
                }
                else
                {
                    MessageBox.Show("Cruise file not processed. Cannot continue.", "Warning");
                    return;
                }
            }
            else if (fileExtention == ".design")
            {
                newFileContext.DesignFilePath = path;
                newFileContext.SetV3FilePathFromDesignFilePath();
                newFileContext.SetReconFilePathFromDesign();
            }
            else
            {
                var message = "Unable to open file. Unrecognized extension";
                Logger.LogWarning(message);
                MessageBox.Show(message, "Information");
                FileContextProvider.CurrentFileContext = null;
                return;
            }

            if (newFileContext.OpenDesignFile(Logger, canCreateNew: canCreateNew))
            {
                FileContextProvider.CurrentFileContext = newFileContext;
            }
            else
            {
                FileContextProvider.CurrentFileContext = null;
                MessageBox.Show("Unable to open the file.", "Information");
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