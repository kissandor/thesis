using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using ExcelUploadClient.Utilities;
using ExcelUploadClient.MVVM.Model;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Windows;
using System.Net.Http;
using System.Windows.Controls;

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
            LoadComputerPartsData();
        }

        private async void LoadComputerPartsData()
        {
            Visibility = Visibility.Hidden;
            try
            {  
                DataTable dataTable = await ApiHandler.GetJsonDataAsync(apiUrl, getAllComputerPartsEndPoint);
                ComputerParts = ConvertDataTableToComputerParts(dataTable);
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

        private ObservableCollection<ComputerPart> ConvertDataTableToComputerParts(DataTable dataTable)
        {
            ObservableCollection<ComputerPart> computerParts = new ObservableCollection<ComputerPart>();

            foreach (DataRow row in dataTable.Rows)
            {
                ComputerPart computerPart = new ComputerPart
                {
                    Id = Convert.ToInt32(row["id"]),
                    ComputerPartName = row["computerPartName"].ToString(),
                    ComputerPartPrice = row["computerPartPrice"] as decimal?,
                    CategoryId = Convert.ToInt32(row["categoryId"]),
                    //Webshop = row["category"].ToString(),
                    WebshopId = row["webshopId"] as int?,
                    //Webshop = row["webshop"].ToString()
                };

                computerParts.Add(computerPart);
            }
            return computerParts;
        }

    }
}