using System.Text.Json.Serialization;

namespace Invoicing.Infrastructure.ExternalModels;

public record NovasoftAccountRequest
{
    [JsonPropertyName("codCli")]
    public string ClientCode { get; init; } = null!;

    [JsonPropertyName("nomCli")]
    public string Name { get; init; } = null!;

    [JsonPropertyName("nitCli")]
    public string Identification { get; init; } = null!;

    [JsonPropertyName("codCiu")]
    public string CityCode { get; init; } = null!;

    [JsonPropertyName("codDep")]
    public string StateCode { get; init; } = null!;

    [JsonPropertyName("codPai")]
    public string CountryCode { get; init; } = null!;

    [JsonPropertyName("di2Cli")]
    public string Address { get; init; } = null!;

    [JsonPropertyName("te1Cli")]
    public string Phone { get; init; } = null!;

    [JsonPropertyName("tipCli")]
    public int ClientType { get; init; }

    [JsonPropertyName("fecIng")]
    public string RegistrationDate { get; init; } = null!;

    [JsonPropertyName("eMail")]
    public string Email { get; init; } = null!;

    [JsonPropertyName("tipIde")]
    public string IdentificationType { get; init; } = null!;

    [JsonPropertyName("ap1Cli")]
    public string LastName { get; init; } = null!;

    [JsonPropertyName("nom1Cli")]
    public string FirstName { get; init; } = null!;

    [JsonPropertyName("tipPer")]
    public int PersonType { get; init; }

    [JsonPropertyName("estCli")]
    public string Status { get; init; } = null!;

    [JsonPropertyName("codCliExtr")]
    public string? ExternalClientCode { get; init; }

    [JsonPropertyName("pagWeb")]
    public string? WebPage { get; init; }
}
