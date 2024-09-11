using ExcelUploadClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExcelUploadClient.Utilities
{
    internal class DownloadService : IDownloadService
    {
        public async Task DownloadFileAsync(string fileUrl, string destinationPath)
        {
            using (WebClient client = new WebClient())
            {
                await client.DownloadFileTaskAsync(fileUrl, destinationPath);
            }
        }
    }
}
