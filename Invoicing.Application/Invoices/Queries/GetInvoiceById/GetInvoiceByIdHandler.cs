using AutoMapper;
using Invoicing.Domain.Interfaces;
using MediatR;

namespace Invoicing.Application.Invoices.Queries.GetInvoiceById;

/// <summary>
/// Handler para obtener el detalle completo de una factura por ID
/// </summary>
public class GetInvoiceByIdHandler : IRequestHandler<GetInvoiceByIdQuery, InvoiceDetailDTO?>
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IMapper _mapper;

    public GetInvoiceByIdHandler(IInvoiceRepository invoiceRepository, IMapper mapper)
    {
        _invoiceRepository = invoiceRepository;
        _mapper = mapper;
    }

    public async Task<InvoiceDetailDTO?> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(request.Id);

        if (invoice is null)
            return null;

        return _mapper.Map<InvoiceDetailDTO>(invoice);
    }
}
