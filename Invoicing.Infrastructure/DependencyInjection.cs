using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Invoicing.Domain.Interfaces;
using Invoicing.Infrastructure.Persistence.Context;
using Invoicing.Infrastructure.Persistence.Repositories;
using Invoicing.Infrastructure.Services;

namespace Invoicing.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. Persistencia: SQL Server LocalDB
        services.AddDbContext<InvoicingDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(InvoicingDbContext).Assembly.FullName))
        );

        // 2. Repositorios (Fase 1)
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IPricingConfigurationRepository, PricingConfigurationRepository>();

        // 3. AutoMapper para mapeos de Infrastructure
        services.AddAutoMapper(typeof(DependencyInjection).Assembly);

        // 4. Integración con Novasoft (Fase 2)
        // Usamos AddHttpClient para IAccountService para gestionar el pooling de conexiones
        services.AddHttpClient<IAccountService, NovasoftAccountService>(client =>
        {
            var baseUrl = configuration["NovasoftApi:BaseUrl"];
            if (string.IsNullOrEmpty(baseUrl))
                throw new InvalidOperationException("La URL base de Novasoft no está configurada.");

            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        return services;
    }
}
