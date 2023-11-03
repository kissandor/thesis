using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelUploadClient.Utilities;
using System.Windows.Input;
using System.Windows;
using Microsoft.Win32;

namespace ExcelUploadClient.VMVM.ViewModel
{
    class NavigationViewModel : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand HomeCmd { get; set; }
        public ICommand CategoriesCmd { get; set; }
        public ICommand WebShopsCmd { get; set; }
        public ICommand ComputerPartsCmd { get; set; }
        public ICommand OpenCmd { get; set; }
        public ICommand DeleteCmd { get; set; }

        private void Home(object obj) => CurrentView = new HomeViewModel();
        private void Categories(object obj) => CurrentView = new ComputerPartCategoriesViewModel();
        private void WebShops(object obj) => CurrentView = new WebShopsViewModel();
        private void ComputerParts(object obj) => CurrentView = new ComputerPartsViewModel();
        private void Open(object obj) => CurrentView = new OpenViewModel();
        private void Delete(object obj) => CurrentView = new DeleteViewModel();


        public NavigationViewModel()
        {
            HomeCmd = new RelayCommand(Home);
            CategoriesCmd = new RelayCommand(Categories);
            WebShopsCmd = new RelayCommand(WebShops);
            ComputerPartsCmd = new RelayCommand(ComputerParts);
            OpenCmd = new RelayCommand(Open);
            DeleteCmd = new RelayCommand(Delete);

            // Startup Page
            CurrentView = new HomeViewModel();
        }
    }
}
