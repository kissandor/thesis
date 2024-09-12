using ExcelUploadClient.Interfaces;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Win32;


namespace ExcelUploadClient.Utilities
{
    internal class FileService : IFileService
    {
        public string OpenFile(string filter = "Excel workbook | *.xlsx")
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = filter, Multiselect = false };
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            return null;
        }

        public async Task<DataTable> ReadExcelFileAsync(string filePath, int sheetIndex)
        {
            if (string.IsNullOrEmpty(filePath)) return null;
            return await Task.Run(() => ExcelFileHandlerInterop.ReadExcelFile(filePath, sheetIndex));
        }
    }
}
