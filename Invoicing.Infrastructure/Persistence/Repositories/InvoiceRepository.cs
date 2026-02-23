using Microsoft.EntityFrameworkCore;
using Invoicing.Domain.Entities;
using Invoicing.Domain.Interfaces;
using Invoicing.Infrastructure.Persistence.Context;

namespace Invoicing.Infrastructure.Persistence.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly InvoicingDbContext _context;

    public InvoiceRepository(InvoicingDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Invoice invoice)
    {
        await _context.Invoices.AddAsync(invoice);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Invoice>> GetAllAsync()
    {
        return await _context.Invoices
            .Include(x => x.Items)
            .OrderByDescending(x => x.Date)
            .ToListAsync();
    }

    public async Task<Invoice?> GetByIdAsync(int id)
    {
        return await _context.Invoices
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}