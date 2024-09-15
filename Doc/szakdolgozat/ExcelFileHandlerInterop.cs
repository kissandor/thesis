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
        //...
        workbook.Close(false);
        excelApp.Quit();

        System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
        System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
        System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

        return dataTable;
    }
}