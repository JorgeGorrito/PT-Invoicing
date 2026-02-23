using Invoicing.Application.Invoices.Commands.CreateInvoice;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Invoicing.Application.Invoices.Commands.CreateInvoice;

/// <summary>
/// Comando para crear una nueva factura
/// </summary>
public record CreateInvoiceCommand : IRequest<int>
{
    /// <summary>
    /// Número de documento del cliente (NIT, CC, CE)
    /// </summary>
    /// <example>1006875365</example>
    [Required]
    public string DocNumber { get; init; } = null!;

    /// <summary>
    /// Primer nombre del cliente
    /// </summary>
    /// <example>Jorge</example>
    [Required]
    public string FirstName { get; init; } = null!;

    /// <summary>
    /// Apellido del cliente
    /// </summary>
    /// <example>Abella</example>
    [Required]
    public string LastName { get; init; } = null!;

    /// <summary>
    /// Dirección de residencia o empresa del cliente
    /// </summary>
    /// <example>Calle 123 #45-67, Bogotá</example>
    [Required]
    public string Address { get; init; } = null!;

    /// <summary>
    /// Número de teléfono de contacto
    /// </summary>
    /// <example>3001234567</example>
    [Required]
    public string Phone { get; init; } = null!;

    /// <summary>
    /// Lista de artículos/items de la factura (mínimo 1 requerido)
    /// </summary>
    [Required]
    public List<InvoiceItemDTO> Items { get; init; } = new();
}
