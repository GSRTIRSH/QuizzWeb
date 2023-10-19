using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace QuizzWebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController: ControllerBase
{
    public AuthController()
    {
        
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    public int TestV1()
    {
        return 322;
    }
    
    [HttpGet]
    [MapToApiVersion("2.0")]
    public string TestV2()
    {
        return "322";
    }
}