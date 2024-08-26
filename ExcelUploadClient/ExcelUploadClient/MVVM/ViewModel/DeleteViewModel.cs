using DocumentFormat.OpenXml.Wordprocessing;
using ExcelUploadClient.Utilities;
using ExcelUploadClient.MVVM.View;
using System;
using System.Windows.Input;
using System.Windows;
using System.Configuration;

namespace ExcelUploadClient.MVVM.ViewModel
{
    internal class DeleteViewModel : ViewModelBase
    {

        private readonly string apiUrl;
        private readonly string apiEndpoint;
        private ICommand cancelCmd;
        private ICommand deleteDatabaseCmd;

        public DeleteViewModel()
        {
            apiUrl = ConfigurationManager.AppSettings["ApiUrl"];
            apiEndpoint = ConfigurationManager.AppSettings["DeleteAll"];

            if (string.IsNullOrEmpty(apiUrl) || string.IsNullOrEmpty(apiEndpoint))
            {
                MessageBox.Show("Configuration settings are missing or invalid.");
            }
        }

        public ICommand CancelCmd
        {
            get
            {
                return cancelCmd ?? (cancelCmd = new RelayCommand(param =>
                {
                    NavigationViewModel navigationViewModel = ((Window)Application.Current.MainWindow).DataContext as NavigationViewModel;
                    navigationViewModel.CurrentView = new Home();
                }));
            }
        }

        public ICommand DeleteDatabaseCmd
        {
            get
            {
                return deleteDatabaseCmd ?? (deleteDatabaseCmd = new RelayCommand(async param =>
                {
                    try
                    {
                        await ApiHandler.DeleteDatabaseAsync(apiUrl, apiEndpoint);
                        NavigationViewModel navigationViewModel = ((Window)Application.Current.MainWindow).DataContext as NavigationViewModel;
                        navigationViewModel.CurrentView = new DatabaseDeleted();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to delete the database: {ex.Message}");
                    }
                }));
            }
        }
    }
}

