using FluentValidation;
using InvoixAPI.Application.Dtos;
using MediatR;

namespace InvoixAPI.Application.CreateInvoice;

public class CreateInvoiceCommand : IRequest<int>
{
    public string Client { get; set; } = null!;
    public DateTime Date { get; set; }
    public List<UpsertInvoiceDetailDto> Details { get; set; } = [];
    public decimal Total { get; set; }
}

public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
{
    public CreateInvoiceCommandValidator()
    {
        RuleFor(x => x.Client)
            .NotEmpty().WithMessage("El cliente es obligatorio.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("La fecha es obligatoria.");

        RuleForEach(x => x.Details)
            .ChildRules(detail =>
            {
                detail.RuleFor(d => d.Product).NotEmpty().WithMessage("El nombre de producto es obligatorio.");
                detail.RuleFor(d => d.Quantity).GreaterThan(0).WithMessage(d => $"La cantidad del producto '{d.Product}' debe ser mayor a 0.");
                detail.RuleFor(d => d.UnitPrice).GreaterThan(0).WithMessage(d => $"El precio del producto '{d.Product}' debe ser mayor a 0."); ;
            });
    }
}
