using Invoicing.Web.Models;
using System.Net.Http.Json;

namespace Invoicing.Web.Services;

public class AccountService
{
    private readonly HttpClient _httpClient;

    public AccountService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ExternalAccount>> GetAllAccountsAsync()
    {
        try
        {
            var accounts = await _httpClient.GetFromJsonAsync<List<ExternalAccount>>("api/accounts");
            return accounts ?? new List<ExternalAccount>();
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidOperationException(
                $"No se pudo conectar con la API. Asegúrate de que el proyecto Invoicing.API esté ejecutándose en {_httpClient.BaseAddress}", 
                ex);
        }
    }

    public async Task<bool> CreateAccountAsync(CreateAccountRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/accounts", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidOperationException(
                $"No se pudo conectar con la API. Asegúrate de que el proyecto Invoicing.API esté ejecutándose en {_httpClient.BaseAddress}", 
                ex);
        }
    }
}
