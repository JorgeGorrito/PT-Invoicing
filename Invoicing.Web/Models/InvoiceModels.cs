namespace Invoicing.Web.Models;

public class InvoiceDTO
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string ClientDocNumber { get; set; } = string.Empty;
    public decimal GrossValue { get; set; }
    public decimal Discount { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
}

public class InvoiceDetailDTO
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public ClientInfoDTO Client { get; set; } = new();
    public List<InvoiceItemDetailDTO> Items { get; set; } = new();
    public decimal GrossValue { get; set; }
    public decimal Discount { get; set; }
    public decimal Tax { get; set; }
    public decimal TotalValue { get; set; }
}

public class ClientInfoDTO
{
    public string DocNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}

public class InvoiceItemDetailDTO
{
    public int Id { get; set; }
    public int ArticleCode { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total { get; set; }
}

public class CreateInvoiceRequest
{
    public string DocNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public List<InvoiceItemRequest> Items { get; set; } = new();
}

public class InvoiceItemRequest
{
    public int ArticleCode { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
