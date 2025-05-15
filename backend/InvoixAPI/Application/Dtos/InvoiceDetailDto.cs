namespace InvoixAPI.Application.Dtos;

public class InvoiceDetailDto
{
    public int? Id { get; set; }
    public string Product { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal => Quantity * UnitPrice;
}
