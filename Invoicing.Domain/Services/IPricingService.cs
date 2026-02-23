using Invoicing.Domain.ValueObjects;
using Invoicing.Domain.Entities;

namespace Invoicing.Domain.Services;

public interface IPricingService
{
    CalculationResult Calculate(IEnumerable<InvoiceItem> items);
}
