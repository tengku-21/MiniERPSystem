using System.Net.Http.Json;
using Orders.Application.DTOs;
using Orders.Application.Interfaces;

namespace Orders.Infrastructure.HttpClients;

public class ProductHttpClient(HttpClient httpClient) : IProductClient
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<ProductDto?> GetProductAsync(Guid productId)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<ProductDto>($"api/products/{productId}");
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }
}