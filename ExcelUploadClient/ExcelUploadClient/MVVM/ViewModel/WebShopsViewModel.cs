using ExcelUploadClient.Utilities;
using ExcelUploadClient.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows;
using System.Net.Http;

namespace ExcelUploadClient.MVVM.ViewModel
{
    public class WebShopsViewModel: ViewModelBase
    {
        private readonly string apiUrl;
        private readonly string getAllWebshopsEndPoint;
        private ObservableCollection<Webshop> webshops;

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

        public ObservableCollection<Webshop> Webshops
        {
            get { return webshops; }
            set
            {
                if (webshops != value)
                {
                    webshops = value;
                    OnPropertyChanged(nameof(Webshops));
                }
            }
        }

        public WebShopsViewModel()
        {
            apiUrl = ConfigurationManager.AppSettings["ApiUrl"];
            getAllWebshopsEndPoint = ConfigurationManager.AppSettings["GetAllWebshopsEndPoint"];
            LoadCategoriesAsync();
        }

        private async void LoadCategoriesAsync()
        {
            Visibility = Visibility.Hidden;
            try
            {
                DataTable dataTable = await ApiHandler.GetJsonDataAsync(apiUrl, getAllWebshopsEndPoint);
                Webshops = ConvertDataTableToCategories(dataTable);
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

            Visibility = Visibility.Visible;
            ErrorMessageText = message;
        }

        private ObservableCollection<Webshop> ConvertDataTableToCategories(DataTable dataTable)
        {
            ObservableCollection<Webshop> webshops = new ObservableCollection<Webshop>();

            foreach (DataRow row in dataTable.Rows)
            {
                Webshop ws = new Webshop
                {
                    Id = Convert.ToInt32(row["id"]),
                    WebshopName = row["webShopName"].ToString(),
                    WebshopURL = row["webShopURL"].ToString(),
                };
                webshops.Add(ws);
            }

            return webshops;
        }
    }
}
