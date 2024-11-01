﻿using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekke.Thesis.GetCheapestProduct.Models
{
    public class DatabaseHelper
    {
        private string connectionString;

        public DatabaseHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Product> GetCheapestProductsFromDatabase()
        {

            List<Product> products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT " +
                                        "CategoryName, " +
                                        "ComputerPartName, " +
                                        "Description, " +
                                        "ComputerPartPrice, " +
                                        "Currency, " +
                                        "WebShopURL, " +
                                        "ProductUrl " +
                                   "FROM " +
                                        "(" +
                                            "SELECT " +
                                                "s.Id, " +
                                                "cp.ComputerPartName, " +
                                                "c.CategoryName, " +
                                                "s.Description, " +
                                                "s.ComputerPartPrice, " +
                                                "s.Currency, " +
                                                "w.WebShopURL, " +
                                                "s.ProductUrl, " +
                                                "ROW_NUMBER() OVER (PARTITION BY cp.Id ORDER BY s.ComputerPartPrice) AS RowNum " +
                                             "FROM " +
                                                "SearchResults s " +
                                                "INNER JOIN ComputerParts cp ON s.ComputerPartId = cp.Id " +
                                                "INNER JOIN Categories c ON cp.CategoryId = c.Id " +
                                                "INNER JOIN WebShops w ON s.WebShopId = w.Id " +
                                          ") AS ranked " +
                                      "WHERE " +
                                           "RowNum = 1;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                Category = reader.GetString(reader.GetOrdinal("CategoryName")),
                                ComputerPartName = reader.GetString(reader.GetOrdinal("ComputerPartName")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                ComputerPartPrice = (double)reader.GetDecimal(reader.GetOrdinal("ComputerPartPrice")),
                                Currency = reader.GetString(reader.GetOrdinal("Currency")),
                                Webshop = reader.GetString(reader.GetOrdinal("WebshopURL")),
                                ProductUrl = reader.GetString(reader.GetOrdinal("ProductUrl"))
                            };

                            products.Add(product);
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.ToString());
                }
            }
            return products;
        }

        public string ConnectionCheck()
        {
            string msg = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    msg = "Succesfull connection";

                }
                catch (Exception ex)
                {
                    msg = "Error " + ex.Message;
                }
                finally
                {
                    if (connection != null) { connection.Close(); }
                }
            }
            return msg;
        }
    }
}
