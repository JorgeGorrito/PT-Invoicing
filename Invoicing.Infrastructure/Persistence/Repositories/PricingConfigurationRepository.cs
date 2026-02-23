using Invoicing.Domain.ValueObjects;
using Invoicing.Domain.Interfaces;
using Invoicing.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Invoicing.Infrastructure.Persistence.Repositories;

public class PricingConfigurationRepository : IPricingConfigurationRepository
{
    private readonly InvoicingDbContext _context;

    public PricingConfigurationRepository(InvoicingDbContext context)
    {
        _context = context;
    }

    public async Task<PricingRules> GetCurrentRulesAsync()
    {
        // Obtiene la última configuración registrada
        var config = await _context.PricingConfigurations
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync();

        // Si no existe, retorna valores por defecto de la prueba técnica
        if (config == null)
        {
            return new PricingRules(
                discountThreshold: 500000m,
                discountRate: 0.05m,
                taxRate: 0.19m
            );
        }

        // Mapea la entidad de infraestructura al Value Object de dominio
        return new PricingRules(
            discountThreshold: config.DiscountThreshold,
            discountRate: config.DiscountRate,
            taxRate: config.TaxRate
        );
    }
}