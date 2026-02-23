using Invoicing.Domain.Entities;

namespace Invoicing.Domain.Interfaces;

public interface IInvoiceRepository
{
    Task AddAsync(Invoice invoice);
    Task<IEnumerable<Invoice>> GetAllAsync();
    Task<Invoice?> GetByIdAsync(int id);
}
