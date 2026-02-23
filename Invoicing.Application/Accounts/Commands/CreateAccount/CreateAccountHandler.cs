using MediatR;
using Invoicing.Domain.Interfaces;
using Invoicing.Domain.ValueObjects;
using Invoicing.Domain.Constants;

namespace Invoicing.Application.Accounts.Commands.CreateAccount;

public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, bool>
{
    private readonly IAccountService _accountService;

    public CreateAccountHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<bool> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var token = await _accountService.LoginAsync();

        var externalAccount = new ExternalAccount
        {
            ClientCode = request.Code,
            Name = request.Name,
            Identification = request.Identification,
            CityCode = AccountConstants.CityCode,
            StateCode = AccountConstants.StateCode,
            CountryCode = AccountConstants.CountryCode,
            Address = request.Address,
            Phone = request.Phone,
            Email = request.Email,
            ClientType = AccountConstants.ClientType,
            RegistrationDate = DateTime.UtcNow,
            IdentificationType = AccountConstants.IdentificationType,
            LastName = request.LastName,
            FirstName = request.FirstName,
            PersonType = AccountConstants.PersonType,
            Status = AccountConstants.DefaultStatus,
            ExternalClientCode = request.ExternalClientCode,
            WebPage = request.WebPage
        };

        await _accountService.CreateAccountAsync(externalAccount, token);

        return true;
    }
}
