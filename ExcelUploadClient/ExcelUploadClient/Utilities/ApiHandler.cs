using ExcelUploadClient.MVVM.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace ExcelUploadClient.Utilities
{
    internal class ApiHandler
    {
        public static async Task<string> SendDataAsync(DataTable dataTable, string apiUrl, string apiEndPoint)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // Set the base URL of the API
                    client.BaseAddress = new Uri(apiUrl);

                    // Serialize JSON data
                    string jsonData = JsonConvert.SerializeObject(dataTable);

                    // Configure and send the HTTP request
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(apiEndPoint, content);
                    
                    // Check the response
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        return $"Successful response: {responseContent}";
                    }
                    else
                    {
                        return $"Error occurred: {response.StatusCode}";
                    }
                }
                catch (Exception ex)
                {
                    return $"Error occured: {ex.Message}";
                }
            }
        }
        public static async Task<DataTable> GetJsonDataAsync(string apiUrl, string apiEndPoint)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(apiUrl);
                    var response = await client.GetAsync(apiEndPoint); 

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        // Process and convert JSON back to DataTable format
                        dynamic jsonObject = JsonConvert.DeserializeObject(jsonResponse);
                        JArray dataArray = jsonObject.value;

                        DataTable dataTable = new DataTable();
                        foreach (JObject item in dataArray)
                        {
                            if (dataTable.Columns.Count == 0)
                            {
                                foreach (var property in item.Properties())
                                {
                                    dataTable.Columns.Add(property.Name);
                                }
                            }
                            DataRow row = dataTable.NewRow();
                            foreach (var property in item.Properties())
                            {
                                row[property.Name] = property.Value.ToString();
                            }
                            dataTable.Rows.Add(row);
                        }

                        return dataTable;
                    }
                    else
                    {
                        throw new Exception($"Error occurred in server response: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error occurred during request: {ex.Message}");
                }
            }
        }
        public static async Task<string> DeleteDatabaseAsync(string apiUrl, string apiEndPoint)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // Construct the URL for the DELETE request
                    string deleteUrl = $"{apiUrl}/{apiEndPoint}";

                    // Send the DELETE request
                    HttpResponseMessage response = await client.DeleteAsync(deleteUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // The DELETE request was successful
                        string responseContent = await response.Content.ReadAsStringAsync();
                        return responseContent;
                    }
                    else
                    {
                        // Handle HTTP error
                        return $"HTTP error: {response.StatusCode}";
                    }
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    return $"Error occurred: {ex.Message}";
                }
            }
        }

    }
}
