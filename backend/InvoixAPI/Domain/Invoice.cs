namespace InvoixAPI.Domain;

public class Invoice
{
    public int Id { get; set; }
    public string Client { get; set; } = string.Empty;
    public DateTime Date { get; set; }

    public decimal Total => Details.Sum(d => d.Subtotal);

    public List<InvoiceDetail> Details { get; set; } = new();
}
