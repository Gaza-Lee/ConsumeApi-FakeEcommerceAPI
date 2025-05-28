using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ConsumeApi.Models;

namespace ConsumeApi.APIService
{
    public class StoreApi
    {
        private readonly HttpClient _httpClient;

        public StoreApi()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Product>> GetStoreDataAsync(string url)
        {
            var response = await _httpClient.GetStringAsync(url); // Use the passed `url` parameter
            return JsonSerializer.Deserialize<List<Product>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Ensure case-insensitive mapping
            });
        }
    }
}