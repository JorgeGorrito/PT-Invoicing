using MediatR;
using Invoicing.Domain.ValueObjects;

namespace Invoicing.Application.Accounts.Queries.GetAccountsList;

public record GetAccountsQuery : IRequest<IEnumerable<ExternalAccount>>;