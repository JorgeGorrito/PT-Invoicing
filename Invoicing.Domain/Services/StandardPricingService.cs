using Invoicing.Domain.Entities;
using Invoicing.Domain.ValueObjects;

namespace Invoicing.Domain.Services;

public class StandardPricingService : IPricingService
{
    private readonly PricingRules _rules = null!;

    public StandardPricingService(PricingRules rules)
    {
        _rules = rules ?? throw new ArgumentNullException(nameof(rules));
    }

    public CalculationResult Calculate(IEnumerable<InvoiceItem> items)
    {
        decimal grossValue = items.Sum( item => item.Total);
        decimal discount = 0;

        if ( grossValue > _rules.DiscountThreshold )
        {
            discount = grossValue * _rules.DiscountRate;
        }

        decimal baseForTax = grossValue - discount;
        decimal tax = baseForTax * _rules.TaxRate;
        decimal total = baseForTax + tax;

        return new CalculationResult(
            GrossValue: grossValue, 
            DiscountAmount: discount, 
            TaxAmount: tax,
            TotalValue: total
        );
    }
}
