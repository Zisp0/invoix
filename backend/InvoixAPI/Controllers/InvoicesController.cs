using InvoixAPI.Application.CreateInvoice;
using InvoixAPI.Application.GetInvoices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvoixAPI.Controllers;

public class InvoicesController : BaseController
{
    private readonly IMediator _mediator;
    public InvoicesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetInvoicesQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateInvoiceCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(Create), new { id }, id);
    }
}
