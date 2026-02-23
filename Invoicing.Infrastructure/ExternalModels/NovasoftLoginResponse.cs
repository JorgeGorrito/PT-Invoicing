using System.Text.Json.Serialization;

namespace Invoicing.Infrastructure.ExternalModels;

public record NovasoftLoginResponse
{
    [JsonPropertyName("token")]
    public string Token { get; init; } = null!;

    [JsonPropertyName("expiration")]
    public DateTime? Expiration { get; init; }
}
