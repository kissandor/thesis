using ExcelUploadClient.Utilities;
using ExcelUploadClient.MVVM.Model;
using ExcelUploadClient.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Configuration;
using System.Net.Http;
using System.Windows;
using DocumentFormat.OpenXml.Office2016.Drawing.Charts;

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

        private Visibility progresBarVisibility = Visibility.Visible;
        public Visibility ProgresBarVisibility
        {
            get { return progresBarVisibility; }
            set
            {
                if (progresBarVisibility != value)
                {
                    progresBarVisibility = value;
                    OnPropertyChanged(nameof(progresBarVisibility));
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
            LoadCategoriesAsync();
        }

        private async void LoadCategoriesAsync()
        {
            Visibility = Visibility.Hidden;
            try
            {
                DataTable dataTable = await ApiHandler.GetJsonDataAsync(apiUrl, getAllCategoriesEndPoint);
                Categories = ConvertDataTableToCategories(dataTable);
                ProgresBarVisibility = Visibility.Hidden;
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
            ProgresBarVisibility = Visibility.Hidden;
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
                        Id = Convert.ToInt32(row["id"]),
                        CategoryName = row["categoryName"].ToString(),
                    };
                    categories.Add(category);
            }

            return categories;
        }
    }

}
