using ExcelUploadClient.Utilities;
using ExcelUploadClient.MVVM.View;
using System;
using System.Windows.Input;
using System.Windows;
using System.Configuration;
using ExcelUploadClient.Interfaces;
using System.Windows.Navigation;

namespace ExcelUploadClient.MVVM.ViewModel
{
    internal class DeleteViewModel : ViewModelBase
    {

        private readonly string apiUrl;
        private readonly string apiEndpoint;
        private ICommand cancelCmd;
        private ICommand deleteDatabaseCmd;
        private readonly INavigationService navigationService;
        private readonly IMessageService messageService;


        public DeleteViewModel()
        {
            messageService = ServiceProvider.MessageService;
            navigationService = ServiceProvider.NavigationService;
            apiUrl = ConfigurationManager.AppSettings["ApiUrl"];
            apiEndpoint = ConfigurationManager.AppSettings["DeleteAll"];

            if (string.IsNullOrEmpty(apiUrl) || string.IsNullOrEmpty(apiEndpoint))
            {
                messageService.ShowMessage("Configuration settings are missing or invalid.");
            }
        }

        public ICommand CancelCmd
        {
            get
            {
                return cancelCmd ?? (cancelCmd = new RelayCommand(param =>
                {
                    navigationService.NavigateTo(new Home());
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

                        navigationService.NavigateTo(new DatabaseDeleted());
                    }
                    catch (Exception ex)
                    {
                        messageService.ShowMessage($"Failed to delete the database: {ex.Message}");
                    }
                }));
            }
        }
    }
}

