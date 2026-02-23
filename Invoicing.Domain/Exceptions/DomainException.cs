using System;

namespace Invoicing.Domain.Exceptions;
public class DomainException : Exception
{
    public DomainError ErrorCode { get;  }

    public DomainException(DomainError errorCode, string message, params Object[] args) : base(string.Format(message, args)) {
        ErrorCode = errorCode;
    }

    public DomainException(DomainError errorCode = DomainError.GeneralError) : base() { }
}

