using System.Windows;

namespace ExcelUploadClient.Interfaces
{
    internal interface IMessageService
    {
        void ShowMessage(string message);
        void ShowMessage(string message, string caption, MessageBoxButton button, MessageBoxImage icon);
    }
}

