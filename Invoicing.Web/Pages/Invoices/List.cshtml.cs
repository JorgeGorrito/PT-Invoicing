using Invoicing.Web.Models;
using Invoicing.Web.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Invoicing.Web.Pages.Invoices;

public class ListModel : PageModel
{
    private readonly InvoiceService _invoiceService;

    public List<InvoiceDTO> Invoices { get; set; } = new();
    public string? ErrorMessage { get; set; }

    public ListModel(InvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    public async Task OnGetAsync()
    {
        try
        {
            Invoices = await _invoiceService.GetAllInvoicesAsync();
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
