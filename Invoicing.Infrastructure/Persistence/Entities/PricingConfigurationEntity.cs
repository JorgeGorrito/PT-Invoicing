namespace Invoicing.Infrastructure.Persistence.Entities;

public class PricingConfigurationEntity
{
    public int Id { get; set; }
    public decimal TaxRate { get; set; }
    public decimal DiscountRate { get; set; }
    public decimal DiscountThreshold { get; set; }
    public DateTime CreatedAt { get; set; }
}
