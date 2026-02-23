using System.Text.Json.Serialization;

namespace Invoicing.Infrastructure.ExternalModels;

public record NovasoftLoginRequest
{
    [JsonPropertyName("userLogin")]
    public string UserLogin { get; init; } = null!;

    [JsonPropertyName("password")]
    public string Password { get; init; } = null!;

    [JsonPropertyName("connectionName")]
    public string ConnectionName { get; init; } = null!;
}
