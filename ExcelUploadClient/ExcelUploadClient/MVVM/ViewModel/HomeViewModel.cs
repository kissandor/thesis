﻿using ExcelUploadClient.Interfaces;
using ExcelUploadClient.Utilities;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace ExcelUploadClient.MVVM.ViewModel
{

    internal class HomeViewModel : ViewModelBase
    {

        private readonly IDownloadService downloadService;
        private readonly IMessageService messageService;

        public HomeViewModel()
        {
            downloadService = ServiceProvider.DownloadService;
            messageService = ServiceProvider.MessageService;    
        }

        public ICommand DownloadFileCmd
        {
            get
            {
                return new RelayCommand(async param =>
                {
                    await DownloadFileAsync();
                });
            }
        }

        private async Task DownloadFileAsync()
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
                    await downloadService.DownloadFileAsync(fileURL, fullPath);
                    messageService.ShowMessage("The file has been downloaded successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (WebException webEx)
                {
                    messageService.ShowMessage($"An error occurred during the download: {webEx.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    messageService.ShowMessage($"A general error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                messageService.ShowMessage("No file was selected for download.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
