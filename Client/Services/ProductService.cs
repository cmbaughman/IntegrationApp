using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared;
using System.Text.Json;

namespace Client.Services
{
    public class ProductService
    {
        private readonly HttpClient _http;
        private Product[]? _cachedProducts;

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        public async Task<Product[]?> GetProductsAsync()
        {
            if (_cachedProducts == null) 
            {
                try
                {
                    var response = await _http.GetAsync("http://localhost:5251/api/v1/productlist");
                    response.EnsureSuccessStatusCode(); // COPILOT: Throw an exception if the request fails

                    var json = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    _cachedProducts = JsonSerializer.Deserialize<Product[]>(json, options);
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Network error: {ex.Message}");
                    // CoPilot: Handle the error appropriately (e.g., log, display a message)
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                    // COPILOT: Handle the error appropriately
                }
            }

            return _cachedProducts;
        }
    }
}