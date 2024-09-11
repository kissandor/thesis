using ExcelUploadClient.Interfaces;
using System.Windows;

namespace ExcelUploadClient.Utilities
{
    internal class MessageService : IMessageService
    {
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
            
        }

        public void ShowMessage(string message, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            MessageBox.Show(message, caption, button, icon);
        }
    }
}
