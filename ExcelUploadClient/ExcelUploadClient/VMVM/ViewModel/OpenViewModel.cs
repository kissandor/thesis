using ExcelUploadClient.Utilities;
using ExcelUploadClient.VMVM.Model;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows;

namespace ExcelUploadClient.VMVM.ViewModel
{
    public class OpenViewModel : ViewModelBase
    {
        private string selectedFilePath;
        private DataTable dtParts, dtCategory, dtWebshop;
  

        private ObservableCollection<ComputerPartCategory> categories;
        private ObservableCollection<Webshop> webshops;
        private ObservableCollection<PartUpload> partsUpload;

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

        public ObservableCollection<PartUpload> PartsUpload
        {
            get { return partsUpload; }
            set
            {
                if (partsUpload != value)
                {
                    partsUpload = value;
                    OnPropertyChanged(nameof(PartsUpload));
                }
            }
        }


        public OpenViewModel()
        {
            
            Load();
        }

        private async void Load()
        {
            try
            {
                selectedFilePath =  OpenFile();
                await SaveToDataTable(selectedFilePath);
                await LoadDataAsync();
                ConvertDataTableToObservableCollection();
            }
            catch (Exception ex)
            {
                // Kezeld a hibákat vagy naplózd őket szükség szerint
            }
        }

        private string OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel workbook | *.xlsx", Multiselect = false };
                try
                {
                    if (openFileDialog.ShowDialog() == true)
                    {
                         return openFileDialog.FileName;
                    }

                return null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hiba történt: {ex.Message}", "Hibajelentés");
                }
            return null;
        }

        private async Task SaveToDataTable(String pathFile)
        {
            if (pathFile != null)
            {
                dtParts = new DataTable();
                dtCategory = new DataTable();
                dtWebshop = new DataTable();
                dtParts = await Task.Run(() => ExcelFileHandler.ReadExcelFile(selectedFilePath, 1));
                dtCategory = await Task.Run(() => ExcelFileHandler.ReadExcelFile(selectedFilePath, 2));
                dtWebshop = await Task.Run(() => ExcelFileHandler.ReadExcelFile(selectedFilePath, 3));

            }
        } 

        private void ConvertDataTableToObservableCollection()
        {
            PartsUpload = ConvertDataTableToComputerParts(dtParts);
            Webshops = ConvertDataTableToWebshops(dtWebshop);
            Categories = ConvertDataTableToCategories(dtCategory);
        }

        private async Task LoadDataAsync()
        {
            try
            {
                String resp = await Task.Run(() => ApiHandler.SendDataAsync(dtCategory, "http://localhost:5278", "api/ComputerPart/CategoryUpload"));
                resp = await Task.Run(() => ApiHandler.SendDataAsync(dtParts, "http://localhost:5278", "api/ComputerPart/ComputerPartsUpload"));
                resp = await Task.Run(() => ApiHandler.SendDataAsync(dtWebshop, "http://localhost:5278", "api/ComputerPart/WebshopUpload"));

            }
            catch
            {

            }
        }

        private ObservableCollection<ComputerPartCategory> ConvertDataTableToCategories(DataTable dataTable)
        {
            ObservableCollection<ComputerPartCategory> categories = new ObservableCollection<ComputerPartCategory>();

            foreach (DataRow row in dataTable.Rows)
            {
                ComputerPartCategory category = new ComputerPartCategory
                {
                    Id = dataTable.Rows.IndexOf(row),
                    CategoryName = row["categoryName"].ToString(),
                };
                categories.Add(category);
            }

            return categories;
        }
        private ObservableCollection<Webshop> ConvertDataTableToWebshops(DataTable dataTable)
        {
            ObservableCollection<Webshop> webShops= new ObservableCollection<Webshop>();

            foreach (DataRow row in dataTable.Rows)
            {
                 Webshop ws = new Webshop
                {
                    Id = dataTable.Rows.IndexOf(row),
                    WebshopName = row["webshopName"].ToString(),
                    WebshopURL = row["webshopURL"].ToString()
                 };
                webShops.Add(ws);
            }

            return webShops;
        }
        private ObservableCollection<PartUpload> ConvertDataTableToComputerParts(DataTable dataTable)
        {
            ObservableCollection<PartUpload> computerParts = new ObservableCollection<PartUpload>();

            foreach (DataRow row in dataTable.Rows)
            {
                PartUpload computerPart = new PartUpload
                {
                    Id = dataTable.Rows.IndexOf(row),
                    ComputerPartName = row["computerPartName"].ToString(),
                    CategoryName = row["categoryName"].ToString()
                };

                computerParts.Add(computerPart);
            }

            return computerParts;
        }
    }
}
