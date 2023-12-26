using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
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
        var action = context.ActionDescriptor as ControllerActionDescriptor;

        var requireApiKeyAttribute = action?.MethodInfo.GetCustomAttributes(typeof(RequireApiKeyAttribute), false);

        RequireApiKeyAttribute? c = null;

        c = requireApiKeyAttribute?.SingleOrDefault() as RequireApiKeyAttribute;

        var isAdmin = c?.IsAdminKey ?? false;

        if (!context.HttpContext.Request.Headers.TryGetValue("X-Api-Key", out var key))
        {
            context.Result = new UnauthorizedObjectResult("Missing API Token");
            return;
        }

        var adminKey = _configuration.GetValue<string>("Authentication:ApiKeyAdmin");
        var apiKey = _configuration.GetValue<string>("Authentication:ApiKey");

        if (isAdmin)
        {
            if (adminKey.Equals(key, StringComparison.Ordinal)) return;
        }
        else
        {
            if (apiKey.Equals(key, StringComparison.Ordinal) || adminKey.Equals(key, StringComparison.Ordinal)) return;
        }

        context.Result = new UnauthorizedObjectResult("Invalid API Token");
    }
}

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public class RequireApiKeyAttribute : Attribute
{
    public bool IsAdminKey { get; }

    public RequireApiKeyAttribute(bool isAdminKey = false)
    {
        IsAdminKey = isAdminKey;
    }
}