using Invoicing.Web.Models;
using System.Net.Http.Json;

namespace Invoicing.Web.Services;

public class InvoiceService
{
    private readonly HttpClient _httpClient;

    public InvoiceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<InvoiceDTO>> GetAllInvoicesAsync()
    {
        try
        {
            var invoices = await _httpClient.GetFromJsonAsync<List<InvoiceDTO>>("api/invoices");
            return invoices ?? new List<InvoiceDTO>();
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidOperationException(
                $"No se pudo conectar con la API. Asegúrate de que el proyecto Invoicing.API esté ejecutándose en {_httpClient.BaseAddress}", 
                ex);
        }
    }

    public async Task<InvoiceDetailDTO?> GetInvoiceByIdAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<InvoiceDetailDTO>($"api/invoices/{id}");
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidOperationException(
                $"No se pudo conectar con la API. Asegúrate de que el proyecto Invoicing.API esté ejecutándose en {_httpClient.BaseAddress}", 
                ex);
        }
    }

    public async Task<int> CreateInvoiceAsync(CreateInvoiceRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/invoices", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidOperationException(
                $"No se pudo conectar con la API. Asegúrate de que el proyecto Invoicing.API esté ejecutándose en {_httpClient.BaseAddress}", 
                ex);
        }
    }
}
