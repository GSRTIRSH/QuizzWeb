using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace QuizzWebApi.Configuration.Filters;

public class QuizExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        var response = new { error = "Sent invalid request to database" };
        context.Result = new ContentResult()
        {
            Content = response.error,
            StatusCode = StatusCodes.Status500InternalServerError,
        };

        context.ExceptionHandled = true;
    }
}