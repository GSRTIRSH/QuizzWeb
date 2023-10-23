using System.Security.Cryptography;
using System.Text;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using QuizzWebApi.Configuration.Filters;
using QuizzWebApi.Models;

namespace QuizzWebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    [HttpGet]
    [MapToApiVersion("1.0")]
    //get user
    public IActionResult TestV1()
    {
        return Ok("hashString");
    }

    [HttpGet]
    [MapToApiVersion("2.0")]
    public IActionResult TestV2()
    {
        return Ok("auth v2");
    }
}