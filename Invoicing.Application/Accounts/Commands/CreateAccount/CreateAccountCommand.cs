using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Invoicing.Application.Accounts.Commands.CreateAccount;

/// <summary>
/// Comando para crear una nueva cuenta en el servicio externo de Novasoft
/// </summary>
public record CreateAccountCommand : IRequest<bool>
{
    /// <summary>
    /// Código único del cliente
    /// </summary>
    /// <example>456</example>
    [Required]
    public string Code { get; init; } = null!;

    /// <summary>
    /// Nombre completo o razón social
    /// </summary>
    /// <example>Empresa XYZ S.A.S</example>
    [Required]
    public string Name { get; init; } = null!;

    /// <summary>
    /// Número de identificación (NIT, CC, CE)
    /// </summary>
    /// <example>900123456</example>
    [Required]
    public string Identification { get; init; } = null!;

    /// <summary>
    /// Correo electrónico de contacto
    /// </summary>
    /// <example>contacto@empresa.com</example>
    [Required]
    [EmailAddress]
    public string Email { get; init; } = null!;

    /// <summary>
    /// Dirección física o comercial
    /// </summary>
    /// <example>Calle 100 #50-25, Oficina 301</example>
    [Required]
    public string Address { get; init; } = null!;

    /// <summary>
    /// Número de teléfono principal
    /// </summary>
    /// <example>6012345678</example>
    [Required]
    public string Phone { get; init; } = null!;

    /// <summary>
    /// Apellido del representante legal o contacto principal
    /// </summary>
    /// <example>García</example>
    [Required]
    public string LastName { get; init; } = null!;

    /// <summary>
    /// Nombre del representante legal o contacto principal
    /// </summary>
    /// <example>Juan</example>
    [Required]
    public string FirstName { get; init; } = null!;

    /// <summary>
    /// Código del cliente en sistema externo (opcional)
    /// </summary>
    /// <example>EXT001</example>
    public string? ExternalClientCode { get; init; }

    /// <summary>
    /// Página web de la empresa (opcional)
    /// </summary>
    /// <example>https://empresa.com</example>
    public string? WebPage { get; init; }
}