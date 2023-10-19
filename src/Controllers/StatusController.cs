using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace QuizzWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatusController : ControllerBase
{
    [HttpGet()]
    public IActionResult Get()
    {
        return new StatusCodeResult(StatusCodes.Status204NoContent);
    }

    [HttpPost()]
    public IActionResult Post()
    {
        return new StatusCodeResult(StatusCodes.Status204NoContent);
    }
}