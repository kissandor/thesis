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
    }
}
