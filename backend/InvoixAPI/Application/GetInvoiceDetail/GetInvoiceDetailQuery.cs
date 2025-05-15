using FluentValidation;
using InvoixAPI.Application.Dtos;
using InvoixAPI.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoixAPI.Application.GetInvoiceDetail;

public class GetInvoiceDetailQuery : IRequest<InvoiceDto>
{
    public int Id { get; set; }
}

public class GetInvoiceDetailQueryValidator : AbstractValidator<GetInvoiceDetailQuery>
{
    public GetInvoiceDetailQueryValidator(ApplicationDbContext dbContext)
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El ID debe ser mayor que 0.")
            .MustAsync(async (id, ct) => await dbContext.Invoices.AnyAsync(i => i.Id == id, ct))
            .WithMessage("La factura con el ID especificado no existe.");
    }
}