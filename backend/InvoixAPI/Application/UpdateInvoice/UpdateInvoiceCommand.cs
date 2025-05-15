using FluentValidation;
using InvoixAPI.Application.Dtos;
using InvoixAPI.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoixAPI.Application.UpdateInvoice;

public class UpdateInvoiceCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string Client { get; set; } = null!;
    public DateTime Date { get; set; }
    public List<UpsertInvoiceDetailDto> Details { get; set; } = [];
    public List<int> DeletedDetailIds { get; set; } = [];
    public decimal Total { get; set; }
}

public class UpdateInvoiceCommandValidator : AbstractValidator<UpdateInvoiceCommand>
{
    public UpdateInvoiceCommandValidator(ApplicationDbContext dbContext)
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El ID debe ser mayor que 0.")
            .MustAsync(async (id, ct) => await dbContext.Invoices.AnyAsync(i => i.Id == id, ct))
            .WithMessage("La factura con el ID especificado no existe.");

        RuleFor(x => x.Client)
            .NotEmpty().WithMessage("El cliente es obligatorio.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("La fecha es obligatoria.");

        RuleForEach(x => x.Details)
            .ChildRules(detail =>
            {
                detail.RuleFor(d => d.Product).NotEmpty().WithMessage("El nombre de producto es obligatorio.");
                detail.RuleFor(d => d.Quantity).GreaterThan(0).WithMessage(d => $"La cantidad del producto '{d.Product}' debe ser mayor a 0.");
                detail.RuleFor(d => d.UnitPrice).GreaterThan(0).WithMessage(d => $"El precio del producto '{d.Product}' debe ser mayor a 0.");
                detail.When(d => d.Id != null, () =>
                {
                    detail.RuleFor(d => d.Id)
                        .MustAsync(async (id, ct) =>
                            await dbContext.InvoiceDetails.AnyAsync(det => det.Id == id, ct))
                        .WithMessage("Uno o más detalles no existen.");
                });
            });
    }
}