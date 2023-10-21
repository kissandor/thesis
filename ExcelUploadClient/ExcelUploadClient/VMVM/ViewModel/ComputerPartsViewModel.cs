using ExcelUploadClient.View;
using ExcelUploadClient.VMVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelUploadClient.ViewModel
{
    class ComputerPartsViewModel
    {
        public ObservableCollection<ComputerPart> ComputerParts { get; set; }

        public ComputerPartsViewModel()
        {
            // Itt példányosítsd a ComputerParts listát és töltsd fel adatokkal (például adatbázisból vagy más forrásból).
            ComputerParts = new ObservableCollection<ComputerPart>
            {
                new ComputerPart { Id = 1, ComputerPartName = "Alaplap", ComputerPartPrice = 100, CategoryId = 1, WebshopId = 1 },
                new ComputerPart { Id = 2, ComputerPartName = "Processzor", ComputerPartPrice = 200, CategoryId = 1, WebshopId = 1 },
                new ComputerPart { Id = 3, ComputerPartName = "Memória", ComputerPartPrice = 50, CategoryId = 2, WebshopId = 2 },
                // Egyéb ComputerPart példányok hozzáadása
            };
        }
    }
}
