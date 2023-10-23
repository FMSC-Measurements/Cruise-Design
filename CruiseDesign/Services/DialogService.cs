using CruiseDesign.Dialogs;
using System;
using System.Windows.Forms;

namespace CruiseDesign.Services
{
    public class DialogService : IDialogService
    {
        public void ShowMessage(string message, string caption = "")
        {
            MessageBox.Show(message, caption);
        }

        public string AskOpenV3TemplateFile(string initialDir = null)
        {
            initialDir ??= Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\CruiseFiles\\Templates";

            using var fileDialog = new OpenFileDialog()
            {
                Filter = "V3 Template File (*.crz3t)|*.crz3t",
                InitialDirectory = initialDir,
            };

            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return fileDialog.FileName;
        }

        public string AskSaveProductionFilePath(bool defaultV3, string defaultFileName)
        {
            var fileDialog = new SaveFileDialog()
            {
                DefaultExt = (defaultV3) ? ".crz3" : ".cruise",
                Filter = "V2 Cruise File|*.cruise|V3 Cruise File|*.crz3",
                FileName = defaultFileName,
            };

            if(fileDialog.ShowDialog() != DialogResult.OK) { return null; }

            return fileDialog.FileName;
        }

        public string AskSelectCreateProductionV3Template(string reconFilePath)
        {
            using var dialog = new SelectCreateProductionV3TemplateDialog(reconFilePath);

            var result = dialog.ShowDialog();

            if(result == DialogResult.OK)
            {
                return dialog.SelectedSourcePath;
            }
            return null;
        }
    }

    public interface IDialogService
    {
        void ShowMessage(string message, string caption = "");

        string AskOpenV3TemplateFile(string initialDir = null);

        string AskSaveProductionFilePath(bool defaultV3, string defaultFileName);

        string AskSelectCreateProductionV3Template(string reconFilePath);
    }
}