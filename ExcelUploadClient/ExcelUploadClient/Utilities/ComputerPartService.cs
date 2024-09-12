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
            return ConvertDataTableToComputerParts(dataTable);
            //return dataConversionService.ConvertDataTableToComputerParts(dataTable);
        }

        private ObservableCollection<ComputerPart> ConvertDataTableToComputerParts(DataTable dataTable)
        {
            var computerParts = new ObservableCollection<ComputerPart>();

            foreach (DataRow row in dataTable.Rows)
            {
                var computerPart = new ComputerPart
                {
                    Id = row["id"] != DBNull.Value ? Convert.ToInt32(row["id"]) : 0,
                    ComputerPartName = row["computerPartName"]?.ToString(),
                    //ComputerPartPrice = row["computerPartPrice"] as decimal?,
                    CategoryId = row["categoryId"] != DBNull.Value ? Convert.ToInt32(row["categoryId"]) : 0,
                    //WebshopId = row["webshopId"] as int?
                };

                computerParts.Add(computerPart);
            }
            return computerParts;
        }
    }
}
