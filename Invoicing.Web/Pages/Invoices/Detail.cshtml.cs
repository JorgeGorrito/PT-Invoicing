using Invoicing.Web.Models;
using Invoicing.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Invoicing.Web.Pages.Invoices;

public class DetailModel : PageModel
{
    private readonly InvoiceService _invoiceService;

    public InvoiceDetailDTO? Invoice { get; set; }

    public DetailModel(InvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Invoice = await _invoiceService.GetInvoiceByIdAsync(id);

        if (Invoice == null)
        {
            return Page();
        }

        return Page();
    }
}
