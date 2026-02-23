using MediatR;

namespace Invoicing.Application.Invoices.Queries.GetInvoiceById;

/// <summary>
/// Query para obtener el detalle completo de una factura por su ID
/// </summary>
public record GetInvoiceByIdQuery(int Id) : IRequest<InvoiceDetailDTO?>;
