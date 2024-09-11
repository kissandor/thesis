using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelUploadClient.Interfaces
{
    internal interface IDownloadService
    {
        Task DownloadFileAsync(string fileUrl, string destinationPath);
    }
}
