using Invoicing.Domain.Exceptions;

namespace Invoicing.Domain.ValueObjects;

public record PricingRules
{
    public decimal DiscountThreshold { get; }
    public decimal DiscountRate { get; }
    public decimal TaxRate { get; }

    public PricingRules(decimal discountThreshold, decimal discountRate, decimal taxRate)
    {
        if (discountThreshold < 0)
            throw new DomainException(
                DomainError.PricingRuleInvalid,
                $"El umbral de descuento no puede ser negativo. Valor proporcionado: {discountThreshold}"
            );

        if (discountRate < 0 || discountRate > 1)
            throw new DomainException(
                DomainError.PricingRuleInvalid,
                $"La tasa de descuento debe estar entre 0 y 1. Valor proporcionado: {discountRate}"
            );

        if (taxRate < 0 || taxRate > 1)
            throw new DomainException(
                DomainError.PricingRuleInvalid,
                $"La tasa de impuesto debe estar entre 0 y 1. Valor proporcionado: {taxRate}"
            );

        DiscountThreshold = discountThreshold;
        DiscountRate = discountRate;
        TaxRate = taxRate;
    }
}
