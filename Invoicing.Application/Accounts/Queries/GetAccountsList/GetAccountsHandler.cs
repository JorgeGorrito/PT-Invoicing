using MediatR;
using Invoicing.Domain.Interfaces;
using Invoicing.Domain.ValueObjects;

namespace Invoicing.Application.Accounts.Queries.GetAccountsList;

public class GetAccountsHandler : IRequestHandler<GetAccountsQuery, IEnumerable<ExternalAccount>>
{
    private readonly IAccountService _accountService;

    public GetAccountsHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<IEnumerable<ExternalAccount>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        var token = await _accountService.LoginAsync();

        return await _accountService.GetAccountsAsync(token);
    }
}