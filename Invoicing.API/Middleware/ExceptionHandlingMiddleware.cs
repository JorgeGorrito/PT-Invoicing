using System.Net;
using System.Text.Json;
using FluentValidation;
using Invoicing.Domain.Exceptions;
using Invoicing.Infrastructure.Exceptions;

namespace Invoicing.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try { await _next(context); }
        catch (Exception ex) { await HandleExceptionAsync(context, ex); }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(new { errors = validationException.Errors.Select(e => e.ErrorMessage) });
                break;
            case DomainException domainException:
                code = MapDomainErrorToHttpStatus(domainException.ErrorCode);
                result = JsonSerializer.Serialize(new 
                { 
                    errorCode = (int)domainException.ErrorCode,
                    error = domainException.Message 
                });
                _logger.LogWarning("Domain error: {ErrorCode} - {Message}", domainException.ErrorCode, domainException.Message);
                break;
            case InfrastructureException infraException:
                code = HttpStatusCode.BadGateway;
                result = JsonSerializer.Serialize(new { error = infraException.Message });
                _logger.LogError(infraException, "Infrastructure error: {Message}", infraException.Message);
                break;
            default:
                _logger.LogError(exception, "Error no controlado");
                result = JsonSerializer.Serialize(new { error = "Ocurrió un error interno en el servidor." });
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }

    private static HttpStatusCode MapDomainErrorToHttpStatus(DomainError errorCode)
    {
        return errorCode switch
        {
            DomainError.ClientNotFound => HttpStatusCode.NotFound,
            DomainError.ExternalServiceAuthenticationError => HttpStatusCode.Unauthorized,
            DomainError.ExternalServiceDataError => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.BadRequest
        };
    }
}