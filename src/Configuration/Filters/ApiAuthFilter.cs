using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace QuizzWebApi.Configuration.Filters;

public class ApiAuthFilter : IAuthorizationFilter
{
    private readonly IConfiguration _configuration;

    public ApiAuthFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("X-Api-Key", out var key))
        {
            context.Result = new UnauthorizedObjectResult("Missing API Token");
            return;
        }

        var apiKey = _configuration.GetValue<string>("Authentication:ApiKey");
        if (!apiKey.Equals(key))
        {
            context.Result = new UnauthorizedObjectResult("Invalid API Token");
        }
    }
}