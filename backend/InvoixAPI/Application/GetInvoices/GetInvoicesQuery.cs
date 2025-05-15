using InvoixAPI.Application.Dtos;
using MediatR;

namespace InvoixAPI.Application.GetInvoices;

public record GetInvoicesQuery : IRequest<IReadOnlyCollection<InvoiceDto>>;
