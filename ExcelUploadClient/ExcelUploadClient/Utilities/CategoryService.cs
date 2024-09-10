using ExcelUploadClient.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelUploadClient.Utilities
{
    internal class CategoryService
    {
        private readonly string apiUrl;
        private readonly string getAllCategoriesEndPoint;

        public CategoryService()
        {
            apiUrl = ConfigurationManager.AppSettings["ApiUrl"];
            getAllCategoriesEndPoint = ConfigurationManager.AppSettings["GetAllCategoriesEndPoint"];
        }
        public async Task<ObservableCollection<ComputerPartCategory>> GetCategoriesAsync()
        {
            DataTable dataTable = await ApiHandler.GetJsonDataAsync(apiUrl, getAllCategoriesEndPoint);
            return ConvertDataTableToCategories(dataTable);
        }

        private ObservableCollection<ComputerPartCategory> ConvertDataTableToCategories(DataTable dataTable)
        {
            ObservableCollection<ComputerPartCategory> categories = new ObservableCollection<ComputerPartCategory>();
            foreach (DataRow row in dataTable.Rows)
            {
                ComputerPartCategory category = new ComputerPartCategory
                {
                    Id = row["id"] != DBNull.Value ? Convert.ToInt32(row["id"]) : 0,
                    CategoryName = row["categoryName"]?.ToString(),
                };
                categories.Add(category);
            }
            return categories;
        }
    }
}
