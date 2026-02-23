using System.ComponentModel.DataAnnotations;

namespace Invoicing.Application.Invoices.Queries.GetInvoiceById;

/// <summary>
/// Modelo de respuesta con el detalle completo de una factura incluyendo sus items
/// </summary>
public record InvoiceDetailDTO
{
    /// <summary>
    /// Identificador único de la factura
    /// </summary>
    /// <example>1</example>
    public int Id { get; init; }

    /// <summary>
    /// Fecha y hora de creación de la factura (UTC)
    /// </summary>
    /// <example>2026-02-23T02:32:11.8486402</example>
    public DateTime Date { get; init; }

    /// <summary>
    /// Información del cliente
    /// </summary>
    public ClientInfoDTO Client { get; init; } = null!;

    /// <summary>
    /// Lista de artículos/items de la factura
    /// </summary>
    public List<InvoiceItemDetailDTO> Items { get; init; } = new();

    /// <summary>
    /// Valor bruto de la factura (suma de todos los ítems sin impuestos ni descuentos)
    /// </summary>
    /// <example>5000.00</example>
    public decimal GrossValue { get; init; }

    /// <summary>
    /// Valor del descuento aplicado (5% si supera el umbral de $500,000)
    /// </summary>
    /// <example>250.00</example>
    public decimal Discount { get; init; }

    /// <summary>
    /// Valor del IVA aplicado (19% sobre el valor después del descuento)
    /// </summary>
    /// <example>950.00</example>
    public decimal Tax { get; init; }

    /// <summary>
    /// Valor total de la factura (GrossValue - Discount + Tax)
    /// </summary>
    /// <example>5950.00</example>
    public decimal TotalValue { get; init; }
}

/// <summary>
/// Información del cliente en el detalle de la factura
/// </summary>
public record ClientInfoDTO
{
    /// <summary>
    /// Número de documento del cliente
    /// </summary>
    /// <example>1006875365</example>
    public string DocNumber { get; init; } = null!;

    /// <summary>
    /// Primer nombre del cliente
    /// </summary>
    /// <example>Jorge</example>
    public string FirstName { get; init; } = null!;

    /// <summary>
    /// Apellido del cliente
    /// </summary>
    /// <example>Abella</example>
    public string LastName { get; init; } = null!;

    /// <summary>
    /// Dirección del cliente
    /// </summary>
    /// <example>Calle 123 #45-67</example>
    public string Address { get; init; } = null!;

    /// <summary>
    /// Número de teléfono del cliente
    /// </summary>
    /// <example>3001234567</example>
    public string Phone { get; init; } = null!;
}

/// <summary>
/// Detalle de un artículo/ítem de la factura
/// </summary>
public record InvoiceItemDetailDTO
{
    /// <summary>
    /// Identificador único del ítem
    /// </summary>
    /// <example>1</example>
    public int Id { get; init; }

    /// <summary>
    /// Código del artículo
    /// </summary>
    /// <example>101</example>
    public int ArticleCode { get; init; }

    /// <summary>
    /// Cantidad de unidades
    /// </summary>
    /// <example>2</example>
    public int Quantity { get; init; }

    /// <summary>
    /// Precio unitario del artículo
    /// </summary>
    /// <example>2500.00</example>
    public decimal UnitPrice { get; init; }

    /// <summary>
    /// Valor total del ítem (Quantity * UnitPrice)
    /// </summary>
    /// <example>5000.00</example>
    public decimal Total { get; init; }
}
