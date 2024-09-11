using ExcelUploadClient.Interfaces;
using ExcelUploadClient.MVVM.ViewModel;
using System.Windows;

namespace ExcelUploadClient.Utilities
{
    internal class NavigationService : INavigationService
    {
        public void NavigateTo(object view)
        {
            var navigationViewModel = ((Window)Application.Current.MainWindow).DataContext as NavigationViewModel;
            navigationViewModel.CurrentView = view;
        }
    }
}
