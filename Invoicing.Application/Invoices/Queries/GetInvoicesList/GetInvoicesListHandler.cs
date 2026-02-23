using Invoicing.Domain.Interfaces;

using AutoMapper;
using MediatR;

namespace Invoicing.Application.Invoices.Queries.GetInvoicesList;

public class GetInvoicesListHandler : IRequestHandler<GetInvoicesListQuery, List<InvoiceDTO>>
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IMapper _mapper;

    public GetInvoicesListHandler(IInvoiceRepository invoiceRepository, IMapper mapper)
    {
        _invoiceRepository = invoiceRepository;
        _mapper = mapper;
    }

    public async Task<List<InvoiceDTO>> Handle(GetInvoicesListQuery request, CancellationToken cancellationToken)
    {
        var invoices = await _invoiceRepository.GetAllAsync();
        return _mapper.Map<List<InvoiceDTO>>(invoices);
    }
}
