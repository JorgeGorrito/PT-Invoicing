namespace Invoicing.Domain.Exceptions;

public enum DomainError
{
    // General
    GeneralError = 100,

    // Facturacion 
    InvoiceInvalidTotal = 200,
    InvoiceItemsRequired = 201,
    InvoiceInvalidClient = 202,
    InvoiceInvalidPricingService = 203,

    // Articulos
    ItemPriceInvalid = 300,
    ItemQuantityInvalid = 301,

    // Cliente
    ClientInvalidData = 400,
    ClientNotFound = 499,

    // Reglas de precios
    PricingRuleInvalid = 500,

    // Servicios Externos - Solo errores de negocio/validación
    ExternalServiceAuthenticationError = 601,
    ExternalServiceDataError = 602,
}