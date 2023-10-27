using CruiseDesign.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Windows.Forms;

namespace CruiseDesign.Dialogs
{
    public partial class SelectCreateProductionV3TemplateDialog : Form
    {
        private string _templatePath;
        private string _reconFilePath;

        public SelectCreateProductionV3TemplateDialog()
        {
            InitializeComponent();

            TemplatePath = null;
            DialogService = Program.ServiceProvider.GetRequiredService<IDialogService>();
        }

        public SelectCreateProductionV3TemplateDialog(string reconFilePath)
            : this()
        {
            ReconFilePath = reconFilePath;
            _useReconFileRadioButton.Checked = !string.IsNullOrEmpty(reconFilePath);
        }

        public IDialogService DialogService { get; }

        public string TemplatePath
        {
            get => _templatePath;
            set
            {
                _templatePath = value;
                _templatePathTextBox.Text = value?.ToString();
            }
        }

        public string ReconFilePath
        {
            get => _reconFilePath;
            set
            {
                _reconFilePath = value;
                if (value != null)
                {
                    _reconFilePathTextBox.Text = value?.ToString();
                }
                else
                {
                    _reconFilePathTextBox.Text = null;
                    _reconSelectionPanel.Enabled = false;
                    _useTemplateFileRadioButton.Checked = true;
                }
            }
        }

        public string SelectedSourcePath
        {
            get
            {
                if (_useTemplateFileRadioButton.Checked)
                {
                    return TemplatePath;
                }
                else
                {
                    return ReconFilePath;
                }
            }
        }

        private void _browseTemplateButton_Click(object sender, EventArgs e)
        {
            var result = DialogService.AskOpenV3TemplateFile();
            if (result != null)
            {
                if (!File.Exists(result))
                {
                    DialogService.ShowMessage("File Not Found");
                    return;
                }

                TemplatePath = result;
            }
        }

        private void _okButton_Click(object sender, EventArgs e)
        {
            var selectedSource = SelectedSourcePath;
            if (selectedSource == null)
            {
                DialogService.ShowMessage("No File Selected");
                return;
            }

            if (!File.Exists(selectedSource))
            {
                DialogService.ShowMessage("Selected File Not Found");
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        bool _changingRadioButtonCheckState = false;
        private void _useReconFileRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_changingRadioButtonCheckState) { return; }
            try
            {
                _changingRadioButtonCheckState = true;
                _useTemplateFileRadioButton.Checked = !_useReconFileRadioButton.Checked;
            }
            finally { _changingRadioButtonCheckState = false; }
        }

        private void _useTemplateFileRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_changingRadioButtonCheckState) { return; }
            try
            {
                _changingRadioButtonCheckState = true;
                _useReconFileRadioButton.Checked = !_useTemplateFileRadioButton.Checked;
            }
            finally { _changingRadioButtonCheckState = false; }
        }
    }
}