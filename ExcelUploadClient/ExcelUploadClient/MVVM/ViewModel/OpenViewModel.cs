using ExcelUploadClient.Utilities;
using ExcelUploadClient.MVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;
using ExcelUploadClient.Interfaces;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ExcelUploadClient.MVVM.ViewModel
{
    public class OpenViewModel : ViewModelBase
    {

        private readonly IFileService fileService;
        private readonly IDataConversionService dataConversionService;
        private readonly IMessageService messageService;

        private readonly string apiUrl;
        private readonly string categoryUploadEndPoint;
        private readonly string computerPartsUploadEndPoint;
        private readonly string webshopUploadEndPoint;

        private string selectedFilePath;
        private DataTable dtParts, dtCategory, dtWebshop;
  

        private ObservableCollection<ComputerPartCategory> categories;
        private ObservableCollection<WebShop> webshops;
        private ObservableCollection<ComputerPart> computerPatrts;

        public ObservableCollection<ComputerPartCategory> Categories
        {
            get { return categories; }
            set
            {
                if (categories != value)
                {
                    categories = value;
                    OnPropertyChanged(nameof(Categories));
                }
            }
        }

        public ObservableCollection<WebShop> Webshops
        {
            get { return webshops; }
            set
            {
                if (webshops != value)
                {
                    webshops = value;
                    OnPropertyChanged(nameof(Webshops));
                }
            }
        }

        public ObservableCollection<ComputerPart> ComputerParts
        {
            get { return computerPatrts; }
            set
            {
                if (computerPatrts != value)
                {
                    computerPatrts = value;
                    OnPropertyChanged(nameof(ComputerParts));
                }
            }
        }


        public OpenViewModel()
        {
            fileService = ServiceProvider.FileService;
            dataConversionService = ServiceProvider.DataConversionService;  
            messageService = ServiceProvider.MessageService;


            apiUrl = ConfigurationManager.AppSettings["ApiUrl"];
            categoryUploadEndPoint = ConfigurationManager.AppSettings["CategoryUploadEndPoint"];
            computerPartsUploadEndPoint = ConfigurationManager.AppSettings["ComputerPartsUploadEndPoint"];
            webshopUploadEndPoint = ConfigurationManager.AppSettings["WebshopUploadEndPoint"];

            LoadAsync();
        }

        private async Task LoadAsync()
        {
            try
            {
                selectedFilePath = fileService.OpenFile();
                if (string.IsNullOrEmpty(selectedFilePath))
                {
                    messageService.ShowMessage($"No file has been selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                await ReadExcelDataToDataTableAsync(selectedFilePath);
                await LoadDataAsync();
                ConvertDataTableToObservableCollection();
            }
            catch (Exception ex)
            {
                messageService.ShowMessage($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task ReadExcelDataToDataTableAsync(String pathFile)
        {
            if (pathFile != null)
            {
                dtParts = await Task.Run(() => ExcelFileHandlerInterop.ReadExcelFile(selectedFilePath, 1));
                dtCategory = await Task.Run(() => ExcelFileHandlerInterop.ReadExcelFile(selectedFilePath, 2));
                dtWebshop = await Task.Run(() => ExcelFileHandlerInterop.ReadExcelFile(selectedFilePath, 3));
            }
        } 

        private void ConvertDataTableToObservableCollection()
        {

            ComputerParts = dataConversionService.ConvertDataTableToComputerParts(dtParts);
            Webshops = dataConversionService.ConvertDataTableToWebshops(dtWebshop);
            Categories = dataConversionService.ConvertDataTableToCategories(dtParts);
        }

        private async Task LoadDataAsync()
        {
            try
            {              
                String resp = await Task.Run(() => ApiHandler.SendDataAsync(dtCategory, apiUrl, categoryUploadEndPoint));
                resp = await Task.Run(() => ApiHandler.SendDataAsync(dtParts, apiUrl, computerPartsUploadEndPoint));
                resp = await Task.Run(() => ApiHandler.SendDataAsync(dtWebshop, apiUrl, webshopUploadEndPoint));
            }
            catch (Exception ex)
            {
                messageService.ShowMessage($"An error occurred during data upload: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
