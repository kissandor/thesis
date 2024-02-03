using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelUploadClient.Utilities
{
    internal class ExcelFileHandler
    {
        public static DataTable ReadExcelFile(string filePath, int sheetNumber)
        {
            DataTable dataTable = new DataTable();

            using (XLWorkbook workbook = new XLWorkbook(filePath))
            {
                if (sheetNumber <= workbook.Worksheets.Count && sheetNumber > 0)
                {
                    IXLWorksheet xLWorksheet = workbook.Worksheet(sheetNumber);

                    bool isFirst = true;

                    foreach (IXLRow row in xLWorksheet.RowsUsed())
                    {
                        if (isFirst)
                        {
                            foreach (IXLCell cell in row.Cells())
                            {
                                dataTable.Columns.Add(cell.Value.ToString());
                            }
                            isFirst = false;
                        }
                        else
                        {
                            dataTable.Rows.Add();
                            int i = 0;
                            foreach (IXLCell cell in row.Cells())
                            {
                                dataTable.Rows[dataTable.Rows.Count - 1][i++] = cell.Value.ToString();
                            }
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
            return dataTable;
        }
    }
}
