using InvoixAPI.Application.Dtos;
using InvoixAPI.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoixAPI.Application.GetInvoiceDetail;

public class GetInvoiceDetailHandler : IRequestHandler<GetInvoiceDetailQuery, InvoiceDto>
{
    private readonly ApplicationDbContext _dbContext;

    public GetInvoiceDetailHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<InvoiceDto> Handle(GetInvoiceDetailQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Invoices.Select(invoice => new InvoiceDto
        {
            Id = invoice.Id,
            Client = invoice.Client,
            Date = invoice.Date,
            Details = invoice.Details.Select(d => new InvoiceDetailDto
            {
                Id = d.Id,
                Product = d.Product,
                Quantity = d.Quantity,
                UnitPrice = d.UnitPrice
            }).ToList(),
            Total = invoice.Details.Sum(d => d.Quantity * d.UnitPrice)
        }).FirstAsync();
    }
}
