using Invoix.Infrastructure;
using InvoixAPI.Application.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoixAPI.Application.GetInvoices;

public class GetInvoicesHandler : IRequestHandler<GetInvoicesQuery, IReadOnlyCollection<InvoiceDto>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetInvoicesHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<InvoiceDto>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Invoices.Select(invoice => new InvoiceDto
        {
            Id = invoice.Id,
            Client = invoice.Client,
            Date = invoice.Date
        }).ToListAsync(cancellationToken);
    }
}
