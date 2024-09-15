using Microsoft.Office.Interop.Excel;
using System.Data;

namespace ExcelUploadClient.Utilities
{
    internal static class ExcelFileHandlerInterop
    {
        public static System.Data.DataTable ReadExcelFile(string filePath, int sheetNumber)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();

            Application excelApp = new Application();
            Workbook workbook = excelApp.Workbooks.Open(filePath);
            Worksheet worksheet = (Worksheet)workbook.Sheets[sheetNumber];

            Range range = worksheet.UsedRange;
            int rowCount = range.Rows.Count;
            int colCount = range.Columns.Count;

            
            for (int j = 1; j <= colCount; j++)
            {
                dataTable.Columns.Add(((Range)range.Cells[1, j]).Value.ToString());
            }

           
            for (int i = 2; i <= rowCount; i++)
            {
                DataRow dataRow = dataTable.NewRow();
                for (int j = 1; j <= colCount; j++)
                {
                    dataRow[j - 1] = ((Range)range.Cells[i, j]).Value.ToString();
                }
                dataTable.Rows.Add(dataRow);
            }

            workbook.Close(false);
            excelApp.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

            return dataTable;
        }
    }
}
