using ExcelUploadClient.MVVM.Model;
using ExcelUploadClient.Utilities;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Windows;
using System;
using System.Threading.Tasks;
using ExcelUploadClient.MVVM.View;

namespace ExcelUploadClient.MVVM.ViewModel
{
    public class ComputerPartsViewModel : ViewModelBase
    {
        private readonly ComputerPartService partsService;
        private ObservableCollection<ComputerPart> computerParts;

        private string errorMessageText;
        public string ErrorMessageText
        {
            get { return errorMessageText; }
            set
            {
                if (errorMessageText != value)
                {
                    errorMessageText = value;
                    OnPropertyChanged(nameof(ErrorMessageText));
                }
            }
        }

        private Visibility visibility;
        public Visibility Visibility
        {
            get { return visibility; }
            set
            {
                if (visibility != value)
                {
                    visibility = value;
                    OnPropertyChanged(nameof(Visibility));
                }
            }
        }

        private Visibility progressBarVisibility = Visibility.Visible;
        public Visibility ProgressBarVisibility
        {
            get { return progressBarVisibility; }
            set
            {
                if (progressBarVisibility != value)
                {
                    progressBarVisibility = value;
                    OnPropertyChanged(nameof(ProgressBarVisibility));
                }
            }
        }

        public ObservableCollection<ComputerPart> ComputerParts
        {
            get { return computerParts; }
            set
            {
                if (computerParts != value)
                {
                    computerParts = value;
                    OnPropertyChanged(nameof(ComputerParts));
                }
            }
        }

        public ComputerPartsViewModel()
        {
            partsService = ServiceProvider.ComputerPartService;
            LoadComputerPartsAsync().ConfigureAwait(false);
        }

        private async Task LoadComputerPartsAsync()
        {
            Visibility = Visibility.Hidden;
            try
            {
                ComputerParts = await partsService.GetComputerPartsAsync();
                ProgressBarVisibility = Visibility.Hidden;
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("The server is currently unavailable. Please try again later.");
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"An error occurred during the request: {ex.Message}");
            }
        }

        private void ShowErrorMessage(string message)
        {
            ProgressBarVisibility = Visibility.Hidden;
            Visibility = Visibility.Visible;
            ErrorMessageText = message;
        }
    }
}