using FluentValidation;
using InvoixAPI.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoixAPI.Application.DeleteInvoice;

public class DeleteInvoiceCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class DeleteInvoiceCommandValidator : AbstractValidator<DeleteInvoiceCommand>
{
    public DeleteInvoiceCommandValidator(ApplicationDbContext dbContext)
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El ID debe ser mayor que 0.")
            .MustAsync(async (id, ct) => await dbContext.Invoices.AnyAsync(i => i.Id == id, ct))
            .WithMessage("La factura con el ID especificado no existe.");
    }
}
