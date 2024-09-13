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
using ExcelUploadClient.Interfaces;

namespace ExcelUploadClient.MVVM.ViewModel
{
    public class WebShopsViewModel: ViewModelBase
    {
        private readonly string apiUrl;
        private readonly string getAllWebshopsEndPoint;
        private ObservableCollection<WebShop> webshops;

        private readonly IDataConversionService conversionService;


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

        public ObservableCollection<WebShop> Webshops
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
            conversionService = ServiceProvider.DataConversionService;

            apiUrl = ConfigurationManager.AppSettings["ApiUrl"];
            getAllWebshopsEndPoint = ConfigurationManager.AppSettings["GetAllWebshopsEndPoint"];
            LoadWebShopsAsync().ConfigureAwait(false);
        }

        private async Task LoadWebShopsAsync()
        {
            Visibility = Visibility.Hidden;
            try
            {
                DataTable dataTable = await ApiHandler.GetJsonDataAsync(apiUrl, getAllWebshopsEndPoint);
                Webshops = conversionService.ConvertDataTableToWebshops(dataTable);
                ProgresBarVisibility = Visibility.Hidden;
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("The server is currentyly offline. Please come back later.");
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error: {ex.Message}");
            }
        }

        private void ShowErrorMessage(string message)
        {
            ProgresBarVisibility = Visibility.Hidden;
            Visibility = Visibility.Visible;
            ErrorMessageText = message;
        }
    }
}
