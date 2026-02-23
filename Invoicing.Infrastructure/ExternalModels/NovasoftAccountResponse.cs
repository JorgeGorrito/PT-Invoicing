using System.Text.Json.Serialization;

namespace Invoicing.Infrastructure.ExternalModels;

public record NovasoftAccountResponse
{
    [JsonPropertyName("codCli")]
    public string ClientCode { get; init; } = null!;

    [JsonPropertyName("nitCli")]
    public string Identification { get; init; } = null!;

    [JsonPropertyName("digVer")]
    public string VerificationDigit { get; init; } = null!;

    [JsonPropertyName("nomCli")]
    public string FullName { get; init; } = null!;

    [JsonPropertyName("nom1Cli")]
    public string? FirstName { get; init; }

    [JsonPropertyName("ap1Cli")]
    public string? LastName { get; init; }

    [JsonPropertyName("eMail")]
    public string Email { get; init; } = null!;

    [JsonPropertyName("te1Cli")]
    public string? Phone { get; init; }

    [JsonPropertyName("di1Cli")]
    public string? Address { get; init; }

    [JsonPropertyName("codCiu")]
    public string CityCode { get; init; } = null!;

    [JsonPropertyName("codDep")]
    public string StateCode { get; init; } = null!;

    [JsonPropertyName("codPai")]
    public string CountryCode { get; init; } = null!;

    [JsonPropertyName("fecIng")]
    public DateTime RegistrationDate { get; init; }

    [JsonPropertyName("tipCli")]
    public int ClientType { get; init; }
}