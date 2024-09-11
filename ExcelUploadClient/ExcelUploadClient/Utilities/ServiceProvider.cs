using ExcelUploadClient.Interfaces;
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
    }
}
