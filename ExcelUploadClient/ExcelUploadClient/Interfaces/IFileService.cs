using System.Data;
using System.Threading.Tasks;

namespace ExcelUploadClient.Interfaces
{
    internal interface IFileService
    {
        string OpenFile(string filter = "Excel workbook | *.xlsx");
        Task<DataTable> ReadExcelFileAsync(string filePath, int sheetIndex);
    }
}
