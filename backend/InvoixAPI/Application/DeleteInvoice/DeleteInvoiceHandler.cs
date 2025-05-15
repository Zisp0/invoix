using InvoixAPI.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoixAPI.Application.DeleteInvoice;

public class DeleteInvoiceHandler : IRequestHandler<DeleteInvoiceCommand, bool>
{
    private readonly ApplicationDbContext _dbContext;

    public DeleteInvoiceHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _dbContext.Invoices
            .Include(i => i.Details)
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        if (invoice is null)
            throw new KeyNotFoundException("La factura no existe.");

        _dbContext.InvoiceDetails.RemoveRange(invoice.Details);
        _dbContext.Invoices.Remove(invoice);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
