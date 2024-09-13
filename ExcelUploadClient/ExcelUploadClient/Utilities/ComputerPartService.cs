using ExcelUploadClient.Interfaces;
using ExcelUploadClient.MVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace ExcelUploadClient.Utilities
{
    internal class ComputerPartService
    {
        private readonly string apiUrl;
        private readonly string getAllComputerPartsEndPoint;
        private readonly IDataConversionService dataConversionService;


        public ComputerPartService() 
        {
            dataConversionService = ServiceProvider.DataConversionService;
            apiUrl = ConfigurationManager.AppSettings["ApiUrl"];
            getAllComputerPartsEndPoint = ConfigurationManager.AppSettings["GetAllComputerPartsEndPoint"];
        }

        public async Task<ObservableCollection<ComputerPart>> GetComputerPartsAsync()
        {
            DataTable dataTable = await ApiHandler.GetJsonDataAsync(apiUrl, getAllComputerPartsEndPoint);
           return dataConversionService.ConvertDataTableToComputerParts(dataTable);
        }
    }
}
