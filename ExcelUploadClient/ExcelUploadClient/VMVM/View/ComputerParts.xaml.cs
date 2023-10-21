﻿using ExcelUploadClient.VMVM.ViewModel;
using ExcelUploadClient.VMVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExcelUploadClient.VMVM.View
{
    /// <summary>
    /// Interaction logic for ComputerParts.xaml
    /// </summary>
    public partial class ComputerParts : UserControl
    {
        public ComputerParts()
        {
            InitializeComponent();
            DataContext = new ComputerPartsViewModel();
        }
    }
}