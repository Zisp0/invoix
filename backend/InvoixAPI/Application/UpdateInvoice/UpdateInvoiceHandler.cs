using InvoixAPI.Domain;
using InvoixAPI.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoixAPI.Application.UpdateInvoice;

public class UpdateInvoiceHandler : IRequestHandler<UpdateInvoiceCommand, bool>
{
    private readonly ApplicationDbContext _dbContext;

    public UpdateInvoiceHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _dbContext.Invoices
            .Include(i => i.Details)
            .FirstAsync(i => i.Id == request.Id, cancellationToken);
       
        invoice.Client = request.Client;
        invoice.Date = request.Date;

        var toDelete = invoice.Details
            .Where(d => request.DeletedDetailIds.Contains(d.Id))
            .ToList();

        _dbContext.InvoiceDetails.RemoveRange(toDelete);

        foreach (var detailDto in request.Details)
        {
            if (detailDto.Id != null)
            {
                var existing = await _dbContext.InvoiceDetails.FindAsync(detailDto.Id);
                if (existing != null)
                {
                    existing.Product = detailDto.Product;
                    existing.Quantity = detailDto.Quantity;
                    existing.UnitPrice = detailDto.UnitPrice;
                }
            }
            else
            {
                var newDetail = new InvoiceDetail
                {
                    Product = detailDto.Product,
                    Quantity = detailDto.Quantity,
                    UnitPrice = detailDto.UnitPrice,
                    InvoiceId = invoice.Id
                };
                _dbContext.InvoiceDetails.Add(newDetail);
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
