using Invoicing.Domain.ValueObjects;

namespace Invoicing.Domain.Interfaces;

public interface IPricingConfigurationRepository
{
    Task<PricingRules> GetCurrentRulesAsync();
}
