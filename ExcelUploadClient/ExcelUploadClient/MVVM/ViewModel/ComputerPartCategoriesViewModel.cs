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

namespace ExcelUploadClient.MVVM.ViewModel
{
    public class ComputerPartCategoriesViewModel : ViewModelBase
    {

        private readonly string apiUrl;
        private readonly string getAllCategoriesEndPoint;
        private ObservableCollection<ComputerPartCategory> categories;

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
            try
            {
                DataTable dataTable = await ApiHandler.GetJsonDataAsync(apiUrl, getAllCategoriesEndPoint);
                Categories = ConvertDataTableToCategories(dataTable);
            }
            catch (Exception ex)
            {
                // Kezeld a hibákat vagy naplózd őket szükség szerint
            }
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
