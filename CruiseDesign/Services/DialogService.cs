using System.Windows.Forms;

namespace CruiseDesign.Services
{
    public class DialogService : IDialogService
    {
        public void ShowMessage(string message, string caption = "")
        {
            MessageBox.Show(message, caption);
        }
    }

    public interface IDialogService
    {
        void ShowMessage(string message, string caption = "");
    }
}