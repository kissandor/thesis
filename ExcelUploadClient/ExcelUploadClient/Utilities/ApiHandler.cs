using ExcelUploadClient.VMVM.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
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
                    // Beállítjuk az API alapcímét
                    client.BaseAddress = new Uri(apiUrl);

                    // JSON adat serializálása
                    string jsonData = JsonConvert.SerializeObject(dataTable);

                    // HTTP kérés konfigurálása és elküldése
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(apiEndPoint, content);

                    // Ellenőrizzük a választ
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        return $"Sikeres válasz: {responseContent}";
                    }
                    else
                    {
                        return $"Hiba történt: {response.StatusCode}";
                    }
                }
                catch (Exception ex)
                {
                    return $"Hiba történt: {ex.Message}";
                }
            }
        }

        public static async Task<ObservableCollection<ComputerPart>> GetComputerParts(string apiUrl, string apiEndpoint)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(apiUrl);
                    var response = await client.GetAsync(apiEndpoint);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        //var result = JsonConvert.DeserializeObject<ApiResult>(jsonResponse);
                        var result = JsonConvert.DeserializeObject<ApiResult>(jsonResponse);

                        if (result != null)
                        {
                            return new ObservableCollection<ComputerPart>(result.value);
                        }
                        else
                        {
                            throw new Exception("Nem található adat a válaszban.");
                        }
                    }
                    else
                    {
                        throw new Exception($"Hiba történt a szerver válaszában: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Hiba történt a kérés során: {ex.Message}");
                }
            }
        }

        public static DataTable GetJsonData(string apiUrl, string apiEndPoint)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(apiUrl);
                    var response = client.GetAsync(apiEndPoint).Result; // Kérés elküldése és válasz megvárása

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = response.Content.ReadAsStringAsync().Result;

                        // JSON feldolgozása és visszaalakítása DataTable formátumra
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
                        throw new Exception($"Hiba történt a szerver válaszában: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Hiba történt a kérés során: {ex.Message}");
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
                    var response = await client.GetAsync(apiEndPoint); // Aszinkron hívás

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync(); // Aszinkron adatolvasás

                        // JSON feldolgozása és visszaalakítása DataTable formátumra
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
                        throw new Exception($"Hiba történt a szerver válaszában: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Hiba történt a kérés során: {ex.Message}");
                }
            }
        }

    }

    public class ApiResult
    {
        public List<ComputerPart> value { get; set; }
    }
}
