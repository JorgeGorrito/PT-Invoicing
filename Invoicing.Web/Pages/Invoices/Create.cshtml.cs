using Invoicing.Web.Models;
using Invoicing.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Invoicing.Web.Pages.Invoices;

public class CreateModel : PageModel
{
    private readonly InvoiceService _invoiceService;

    [BindProperty]
    public CreateInvoiceRequest Request { get; set; } = new();

    [TempData]
    public string? SuccessMessage { get; set; }

    [TempData]
    public string? ErrorMessage { get; set; }

    public CreateModel(InvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
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

        if (Request.Items == null || Request.Items.Count == 0)
        {
            ErrorMessage = "Debe agregar al menos un artículo a la factura.";
            return Page();
        }

        try
        {
            var invoiceId = await _invoiceService.CreateInvoiceAsync(Request);
            SuccessMessage = $"¡Factura creada exitosamente! Número de factura: {invoiceId}";
            return RedirectToPage("/Invoices/Create");
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error al crear la factura: {ex.Message}";
            return Page();
        }
    }
}
