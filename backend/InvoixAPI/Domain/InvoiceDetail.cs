namespace InvoixAPI.Domain;

public class InvoiceDetail
{
    public int Id { get; set; }
    public string Product { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal => Quantity * UnitPrice;

    public int InvoiceId { get; set; }
}
