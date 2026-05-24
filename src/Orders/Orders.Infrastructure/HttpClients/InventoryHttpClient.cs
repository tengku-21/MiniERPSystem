using System.Net.Http.Json;
using Orders.Application.DTOs;
using Orders.Application.Interfaces;

namespace Orders.Infrastructure.HttpClients;

public class InventoryHttpClient(HttpClient httpClient) : IInventoryClient
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<StockDto?> GetStockAsync(Guid productId)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<StockDto>($"api/inventory/{productId}");
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }

    public async Task<bool> DeductStockAsync(Guid productId, int quantity)
    {
        var response = await _httpClient.PutAsJsonAsync(
            $"api/inventory/{productId}/deduct", new { quantity });
        return response.IsSuccessStatusCode;
    }
}