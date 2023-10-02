using Microsoft.AspNetCore.Mvc;
using QuizzWebApi.Models;


namespace QuizzWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController
{
        
    [HttpGet(Name = "Test")]
    public string Get()
    {
        return "test";
    }
}