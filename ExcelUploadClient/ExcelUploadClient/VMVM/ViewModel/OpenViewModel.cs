using DocumentFormat.OpenXml.Wordprocessing;
using ExcelUploadClient.Utilities;
using ExcelUploadClient.VMVM.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ExcelUploadClient.VMVM.ViewModel
{
    public class OpenViewModel : INotifyPropertyChanged
    {
        private string selectedFilePath;

        private ObservableCollection<ComputerPartCategory> categories;
        private ObservableCollection<Webshop> webshops;
        private ObservableCollection<ComputerPart> computerParts;

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


        public OpenViewModel()
        {
            
            Load();
        }

        private async void Load()
        {
            try
            {
                await LoadDataAsync();
                /*
                ComputerParts = ConvertDataTableToCategories(dataTableCP);
                Webshops = ConvertDataTableToCategories(dataTableWS);
                Categories = ConvertDataTableToCategories(dataTableCAT);
                */
            }
            catch (Exception ex)
            {
                // Kezeld a hibákat vagy naplózd őket szükség szerint
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        private async Task LoadDataAsync()
        {
           // selectedFilePath = await Task.Run(OpenFile);
;
            OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel workbook | *.xlsx", Multiselect = false };
            try
            {
                if (openFileDialog.ShowDialog() == true)
                {
                    selectedFilePath = openFileDialog.FileName;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt: {ex.Message}", "Hibajelentés");
                selectedFilePath = null;
            }


            if (selectedFilePath != null)
            {
                /*
                DataTable dtParts = await Task.Run(() => ExcelFileHandler.ReadExcelFile(selectedFilePath, 1));
                DataTable dtCategory = await Task.Run(() => ExcelFileHandler.ReadExcelFile(selectedFilePath, 2));
                DataTable dtWebshop = await Task.Run(() => ExcelFileHandler.ReadExcelFile(selectedFilePath, 3));

                String resp = await Task.Run(() => ApiHandler.SendDataAsync(dtCategory, "http://localhost:5278", "api/ComputerPart/CategoryUpload"));
                resp = await Task.Run(() => ApiHandler.SendDataAsync(dtParts, "http://localhost:5278", "api/ComputerPart/ComputerPartsUpload"));
                resp = await Task.Run(() => ApiHandler.SendDataAsync(dtWebshop, "http://localhost:5278", "api/ComputerPart/WebshopUpload"));
                */
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

    }
}
