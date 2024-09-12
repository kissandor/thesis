using ExcelUploadClient.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelUploadClient.Interfaces
{
    internal interface IDataConversionService
    {
        ObservableCollection<ComputerPartCategory> ConvertDataTableToCategories(DataTable dataTable);
        ObservableCollection<WebShop> ConvertDataTableToWebshops(DataTable dataTable);
        ObservableCollection<PartUpload> ConvertDataTableToComputerParts(DataTable dataTable);
    }
}
