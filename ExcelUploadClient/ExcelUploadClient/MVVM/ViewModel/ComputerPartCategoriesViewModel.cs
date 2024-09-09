using ExcelUploadClient.Utilities;
using ExcelUploadClient.MVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http;
using System.Windows;

namespace ExcelUploadClient.MVVM.ViewModel
{
    public class ComputerPartCategoriesViewModel : ViewModelBase
    {

        private readonly CategoryService categoryService;
        private ObservableCollection<ComputerPartCategory> categories;

        private string errorMessageText;
        public string ErrorMessageText {
            get { return errorMessageText; } 
            set
            {
                if (errorMessageText != value)
                {
                    errorMessageText = value;
                    OnPropertyChanged(nameof(ErrorMessageText));
                }
            }
        }

        private Visibility visibility;
        public Visibility Visibility
        {
            get { return visibility; }
            set
            {
                if (visibility != value)
                {
                    visibility = value;
                    OnPropertyChanged(nameof(Visibility));
                }
            }
        }

        private Visibility progressBarVisibility = Visibility.Visible;
        public Visibility ProgressBarVisibility
        {
            get { return progressBarVisibility; }
            set
            {
                if (progressBarVisibility != value)
                {
                    progressBarVisibility = value;
                    OnPropertyChanged(nameof(ProgressBarVisibility));
                }
            }
        }

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

        public ComputerPartCategoriesViewModel()
        {
            categoryService = new CategoryService();
            LoadCategoriesAsync().ConfigureAwait(false);
        }

        private async Task LoadCategoriesAsync()
        {
            Visibility = Visibility.Hidden;
            try
            {
                Categories = await categoryService.GetCategoriesAsync();
                ProgressBarVisibility = Visibility.Hidden;
            }
            catch (HttpRequestException)
            {
                // Az HTTP kérés hiba, tehát a szerver nem érhető el
                ShowErrorMessage("A szerver jelenleg nem elérhető. Kérlek próbáld újra később.");
            }
            catch (Exception ex)
            {
                // Bármilyen más hiba esetén
                ShowErrorMessage($"Hiba történt a kérés során: {ex.Message}");
            }
        }

        private void ShowErrorMessage(string message)
        {
            ProgressBarVisibility = Visibility.Hidden;
            Visibility = Visibility.Visible;
            ErrorMessageText = message;
        }

    }

}
