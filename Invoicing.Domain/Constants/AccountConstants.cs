namespace Invoicing.Domain.Constants;

/// <summary>
/// Valores constantes para la creación de cuentas en el servicio externo
/// Definidos según especificaciones de la prueba técnica
/// </summary>
public static class AccountConstants
{
    /// <summary>
    /// Código de ciudad fijo (11001 - Bogotá)
    /// </summary>
    public const string CityCode = "11001";

    /// <summary>
    /// Código de departamento/estado fijo (11 - Bogotá D.C.)
    /// </summary>
    public const string StateCode = "11";

    /// <summary>
    /// Código de país fijo (057 - Colombia)
    /// </summary>
    public const string CountryCode = "057";

    /// <summary>
    /// Tipo de cliente fijo (2)
    /// </summary>
    public const int ClientType = 2;

    /// <summary>
    /// Tipo de identificación fijo (06)
    /// </summary>
    public const string IdentificationType = "06";

    /// <summary>
    /// Tipo de persona fijo (2)
    /// </summary>
    public const int PersonType = 2;

    /// <summary>
    /// Estado por defecto de la cuenta (1 - Activo)
    /// </summary>
    public const string DefaultStatus = "1";
}
