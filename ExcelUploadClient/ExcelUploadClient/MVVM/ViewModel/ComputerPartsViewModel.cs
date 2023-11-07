using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using ExcelUploadClient.Utilities;
using ExcelUploadClient.MVVM.Model;
using Newtonsoft.Json.Linq;

namespace ExcelUploadClient.MVVM.ViewModel
{

    public class ComputerPartsViewModel : ViewModelBase
    {
        private ObservableCollection<ComputerPart> computerParts;

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
            LoadComputerPartsData();
        }

        private async void LoadComputerPartsData()
        {
            try
            {
                string apiUrl = "http://localhost:5278"; // Cseréld le a saját API URL-re
                string apiEndpoint = "api/ComputerPart/GetAllComputerParts"; // Cseréld le a saját API végpontodra

                //ObservableCollection<ComputerPart> parts = await ApiHandler.GetComputerParts(apiUrl, apiEndpoint);                        
                DataTable dataTable = await ApiHandler.GetJsonDataAsync(apiUrl, apiEndpoint);
                ComputerParts = ConvertDataTableToComputerParts(dataTable);
            }
            catch (Exception ex)
            {
                // Kezeld a hibákat vagy naplózd őket szükség szerint
            }
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