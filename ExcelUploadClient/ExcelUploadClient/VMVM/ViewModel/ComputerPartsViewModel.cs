using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ExcelUploadClient.Utilities;
using ExcelUploadClient.VMVM.Model;

namespace ExcelUploadClient.VMVM.ViewModel
{

    public class ComputerPartsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ComputerPart> computerParts;

        public ObservableCollection<ComputerPart> ComputerParts
        {
            get { return computerParts; }
            set
            {
                if (computerParts != value)
                {
                    computerParts = value;
                    OnPropertyChanged(nameof(ComputerParts));
                }
            }
        }

        public ComputerPartsViewModel()
        {
            LoadComputerPartsData();
        }

        private async void LoadComputerPartsData()
        {
            try
            {
                string apiUrl = "http://localhost:5278"; // Cseréld le a saját API URL-re
                string apiEndpoint = "api/ComputerPart/GetAllComputerParts"; // Cseréld le a saját API végpontodra

                ObservableCollection<ComputerPart> parts = await ApiHandler.GetComputerParts(apiUrl, apiEndpoint);

                ComputerParts = parts;
            }
            catch (Exception ex)
            {
                // Kezeld a hibákat vagy naplózd őket szükség szerint
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}