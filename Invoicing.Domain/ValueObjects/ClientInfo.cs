using Invoicing.Domain.Exceptions;

namespace Invoicing.Domain.ValueObjects;

public record ClientInfo {
    public const int MaxDocLength = 15;
    public const int MaxNameLength = 100;
    public const int MaxAddressLength = 200;

    public string DocNumber { get; }
    public string FirstName { get; } 
    public string LastName { get; }
    public string Address { get; }
    public string Phone { get; }

    private static void Validate(string value, int maxLength, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(
                DomainError.ClientInvalidData,
                $"El campo {fieldName} del cliente es obligatorio."
            );
        if (value.Length > maxLength)
            throw new DomainException(
                DomainError.ClientInvalidData,
                $"El campo {fieldName} del cliente no puede exceder los {maxLength} caracteres. Valor proporcionado: {value.Length}"
            );
    }

    public ClientInfo(
        string docNumber,
        string firstName,
        string lastName,
        string address,
        string phone
    )
    {
        Validate(docNumber, MaxDocLength, "El número de documento");
        Validate(firstName, MaxNameLength, "El nombre");
        Validate(lastName, MaxNameLength, "El apellido");
        Validate(address, MaxAddressLength, "La dirección");
        Validate(phone, MaxDocLength, "El teléfono");

        DocNumber = docNumber;
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        Phone = phone;
    }
}