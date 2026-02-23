using Invoicing.Web.Models;
using Invoicing.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Invoicing.Web.Pages.Accounts;

public class CreateModel : PageModel
{
    private readonly AccountService _accountService;

    [BindProperty]
    public CreateAccountRequest Request { get; set; } = new();

    [TempData]
    public string? SuccessMessage { get; set; }

    [TempData]
    public string? ErrorMessage { get; set; }

    public CreateModel(AccountService accountService)
    {
        _accountService = accountService;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            ErrorMessage = "Por favor complete todos los campos requeridos.";
            return Page();
        }

        try
        {
            var result = await _accountService.CreateAccountAsync(Request);
            if (result)
            {
                SuccessMessage = "¡Cuenta creada exitosamente en el sistema de Novasoft!";
                return RedirectToPage("/Accounts/Create");
            }
            else
            {
                ErrorMessage = "No se pudo crear la cuenta. Por favor intente nuevamente.";
                return Page();
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error al crear la cuenta: {ex.Message}";
            return Page();
        }
    }
}
