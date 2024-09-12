using ExcelUploadClient.Interfaces;
using ExcelUploadClient.MVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.Data;


namespace ExcelUploadClient.Utilities
{
    internal class DataConversionService : IDataConversionService
    {
        public ObservableCollection<ComputerPartCategory> ConvertDataTableToCategories(DataTable dataTable)
        {
            ObservableCollection<ComputerPartCategory> categories = new ObservableCollection<ComputerPartCategory>();

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    ComputerPartCategory category = new ComputerPartCategory
                    {
                        Id = dataTable.Rows.IndexOf(row),
                        CategoryName = row["categoryName"]?.ToString(),
                    };
                    categories.Add(category);
                }
            }
            return categories;
        }

        public ObservableCollection<PartUpload> ConvertDataTableToComputerParts(DataTable dataTable)
        {
            ObservableCollection<PartUpload> computerParts = new ObservableCollection<PartUpload>();
            if (dataTable != null)
            {
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
            }
            return computerParts;
        }

        public ObservableCollection<WebShop> ConvertDataTableToWebshops(DataTable dataTable)
        {
            ObservableCollection<WebShop> webShops = new ObservableCollection<WebShop>();

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    WebShop ws = new WebShop
                    {
                        Id = dataTable.Rows.IndexOf(row),
                        WebshopName = row["webshopName"].ToString(),
                        WebshopURL = row["webshopURL"].ToString()
                    };
                    webShops.Add(ws);
                }
            }
            return webShops;
        }
    }
}
