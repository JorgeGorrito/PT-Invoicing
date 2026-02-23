using System.Reflection;
using FluentValidation;
using Invoicing.Application.Common.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // 1. Registra MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        // 2. Registra automáticamente todos los Validators de FluentValidation
        services.AddValidatorsFromAssembly(assembly);

        // 3. Registra el Pipeline de Validación
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        // 4. Registra AutoMapper
        services.AddAutoMapper(assembly);

        return services;
    }
}