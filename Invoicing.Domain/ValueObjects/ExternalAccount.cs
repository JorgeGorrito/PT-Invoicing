namespace Invoicing.Domain.ValueObjects;

public record ExternalAccount
{
    public string ClientCode { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Identification { get; init; } = string.Empty;
    public string CityCode { get; init; } = string.Empty;
    public string StateCode { get; init; } = string.Empty;
    public string CountryCode { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public int ClientType { get; init; }
    public DateTime RegistrationDate { get; init; }
    public string IdentificationType { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public int PersonType { get; init; }
    public string Status { get; init; } = string.Empty;
    public string? ExternalClientCode { get; init; }
    public string? WebPage { get; init; }
}
