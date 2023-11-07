using DocumentFormat.OpenXml.Wordprocessing;
using ExcelUploadClient.Utilities;
using ExcelUploadClient.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace ExcelUploadClient.MVVM.ViewModel
{
    internal class DeleteViewModel : ViewModelBase
    {

        private string apiUrl = "http://localhost:5278";
        private string apiEndpoint = "api/ComputerPart/DeleteAll";
        public ICommand CancelCmd
        {
            get
            {
                return new RelayCommand(param =>
                {
                    NavigationViewModel navigationViewModel = ((Window)Application.Current.MainWindow).DataContext as NavigationViewModel;
                    navigationViewModel.CurrentView = new Home();
                });
            }
        }

        public ICommand DeleteDatabaseCmd
        {
            get
            {
                return new RelayCommand(async param =>
                {
                    await ApiHandler.DeleteDatabaseAsync(apiUrl, apiEndpoint);
                    NavigationViewModel navigationViewModel = ((Window)Application.Current.MainWindow).DataContext as NavigationViewModel;
                    navigationViewModel.CurrentView = new Home();
                });
            }
        }


    }

}

