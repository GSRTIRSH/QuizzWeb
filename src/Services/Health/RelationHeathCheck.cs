using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using QuizzWebApi.Data;

namespace QuizzWebApi.Services.Health;

public class RelationHeathCheck<T> : IHealthCheck where T : DbContext
{
    private readonly T _context;


    public RelationHeathCheck(T context)
    {
        _context = context;
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = new())
    {
        try
        {
            IEnumerable<object> c = _context switch
            {
                QuizContext quizContext => quizContext.Quizzes.ToList(),
                QuizContextV2 quizContextV2 => quizContextV2.QuestionsV2.ToList(),
                UserContext userContext => userContext.Users.ToList(),
                IdentityContext identityContext => identityContext.Users.ToList(),
                _ => throw new NotImplementedException()
            };
            return Task.FromResult(HealthCheckResult.Healthy());
        }
        catch (Exception exception)
        {
            return Task.FromResult(HealthCheckResult.Unhealthy());
        }
    }
}