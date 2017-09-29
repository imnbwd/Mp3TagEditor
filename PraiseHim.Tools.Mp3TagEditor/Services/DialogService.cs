using System.Windows;

namespace PraiseHim.Tools.Mp3TagEditor.Services
{
    public class DialogService : IDialogService
    {
        public void Alert(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void Info(string message)
        {
            MessageBox.Show(message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}