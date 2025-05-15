using InvoixAPI.Infrastructure;
using InvoixAPI.Domain;
using MediatR;

namespace InvoixAPI.Application.CreateInvoice;

public class CreateInvoiceHandler : IRequestHandler<CreateInvoiceCommand, int>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateInvoiceHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = new Invoice
        {
            Client = request.Client,
            Date = DateTime.SpecifyKind(request.Date, DateTimeKind.Utc),
            Details = request.Details.Select(d => new InvoiceDetail
            {
                Product = d.Product,
                Quantity = d.Quantity,
                UnitPrice = d.UnitPrice
            }).ToList()
        };

        _dbContext.Invoices.Add(invoice);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return invoice.Id;
    }
}
