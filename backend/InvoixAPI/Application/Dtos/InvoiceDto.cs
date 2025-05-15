namespace InvoixAPI.Application.Dtos;

public class InvoiceDto
{
    public int Id { get; set; }
    public string Client { get; set; } = null!;
    public DateTime Date { get; set; }
    public IReadOnlyCollection<InvoiceDetailDto> Details { get; set; } = [];
    public decimal Total { get; set; }
}
