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

namespace ExcelUploadClient.MVVM.ViewModel
{
    public class WebShopsViewModel: ViewModelBase
    {
        private ObservableCollection<Webshop> webshops;

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
            LoadCategoriesAsync();
        }

        private async void LoadCategoriesAsync()
        {
            try
            {
                string apiUrl = "http://localhost:5278";
                string apiEndpoint = "api/ComputerPart/GetAllWebshops";

                DataTable dataTable = await ApiHandler.GetJsonDataAsync(apiUrl, apiEndpoint);
                Webshops = ConvertDataTableToCategories(dataTable);
            }
            catch (Exception ex)
            {
                // Kezeld a hibákat vagy naplózd őket szükség szerint
            }
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
