using Microsoft.AspNetCore.Mvc;

namespace InvoixAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class BaseController : ControllerBase
{
}
