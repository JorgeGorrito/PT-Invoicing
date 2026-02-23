using MediatR;
using Invoicing.Domain.Entities;
using Invoicing.Domain.Interfaces;
using Invoicing.Domain.ValueObjects;
using Invoicing.Domain.Services;

namespace Invoicing.Application.Invoices.Commands.CreateInvoice;

public class CreateInvoiceHandler : IRequestHandler<CreateInvoiceCommand, int>
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IPricingConfigurationRepository _configPricingRepository;

    public CreateInvoiceHandler(
        IInvoiceRepository invoiceRepository,
        IPricingConfigurationRepository configPricingRepository)
    {
        _invoiceRepository = invoiceRepository;
        _configPricingRepository = configPricingRepository;
    }

    public async Task<int> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        ClientInfo clientInfo = new ClientInfo(
            docNumber: request.DocNumber,
            firstName: request.FirstName,
            lastName: request.LastName,
            address: request.Address,
            phone: request.Phone
        );

        Invoice invoice = new Invoice(clientInfo);
        foreach ( var item in request.Items)
        {
            invoice.AddItem(
                articleCode: item.ArticleCode,
                quantity: item.Quantity,
                unitPrice: item.UnitPrice
            );
        }

        PricingRules currentRules = await _configPricingRepository.GetCurrentRulesAsync();
        IPricingService standardPricingService = new StandardPricingService(currentRules);
        invoice.CalculateTotals(standardPricingService);

        await _invoiceRepository.AddAsync(invoice);

        return invoice.Id;
    }
}
