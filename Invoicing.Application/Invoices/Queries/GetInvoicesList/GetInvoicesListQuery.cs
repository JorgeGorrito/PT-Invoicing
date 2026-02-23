using MediatR;

namespace Invoicing.Application.Invoices.Queries.GetInvoicesList;

public class GetInvoicesListQuery : IRequest<List<InvoiceDTO>>
{
}
