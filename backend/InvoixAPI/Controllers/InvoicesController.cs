using InvoixAPI.Application.CreateInvoice;
using InvoixAPI.Application.DeleteInvoice;
using InvoixAPI.Application.GetInvoiceDetail;
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

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _mediator.Send(new GetInvoiceDetailQuery { Id = id });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateInvoiceCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteInvoiceCommand { Id = id });
        return Ok(result);
    }
}
