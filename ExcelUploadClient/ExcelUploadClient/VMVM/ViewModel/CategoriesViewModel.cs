using ExcelUploadClient.Utilities;
using ExcelUploadClient.VMVM.Model;
using ExcelUploadClient.VMVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ExcelUploadClient.VMVM.ViewModel
{
    public class CategoriesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Category> categories;

        public ObservableCollection<Category> Categories
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

        public CategoriesViewModel()
        {
            LoadCategoriesAsync();
        }

        private async void LoadCategoriesAsync()
        {
            try
            {
                string apiUrl = "http://localhost:5278";
                string apiEndpoint = "api/ComputerPart/GetAllCategories";

                DataTable dataTable = await ApiHandler.GetJsonDataAsync(apiUrl, apiEndpoint);
                Categories = ConvertDataTableToCategories(dataTable);
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

        private ObservableCollection<Category> ConvertDataTableToCategories(DataTable dataTable)
        {
            ObservableCollection<Category> categories = new ObservableCollection<Category>();

            foreach (DataRow row in dataTable.Rows)
            {
                    Category category = new Category
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
