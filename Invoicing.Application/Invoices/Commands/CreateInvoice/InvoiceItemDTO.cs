using System.ComponentModel.DataAnnotations;

namespace Invoicing.Application.Invoices.Commands.CreateInvoice;

/// <summary>
/// Representa un artículo/ítem dentro de una factura
/// </summary>
/// <param name="ArticleCode">Código único del artículo</param>
/// <param name="Quantity">Cantidad de unidades (debe ser mayor a 0)</param>
/// <param name="UnitPrice">Precio unitario del artículo (debe ser mayor a 0)</param>
public record InvoiceItemDTO(
    /// <example>101</example>
    [Required]
    int ArticleCode,

    /// <example>2</example>
    [Required]
    [Range(1, int.MaxValue)]
    int Quantity,

    /// <example>2500.50</example>
    [Required]
    [Range(0.01, double.MaxValue)]
    decimal UnitPrice
);
