using Invoicing.Web.Models;
using Invoicing.Web.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Invoicing.Web.Pages.Accounts;

public class ListModel : PageModel
{
    private readonly AccountService _accountService;

    public List<ExternalAccount> Accounts { get; set; } = new();
    public string? ErrorMessage { get; set; }

    public ListModel(AccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task OnGetAsync()
    {
        try
        {
            Accounts = await _accountService.GetAllAccountsAsync();
        }
        catch (InvalidOperationException ex)
        {
            ErrorMessage = ex.Message;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error inesperado: {ex.Message}";
        }
    }
}
