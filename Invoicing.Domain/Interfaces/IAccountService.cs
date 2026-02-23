using Invoicing.Domain.ValueObjects;

namespace Invoicing.Domain.Interfaces;

public interface IAccountService
{
    Task<string> LoginAsync();
    Task<IEnumerable<ExternalAccount>> GetAccountsAsync(string token);
    Task CreateAccountAsync(ExternalAccount account, string token);
}
