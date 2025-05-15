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
}
