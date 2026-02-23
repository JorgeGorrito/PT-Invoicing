namespace Invoicing.Infrastructure.Exceptions;

/// <summary>
/// Excepción para errores técnicos de infraestructura (red, timeout, servicios externos caídos)
/// </summary>
public class InfrastructureException : Exception
{
    public InfrastructureException(string message) : base(message) { }

    public InfrastructureException(string message, Exception innerException) 
        : base(message, innerException) { }
}
