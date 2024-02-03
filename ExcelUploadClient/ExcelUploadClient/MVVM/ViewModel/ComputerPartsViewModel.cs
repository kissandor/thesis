﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using ExcelUploadClient.Utilities;
using ExcelUploadClient.MVVM.Model;
using Newtonsoft.Json.Linq;
using System.Configuration;

namespace ExcelUploadClient.MVVM.ViewModel
{

    public class ComputerPartsViewModel : ViewModelBase
    {
        private readonly string apiUrl;
        private readonly string getAllComputerPartsEndPoint;
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
            apiUrl = ConfigurationManager.AppSettings["ApiUrl"];
            getAllComputerPartsEndPoint = ConfigurationManager.AppSettings["GetAllComputerPartsEndPoint"];
            LoadComputerPartsData();
        }

        private async void LoadComputerPartsData()
        {
            try
            {                      
                DataTable dataTable = await ApiHandler.GetJsonDataAsync(apiUrl, getAllComputerPartsEndPoint);
                ComputerParts = ConvertDataTableToComputerParts(dataTable);
            }
            catch (Exception ex)
            {
                // Kezeld a hibákat vagy naplózd őket szükség szerint
            }
        }

        private ObservableCollection<ComputerPart> ConvertDataTableToComputerParts(DataTable dataTable)
        {
            ObservableCollection<ComputerPart> computerParts = new ObservableCollection<ComputerPart>();

            foreach (DataRow row in dataTable.Rows)
            {
                ComputerPart computerPart = new ComputerPart
                {
                    Id = Convert.ToInt32(row["id"]),
                    ComputerPartName = row["computerPartName"].ToString(),
                    ComputerPartPrice = row["computerPartPrice"] as decimal?,
                    CategoryId = Convert.ToInt32(row["categoryId"]),
                    //Webshop = row["category"].ToString(),
                    WebshopId = row["webshopId"] as int?,
                    //Webshop = row["webshop"].ToString()
                };

                computerParts.Add(computerPart);
            }
            return computerParts;
        }
    }
}