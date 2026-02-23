using Invoicing.Domain.Abstractions;
using Invoicing.Domain.Exceptions;

namespace Invoicing.Domain.Entities;

public class InvoiceItem : BaseEntity<int>
{
    public int ArticleCode { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }

    public decimal Total => Quantity * UnitPrice;

    // Foreign Key para EF Core (shadow property puede no funcionar con DDD)
    public int InvoiceId { get; private set; }

    private InvoiceItem() { } // constructor privado para EF

    internal InvoiceItem(int articleCode, int quantity, decimal unitPrice) {
        if (quantity <= 0)
            throw new DomainException(
                DomainError.ItemQuantityInvalid,
                "La cantidad del artículo debe ser mayor a cero. Valor proporcionado: {0}",
                articleCode
            );

        if (unitPrice < 0)
            throw new DomainException(
                DomainError.ItemPriceInvalid,
                "El precio unitario del artículo no puede ser negativo. Valor proporcionado: {0}",
                articleCode
            );

        ArticleCode = articleCode;
        Quantity = quantity;
        UnitPrice = unitPrice;
    } 
}