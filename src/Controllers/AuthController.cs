using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace QuizzWebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    public AuthController()
    {
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    public IActionResult TestV1()
    {
        return Ok("auth v1");
    }

    [HttpGet]
    [MapToApiVersion("2.0")]
    public IActionResult TestV2()
    {
        return Ok("auth v2");
    }
}