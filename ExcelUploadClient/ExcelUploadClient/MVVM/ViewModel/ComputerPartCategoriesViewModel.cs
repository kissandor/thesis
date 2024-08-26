using ExcelUploadClient.Utilities;
using ExcelUploadClient.MVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http;
using System.Windows;

namespace ExcelUploadClient.MVVM.ViewModel
{
    public class ComputerPartCategoriesViewModel : ViewModelBase
    {

        private readonly string apiUrl;
        private readonly string getAllCategoriesEndPoint;
        private ObservableCollection<ComputerPartCategory> categories;

        private string errorMessageText;
        public string ErrorMessageText {
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

        public ObservableCollection<ComputerPartCategory> Categories
        {
            get { return categories; }
            set
            {
                if (categories != value)
                {
                    categories = value;
                    OnPropertyChanged(nameof(Categories));
                }
            }
        }

        public ComputerPartCategoriesViewModel()
        {
            
            apiUrl = ConfigurationManager.AppSettings["ApiUrl"];
            getAllCategoriesEndPoint = ConfigurationManager.AppSettings["GetAllCategoriesEndPoint"];
            
            if (string.IsNullOrEmpty(apiUrl) || string.IsNullOrEmpty(getAllCategoriesEndPoint))
            {
                ShowErrorMessage("Configuration settings are missing or invalid.");
                return;
            }

            LoadCategoriesAsync().ConfigureAwait(false);
        }

        private async Task LoadCategoriesAsync()
        {
            Visibility = Visibility.Hidden;
            try
            {
                DataTable dataTable = await ApiHandler.GetJsonDataAsync(apiUrl, getAllCategoriesEndPoint);
                Categories = ConvertDataTableToCategories(dataTable);
                ProgressBarVisibility = Visibility.Hidden;
            }
            catch (HttpRequestException)
            {
                // Az HTTP kérés hiba, tehát a szerver nem érhető el
                ShowErrorMessage("A szerver jelenleg nem elérhető. Kérlek próbáld újra később.");
            }
            catch (Exception ex)
            {
                // Bármilyen más hiba esetén
                ShowErrorMessage($"Hiba történt a kérés során: {ex.Message}");
            }
        }

        private void ShowErrorMessage(string message)
        {
            ProgressBarVisibility = Visibility.Hidden;
            Visibility = Visibility.Visible;
            ErrorMessageText = message;
        }

        private ObservableCollection<ComputerPartCategory> ConvertDataTableToCategories(DataTable dataTable)
        {
            ObservableCollection<ComputerPartCategory> categories = new ObservableCollection<ComputerPartCategory>();

            foreach (DataRow row in dataTable.Rows)
            {
                    ComputerPartCategory category = new ComputerPartCategory
                    {
                        Id = row["id"] != DBNull.Value ? Convert.ToInt32(row["id"]) : 0,
                        CategoryName = row["categoryName"]?.ToString(),
                    };
                    categories.Add(category);
            }

            return categories;
        }
    }

}
