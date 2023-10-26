using CruiseDesign.Dialogs;
using System;
using System.Windows.Forms;

namespace CruiseDesign.Services
{
    public class DialogService : IDialogService
    {
        public IWindowProvider WindowProvider { get; }
        public Form MainWindow => WindowProvider.MainWindow;

        public DialogService(IWindowProvider windowProvider)
        {
            WindowProvider = windowProvider ?? throw new ArgumentNullException(nameof(windowProvider));
        }

        protected DialogResult ShowDialog(Form form)
        {
            var mainWindow = MainWindow;
            if(mainWindow != null && mainWindow.InvokeRequired)
            {
                return (DialogResult)mainWindow.Invoke(new Action(() =>  ShowDialog(form)));
            }
            else
            {
                return form.ShowDialog(mainWindow);
            }
        }

        protected DialogResult ShowDialog(FileDialog form)
        {
            var mainWindow = MainWindow;
            if (mainWindow != null && mainWindow.InvokeRequired)
            {
                return (DialogResult)mainWindow.Invoke(new Action(() => ShowDialog(form)));
            }
            else
            {
                return form.ShowDialog(mainWindow);
            }
        }

        public void ShowMessage(string message, string caption = "")
        {
            var mainWindow = MainWindow;
            if (mainWindow != null && mainWindow.InvokeRequired)
            {
                mainWindow.Invoke(new Action(() => ShowMessage(message, caption)));
            }
            else
            {
                MessageBox.Show(message, caption);
            }
        }

        public string AskOpenV3TemplateFile(string initialDir = null)
        {
            initialDir ??= Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\CruiseFiles\\Templates";

            using var fileDialog = new OpenFileDialog()
            {
                Filter = "V3 Template File (*.crz3t)|*.crz3t",
                InitialDirectory = initialDir,
            };

            if (ShowDialog(fileDialog) != DialogResult.OK)
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

            if(ShowDialog(fileDialog) != DialogResult.OK) { return null; }

            return fileDialog.FileName;
        }

        public string AskSelectCreateProductionV3Template(string reconFilePath)
        {
            using var dialog = new SelectCreateProductionV3TemplateDialog(reconFilePath);

            var result = ShowDialog(dialog);

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