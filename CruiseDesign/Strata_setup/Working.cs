using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CruiseDAL;
using CruiseDAL.DataObjects;
using CruiseDesign.Services;
using FMSC.ORM.Core.SQL;
using Microsoft.Extensions.Logging;

namespace CruiseDesign.Strata_setup
{

    public partial class Working : Form
    {
        public ILogger Logger { get; protected set; }

        struct dataFiles
        {
            public DAL cdDAL { get; set; }
            public string rFile;
        };
        dataFiles df;

        protected Working()
        {
            InitializeComponent();
        }

        public Working(ICruiseDesignFileContextProvider fileContextProvider, ILogger logger)
            : this()
        {
            var fileContext = fileContextProvider.CurrentFileContext;
            Logger = logger;

            if (!fileContext.DoesReconExist)
            {
                Finish(true);
            }
            else
            {
                if (!fileContext.OpenDesignFile(logger))
                {
                    MessageBox.Show("Unable to create the design file", "Information");
                }
                else
                {
                    backgroundWorker1.RunWorkerAsync(fileContext);
                }
            }
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            createNewDatabase((CruiseDesignFileContext)e.Argument);
        }

        private void createNewDatabase(CruiseDesignFileContext fileContext)
        {
            try
            {
                using var rDAL = new DAL(fileContext.ReconFilePath);
                copyTablesToDesign(fileContext.DesignDb, rDAL);
            }

            catch (System.IO.IOException ie)
            {
                Logger.LogError(ie, "");
            }
            catch (System.Exception ie)
            {
                Logger.LogError(ie, "");
            }
        }

        private void copyTablesToDesign(DAL cdDAL, DAL rDAL)
        {
            // copy Sale table
            //       cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.SALE._NAME, null, OnConflictOption.Ignore);
            var sale = rDAL.From<SaleDO>().Read().ToList();
            foreach (SaleDO sl in sale)
            {
                sl.DAL = cdDAL;
                sl.Save();
            }

            //copy CuttingUnit table
            //cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.CUTTINGUNIT._NAME, null, OnConflictOption.Ignore);
            foreach (CuttingUnitDO fld in rDAL.From<CuttingUnitDO>().Query())
            {
                cdDAL.Insert(fld, "CuttingUnit", Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy TreeDefaultValues table
            //cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.TREEDEFAULTVALUE._NAME, null, OnConflictOption.Ignore);
            foreach (TreeDefaultValueDO fld in rDAL.From<TreeDefaultValueDO>().Query())
            {
                cdDAL.Insert(fld, "TreeDefaultValue", Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy globals table
            //cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.GLOBALS._NAME, null, OnConflictOption.Ignore);
            foreach (GlobalsDO fld in rDAL.From<GlobalsDO>().Query())
            {
                cdDAL.Insert(fld, "Globals", Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy logfieldsetupdefault
            //cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.LOGFIELDSETUPDEFAULT._NAME, null, OnConflictOption.Ignore);
            foreach (LogFieldSetupDefaultDO fld in rDAL.From<LogFieldSetupDefaultDO>().Query())
            {
                cdDAL.Insert(fld, "LogfieldSetupDefault", Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy logfieldsetup
            //cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.LOGFIELDSETUP._NAME, null, OnConflictOption.Ignore);
            //copy messagelog
            //cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.MESSAGELOG._NAME, null, OnConflictOption.Ignore);
            foreach (MessageLogDO fld in rDAL.From<MessageLogDO>().Query())
            {
                cdDAL.Insert(fld, "MessageLog", Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy reports
            //cdDAL.DirectCopy(rDAL, "Reports", null, OnConflictOption.Ignore);
            foreach (ReportsDO fld in rDAL.From<ReportsDO>().Query())
            {
                cdDAL.Insert(fld, "Reports", Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy treefieldsetupdefault
            //cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.TREEFIELDSETUPDEFAULT._NAME, null, OnConflictOption.Ignore);
            foreach (TreeFieldSetupDefaultDO fld in rDAL.From<TreeFieldSetupDefaultDO>().Query())
            {
                cdDAL.Insert(fld, "TreeFieldSetupDefault", Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy treefieldsetup
            //cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.TREEFIELDSETUP._NAME, null, OnConflictOption.Ignore);
            //copy volumeequations
            //cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.VOLUMEEQUATION._NAME, null, OnConflictOption.Ignore);
            foreach (VolumeEquationDO fld in rDAL.From<VolumeEquationDO>().Query())
            {
                cdDAL.Insert(fld, "VolumeEquation", Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy treeauditvalue
            //cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.TREEAUDITVALUE._NAME, null, OnConflictOption.Ignore);
            foreach (TreeAuditValueDO fld in rDAL.From<TreeAuditValueDO>().Query())
            {
                cdDAL.Insert(fld, "TreeAuditValue", Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy treedefaultvaluetreeauditvalue
            //cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.TREEDEFAULTVALUETREEAUDITVALUE._NAME, null, OnConflictOption.Ignore);
            foreach (TreeDefaultValueTreeAuditValueDO fld in rDAL.From<TreeDefaultValueTreeAuditValueDO>().Query())
            {
                cdDAL.Insert(fld, "TreeDefaultValueTreeAuditValue", Backpack.SqlBuilder.OnConflictOption.Replace);
            }
            //copy tally
            //cdDAL.DirectCopy(rDAL, CruiseDAL.Schema.TALLY._NAME, null, OnConflictOption.Ignore);
            foreach (TallyDO fld in rDAL.From<TallyDO>().Query())
            {
                cdDAL.Insert(fld, "Tally", Backpack.SqlBuilder.OnConflictOption.Replace);
            }

            foreach (var lm in rDAL.From<LogMatrixDO>().Query())
            {
                cdDAL.Insert(lm, option: Backpack.SqlBuilder.OnConflictOption.Replace);
            }

            cdDAL.LogMessage("Working, Copied Data From File: " + rDAL.Path);
        }

        private void Finish(bool result)
        {
            label1.Text = "No Recon File Found.\nMake sure Recon file is in same Folder as the design file.\nDesign file not created.";
            pictureBox1.Visible = false;
            buttonOK.Enabled = true;
            buttonOK.Visible = true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            label1.Text = "File Created.";
            pictureBox1.Visible = false;
            buttonOK.Enabled = true;
        }
    }
}
