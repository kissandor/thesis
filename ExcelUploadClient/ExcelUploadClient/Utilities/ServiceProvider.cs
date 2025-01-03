﻿using ExcelUploadClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelUploadClient.Utilities
{
    internal static class ServiceProvider
    {
        public static IMessageService MessageService { get; set; } = new MessageService();
        public static INavigationService NavigationService { get; set; } = new NavigationService();
        public static IDownloadService DownloadService { get; set; } = new DownloadService();
        public static IFileService FileService { get; set; } = new FileService();
        public static IDataConversionService DataConversionService { get; set; } = new DataConversionService();
        public static CategoryService CategoryService { get; set; } = new CategoryService();
        public static ComputerPartService ComputerPartService { get; set; } = new ComputerPartService();
    }
}
