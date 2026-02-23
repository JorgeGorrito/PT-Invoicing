namespace Invoicing.Domain.ValueObjects;

public record CalculationResult(
    decimal GrossValue,
    decimal DiscountAmount,
    decimal TaxAmount,
    decimal TotalValue
);