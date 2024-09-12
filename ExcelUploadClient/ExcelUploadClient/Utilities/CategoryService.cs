using ExcelUploadClient.Interfaces;
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
        private readonly IDataConversionService dataConversionService;

        public CategoryService()
        {
            dataConversionService = ServiceProvider.DataConversionService;
            apiUrl = ConfigurationManager.AppSettings["ApiUrl"];
            getAllCategoriesEndPoint = ConfigurationManager.AppSettings["GetAllCategoriesEndPoint"];
        }
        public async Task<ObservableCollection<ComputerPartCategory>> GetCategoriesAsync()
        {
            DataTable dataTable = await ApiHandler.GetJsonDataAsync(apiUrl, getAllCategoriesEndPoint);
            return dataConversionService.ConvertDataTableToCategories(dataTable);
        }
    }
}
