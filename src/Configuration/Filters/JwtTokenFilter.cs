using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace QuizzWebApi.Configuration.Filters;

public class JwtTokenFilter : IActionFilter
{
    private readonly IConfiguration _configuration;

    public JwtTokenFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeaderValue))
        {
            context.Result = new BadRequestObjectResult("Authorization header is missing");
            return;
        }

        var token = authHeaderValue.ToString().Replace("Bearer ", string.Empty);
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            context.HttpContext.Items["JsonToken"] = jsonToken;
        }
        catch (Exception ex)
        {
            context.Result = new BadRequestObjectResult("Invalid Jwt Token");
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}