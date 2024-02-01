using CruiseDAL;
using CruiseDAL.DataObjects;
using CruiseDesign.Design_Pages;
using CruiseDesign.Historical_setup;
using CruiseDesign.ProductionDesign;
using CruiseDesign.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
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

        public IDialogService DialogService { get; }
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

        public string[] OpenArgs { get; }

        protected CruiseDesignMain()
        {
            InitializeComponent();
        }

        public CruiseDesignMain(string[] args, IServiceProvider serviceProvider, ILogger<CruiseDesignMain> logger, ICruiseDesignFileContextProvider fileContextProvider, IDialogService dialogService)
            : this(serviceProvider, logger, fileContextProvider, dialogService)
        {
            OpenArgs = args;
        }

        protected CruiseDesignMain(IServiceProvider serviceProvider, ILogger<CruiseDesignMain> logger, ICruiseDesignFileContextProvider fileContextProvider, IDialogService dialogService)
            : this()
        {
            DialogService = dialogService;
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            FileContextProvider = fileContextProvider ?? throw new ArgumentNullException(nameof(fileContextProvider));

            var version = Assembly.GetEntryAssembly()?.GetName().Version?.ToString(3);
            this.Text = $"Cruise Design {version}";

            Logger.LogInformation("Program Started");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var args = OpenArgs;
            if (args != null && args.Length != 0)
            {
                var filePath = Convert.ToString(args[0]);
                if (CruiseDesignFileContext.EnsurePathValid(filePath, Logger, DialogService)
                && CruiseDesignFileContext.EnsurePathExistsAndCanWrite(filePath, Logger, DialogService))
                {
                    var newFileContext = new CruiseDesignFileContext()
                    { DesignFilePath = filePath };

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
            CreateNewFileFromHistorical(path, FileContextProvider, Logger, DialogService);
        }

        public static void CreateNewFileFromHistorical(string path, ICruiseDesignFileContextProvider fileContextProvider, ILogger logger, IDialogService dialogService)
        {
            var newFileContext = new CruiseDesignFileContext();

            if (!newFileContext.OpenDesignFile(path, logger, canCreateNew: true))
            {
                dialogService.ShowMessage("Unable to create the design file", "Information");
                return;
            }

            //need to set file context for dialog
            fileContextProvider.CurrentFileContext = newFileContext;
            if (dialogService.ShowDialog<SaleSetupPage>() != DialogResult.OK)
            {
                // user exited dialog without clicking finish
                fileContextProvider.CurrentFileContext = null;
            }
        }

        public void CreateNewFileFromRecon(string path)
        {
            Cursor.Current = Cursors.WaitCursor;

            CreateNewFromRecon(path, FileContextProvider, Logger, DialogService);
            Cursor.Current = this.Cursor;
        }

        public static void CreateNewFromRecon(string path, ICruiseDesignFileContextProvider fileContextProvider, ILogger logger, IDialogService dialogService)
        {
            var fileExtention = Path.GetExtension(path).ToLowerInvariant();

            string reconFilePath = null;

            // check for cruise design database
            //check version 3
            if (fileExtention == ".crz3")
            {
                // if crz3, look for process file
                var processFilePath = CruiseDesignFileContext.GetProcessFilePathFromV3Cruise(path);
                if (File.Exists(processFilePath))
                {
                    reconFilePath = processFilePath;
                }
                else
                {
                    dialogService.ShowMessage("Cruise file not processed. Cannot continue.", "Warning");
                    return;
                }
            }
            else if (fileExtention == ".cruise")
            {
                reconFilePath = path;
            }
            else
            {
                dialogService.ShowMessage("Invalid File Extension", "Warning");
                return;
            }

            var designPath = CruiseDesignFileContext.GetDesignFilePathFromRecon(reconFilePath);
            if (!File.Exists(designPath))
            {
                DAL reconDb;
                try
                {
                    reconDb = new DAL(reconFilePath);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Unable To Recon File:{reconFilePath}", reconFilePath);
                    return;
                }

                DAL designDb;
                try
                {
                    designDb = new DAL(designPath, true);
                }
                catch (Exception ex)
                {

                    logger.LogError(ex, "Unable To Create Design File:{designPath}", designPath);
                    reconDb?.Dispose();
                    return;
                }

                try
                {
                    CopyReconToDesign(designDb, reconDb);
                }
                catch(Exception ex)
                {
                    logger.LogError(ex, "Error Copying Recon Data To Design File");
                    File.Delete(designPath);
                    return;
                }
                finally
                {
                    designDb?.Dispose();
                    reconDb?.Dispose();
                }
            }

            OpenExistingDesignFile(designPath, fileContextProvider, logger, dialogService);
        }

        public static void CopyReconToDesign(DAL cdDAL, DAL rDAL)
        {
            cdDAL.BeginTransaction();
            try
            {
                // copy Sale table
                var sale = rDAL.From<SaleDO>().Read().ToList();
                foreach (SaleDO sl in sale)
                {
                    sl.DAL = cdDAL;
                    sl.Save();
                }

                //copy CuttingUnit table
                foreach (CuttingUnitDO fld in rDAL.From<CuttingUnitDO>().Query())
                {
                    cdDAL.Insert(fld, "CuttingUnit", Backpack.SqlBuilder.OnConflictOption.Replace);
                }

                //copy TreeDefaultValues table
                foreach (TreeDefaultValueDO fld in rDAL.From<TreeDefaultValueDO>().Query())
                {
                    cdDAL.Insert(fld, "TreeDefaultValue", Backpack.SqlBuilder.OnConflictOption.Replace);
                }

                //copy globals table
                foreach (GlobalsDO fld in rDAL.From<GlobalsDO>().Where("Block != 'Database'").Query())
                {
                    cdDAL.Insert(fld, "Globals", Backpack.SqlBuilder.OnConflictOption.Replace);
                }

                //copy logfieldsetupdefault
                foreach (LogFieldSetupDefaultDO fld in rDAL.From<LogFieldSetupDefaultDO>().Query())
                {
                    cdDAL.Insert(fld, "LogfieldSetupDefault", Backpack.SqlBuilder.OnConflictOption.Replace);
                }

                //copy messagelog
                foreach (MessageLogDO fld in rDAL.From<MessageLogDO>().Query())
                {
                    cdDAL.Insert(fld, "MessageLog", Backpack.SqlBuilder.OnConflictOption.Replace);
                }

                //copy reports
                foreach (ReportsDO fld in rDAL.From<ReportsDO>().Query())
                {
                    cdDAL.Insert(fld, "Reports", Backpack.SqlBuilder.OnConflictOption.Replace);
                }

                //copy treefieldsetupdefault
                foreach (TreeFieldSetupDefaultDO fld in rDAL.From<TreeFieldSetupDefaultDO>().Query())
                {
                    cdDAL.Insert(fld, "TreeFieldSetupDefault", Backpack.SqlBuilder.OnConflictOption.Replace);
                }

                //copy volumeequations
                foreach (VolumeEquationDO fld in rDAL.From<VolumeEquationDO>().Query())
                {
                    cdDAL.Insert(fld, "VolumeEquation", Backpack.SqlBuilder.OnConflictOption.Replace);
                }

                //copy treeauditvalue
                foreach (TreeAuditValueDO fld in rDAL.From<TreeAuditValueDO>().Query())
                {
                    cdDAL.Insert(fld, "TreeAuditValue", Backpack.SqlBuilder.OnConflictOption.Replace);
                }

                //copy treedefaultvaluetreeauditvalue
                foreach (TreeDefaultValueTreeAuditValueDO fld in rDAL.From<TreeDefaultValueTreeAuditValueDO>().Query())
                {
                    cdDAL.Insert(fld, "TreeDefaultValueTreeAuditValue", Backpack.SqlBuilder.OnConflictOption.Replace);
                }

                //copy tally
                foreach (TallyDO fld in rDAL.From<TallyDO>().Query())
                {
                    cdDAL.Insert(fld, "Tally", Backpack.SqlBuilder.OnConflictOption.Replace);
                }

                foreach (var lm in rDAL.From<LogMatrixDO>().Query())
                {
                    cdDAL.Insert(lm, option: Backpack.SqlBuilder.OnConflictOption.Replace);
                }

                cdDAL.LogMessage("Copied Data From Recon File: " + rDAL.Path);

                cdDAL.CommitTransaction();
            }
            catch
            {
                cdDAL.RollbackTransaction();
                throw;
            }
        }

        public void OpenExistingDesignFile(string path)
        {
            OpenExistingDesignFile(path, FileContextProvider, Logger, DialogService);
        }

        public static void OpenExistingDesignFile(string path, ICruiseDesignFileContextProvider fileContextProvider, ILogger logger, IDialogService dialogService)
        {
            if(!CruiseDesignFileContext.EnsurePathValid(path, logger, dialogService)
                && !CruiseDesignFileContext.EnsurePathExistsAndCanWrite(path, logger, dialogService))
            {
                return;
            }

            //if(File.GetAttributes(path).HasFlag(FileAttributes.ReadOnly))
            //{ dialogService.ShowMessage("Selected File Is Read Only"); }

            var fileExtention = Path.GetExtension(path).ToLower();

            var newFileContext = new CruiseDesignFileContext();

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
                }
                else
                {
                    dialogService.ShowMessage("Cruise file not processed. Cannot continue.", "Warning");
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
                logger.LogWarning(message);
                dialogService.ShowMessage(message, "Warning");
                fileContextProvider.CurrentFileContext = null;
                return;
            }

            if (newFileContext.OpenDesignFile(logger))
            {
                fileContextProvider.CurrentFileContext = newFileContext;
            }
            else
            {
                fileContextProvider.CurrentFileContext = null;
                dialogService.ShowMessage("Unable to open the file.", "Information");
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