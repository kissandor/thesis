using ExcelUploadClient.MVVM.Model;
using ExcelUploadClient.Utilities;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Windows;
using System;
using System.Threading.Tasks;

namespace ExcelUploadClient.MVVM.ViewModel
{
    public class ComputerPartsViewModel : ViewModelBase
    {
        private readonly string apiUrl;
        private readonly string getAllComputerPartsEndPoint;
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
            apiUrl = ConfigurationManager.AppSettings["ApiUrl"];
            getAllComputerPartsEndPoint = ConfigurationManager.AppSettings["GetAllComputerPartsEndPoint"];

            if (string.IsNullOrEmpty(apiUrl) || string.IsNullOrEmpty(getAllComputerPartsEndPoint))
            {
                ShowErrorMessage("Configuration settings are missing or invalid.");
                return;
            }

            LoadComputerPartsData().ConfigureAwait(false);
        }

        private async Task LoadComputerPartsData()
        {
            Visibility = Visibility.Hidden;
            try
            {
                DataTable dataTable = await ApiHandler.GetJsonDataAsync(apiUrl, getAllComputerPartsEndPoint);
                ComputerParts = ConvertDataTableToComputerParts(dataTable);
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

        private ObservableCollection<ComputerPart> ConvertDataTableToComputerParts(DataTable dataTable)
        {
            var computerParts = new ObservableCollection<ComputerPart>();

            foreach (DataRow row in dataTable.Rows)
            {
                var computerPart = new ComputerPart
                {
                    Id = row["id"] != DBNull.Value ? Convert.ToInt32(row["id"]) : 0,
                    ComputerPartName = row["computerPartName"]?.ToString(),
                    //ComputerPartPrice = row["computerPartPrice"] as decimal?,
                    CategoryId = row["categoryId"] != DBNull.Value ? Convert.ToInt32(row["categoryId"]) : 0,
                    //WebshopId = row["webshopId"] as int?
                };

                computerParts.Add(computerPart);
            }
            return computerParts;
        }
    }
}