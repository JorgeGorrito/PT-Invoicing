using Invoicing.Domain.Abstractions;
using Invoicing.Domain.Exceptions;
using Invoicing.Domain.Services;
using Invoicing.Domain.ValueObjects;

namespace Invoicing.Domain.Entities;

public class Invoice : BaseEntity<int>
{
    public DateTime Date { get; private set; }
    public ClientInfo Client { get; private set; } = null!;

    private readonly List<InvoiceItem> _items = new();
    public IReadOnlyCollection<InvoiceItem> Items => _items.AsReadOnly();

    public decimal GrossValue { get; private set; }
    public decimal Discount { get; private set; }
    public decimal Tax { get; private set; }
    public decimal TotalValue { get; private set; }

    private Invoice() { } // constructor privado para EF

    public Invoice(ClientInfo client)
    {
        if (client is null)
            throw new DomainException(
                DomainError.InvoiceInvalidClient,
                "La información del cliente es obligatoria para crear una factura."
            );

        Client = client;
        Date = DateTime.UtcNow;
    }

    public void AddItem(int articleCode, int quantity, decimal unitPrice)
    {
        InvoiceItem item = new(articleCode:  articleCode, quantity: quantity, unitPrice: unitPrice);
        _items.Add(item);
    }

    private void ResetTotals()
    {
        GrossValue = 0;
        Discount = 0;
        Tax = 0;
        TotalValue = 0;
    }

    public void CalculateTotals(IPricingService pricingService)
    {
        if (pricingService is null)
            throw new DomainException(
                DomainError.InvoiceInvalidPricingService,
                "El servicio de cálculo de precios es obligatorio para recalcular la factura."
            );

        if (!_items.Any())
        {
            ResetTotals();
            return;
        }

        CalculationResult result = pricingService.Calculate(Items);
        GrossValue = result.GrossValue;
        Discount = result.DiscountAmount;
        Tax = result.TaxAmount;
        TotalValue = result.TotalValue;
    }
}