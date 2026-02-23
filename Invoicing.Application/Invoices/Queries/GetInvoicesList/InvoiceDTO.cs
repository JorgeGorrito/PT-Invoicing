using System.ComponentModel.DataAnnotations;

namespace Invoicing.Application.Invoices.Queries.GetInvoicesList;

/// <summary>
/// Modelo de respuesta para consulta de facturas
/// </summary>
public record InvoiceDTO
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
    /// Nombre completo del cliente
    /// </summary>
    /// <example>Jorge Abella</example>
    public string ClientName { get; init; } = null!;

    /// <summary>
    /// Número de documento del cliente
    /// </summary>
    /// <example>1006875365</example>
    public string ClientDocNumber { get; init; } = null!;

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
    public decimal Total { get; init; }
}
