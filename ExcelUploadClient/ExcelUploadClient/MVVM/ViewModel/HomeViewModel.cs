using ExcelUploadClient.Utilities;
using System;
using System.Net;
using System.Windows;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace ExcelUploadClient.MVVM.ViewModel
{
    internal class HomeViewModel : ViewModelBase
    {
        public ICommand DownloadFileCmd
        {
            get
            {
                return new RelayCommand(param =>
                {
                    DownloadFile();
                });
            }
        }

        private void DownloadFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "upload.xlsx"; // Default file name
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"; // Filter files by extension
            saveFileDialog.Title = "Save an Excel File";

            string fileURL = "..\\..\\Resources\\Upload template.xlsx";

            if (saveFileDialog.ShowDialog() == true)
            {
                string fullPath = saveFileDialog.FileName;

                try
                {
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(fileURL, fullPath);
                        MessageBox.Show("The file has been downloaded successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (WebException webEx)
                {
                    MessageBox.Show($"An error occurred during the download: {webEx.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"A general error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
