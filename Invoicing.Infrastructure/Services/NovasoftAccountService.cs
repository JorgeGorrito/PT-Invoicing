using System.Net.Http.Headers;
using AutoMapper;
using Invoicing.Domain.Interfaces;
using Invoicing.Domain.ValueObjects;
using Invoicing.Domain.Exceptions;
using Invoicing.Infrastructure.ExternalModels;
using Invoicing.Infrastructure.Exceptions;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace Invoicing.Infrastructure.Services;

public class NovasoftAccountService : IAccountService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public NovasoftAccountService(HttpClient httpClient, IConfiguration configuration, IMapper mapper)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _mapper = mapper;

        var user = _configuration["NovasoftApi:User"];
        var password = _configuration["NovasoftApi:Password"];
        var connectionName = _configuration["NovasoftApi:ConnectionName"];

        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(connectionName))
            throw new InvalidOperationException(
                "Las credenciales de la API de Novasoft no están configuradas. " +
                "Verifique appsettings.json (NovasoftApi:User, NovasoftApi:Password, NovasoftApi:ConnectionName)");
    }

    public async Task<string> LoginAsync()
    {
        var user = _configuration["NovasoftApi:User"]!;
        var password = _configuration["NovasoftApi:Password"]!;
        var connectionName = _configuration["NovasoftApi:ConnectionName"]!;

        var loginRequest = new NovasoftLoginRequest
        {
            UserLogin = user,
            Password = password,
            ConnectionName = connectionName
        };

        try
        {
            var response = await _httpClient.PostAsJsonAsync("Cuenta/Login", loginRequest);

            if (!response.IsSuccessStatusCode)
            {
                throw response.StatusCode switch
                {
                    System.Net.HttpStatusCode.Unauthorized => new DomainException(
                        DomainError.ExternalServiceAuthenticationError,
                        "Credenciales inválidas para el servicio externo."),
                    System.Net.HttpStatusCode.ServiceUnavailable => new InfrastructureException(
                        "El servicio externo no está disponible temporalmente."),
                    _ => new InfrastructureException(
                        $"Error al autenticarse con el servicio externo. HTTP {(int)response.StatusCode}")
                };
            }

            var loginResponse = await response.Content.ReadFromJsonAsync<NovasoftLoginResponse>();

            return loginResponse?.Token ?? throw new InfrastructureException(
                "El servicio externo no devolvió un token válido.");
        }
        catch (DomainException)
        {
            throw;
        }
        catch (InfrastructureException)
        {
            throw;
        }
        catch (TaskCanceledException ex)
        {
            throw new InfrastructureException(
                "Tiempo de espera agotado al conectarse al servicio externo.", ex);
        }
        catch (HttpRequestException ex)
        {
            throw new InfrastructureException(
                "Error de red al conectarse al servicio externo.", ex);
        }
        catch (Exception ex)
        {
            throw new InfrastructureException(
                "Error inesperado al procesar la respuesta del servicio externo.", ex);
        }
    }

    public async Task<IEnumerable<ExternalAccount>> GetAccountsAsync(string token)
    {
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, "CXC/Senior/Accounts");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw response.StatusCode switch
                {
                    System.Net.HttpStatusCode.Unauthorized => new DomainException(
                        DomainError.ExternalServiceAuthenticationError,
                        "Token de autenticación inválido o expirado."),
                    System.Net.HttpStatusCode.Forbidden => new DomainException(
                        DomainError.ExternalServiceAuthenticationError,
                        "Sin permisos para acceder a las cuentas del servicio externo."),
                    System.Net.HttpStatusCode.ServiceUnavailable => new InfrastructureException(
                        "El servicio externo no está disponible temporalmente."),
                    _ => new InfrastructureException(
                        $"Error al obtener cuentas del servicio externo. HTTP {(int)response.StatusCode}")
                };
            }

            var accounts = await response.Content.ReadFromJsonAsync<List<NovasoftAccountResponse>>();

            return accounts?.Select(a => _mapper.Map<ExternalAccount>(a)) ?? Enumerable.Empty<ExternalAccount>();
        }
        catch (DomainException)
        {
            throw;
        }
        catch (InfrastructureException)
        {
            throw;
        }
        catch (TaskCanceledException ex)
        {
            throw new InfrastructureException(
                "Tiempo de espera agotado al obtener las cuentas.", ex);
        }
        catch (HttpRequestException ex)
        {
            throw new InfrastructureException(
                "Error de red al obtener las cuentas del servicio externo.", ex);
        }
        catch (Exception ex)
        {
            throw new InfrastructureException(
                "Error al procesar las cuentas del servicio externo.", ex);
        }
    }

    public async Task CreateAccountAsync(ExternalAccount account, string token)
    {
        var requestBody = _mapper.Map<NovasoftAccountRequest>(account);

        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, "CXC/Senior/Accounts");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Content = JsonContent.Create(requestBody);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw response.StatusCode switch
                {
                    System.Net.HttpStatusCode.Unauthorized => new DomainException(
                        DomainError.ExternalServiceAuthenticationError,
                        "Token de autenticación inválido o expirado."),
                    System.Net.HttpStatusCode.BadRequest => new DomainException(
                        DomainError.ExternalServiceDataError,
                        "Los datos de la cuenta son inválidos para el servicio externo."),
                    System.Net.HttpStatusCode.Conflict => new DomainException(
                        DomainError.ExternalServiceDataError,
                        "La cuenta ya existe en el servicio externo."),
                    System.Net.HttpStatusCode.ServiceUnavailable => new InfrastructureException(
                        "El servicio externo no está disponible temporalmente."),
                    _ => new InfrastructureException(
                        $"Error al crear la cuenta en el servicio externo. HTTP {(int)response.StatusCode}")
                };
            }
        }
        catch (DomainException)
        {
            throw;
        }
        catch (InfrastructureException)
        {
            throw;
        }
        catch (TaskCanceledException ex)
        {
            throw new InfrastructureException(
                "Tiempo de espera agotado al crear la cuenta.", ex);
        }
        catch (HttpRequestException ex)
        {
            throw new InfrastructureException(
                "Error de red al crear la cuenta en el servicio externo.", ex);
        }
        catch (Exception ex)
        {
            throw new InfrastructureException(
                "Error inesperado al crear la cuenta en el servicio externo.", ex);
        }
    }
}
