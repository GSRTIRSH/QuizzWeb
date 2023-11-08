using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizzWebApi.Configuration.Filters;
using QuizzWebApi.Models;
using QuizzWebApi.Data;

namespace QuizzWebApi.Controllers.v2;

[ApiController]
[ApiVersion("2.0")]
[QuizExceptionFilter]
[ServiceFilter(typeof(ApiAuthFilter))]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
public class QuizzesController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly QuizContextV2 _context;
    private readonly UserManager<IdentityUser> _userManager;

    public QuizzesController(QuizContextV2 context, UserManager<IdentityUser> userManager, ILogger logger)
    {
        _context = context;
        _userManager = userManager;
        _logger = logger;
    }

    /// <summary>
    /// Returns a list of quizzes that satisfy the conditions
    /// </summary>
    /// <param name="limit">count of returned quizzes</param>
    /// <param name="category"></param>
    /// <param name="difficulty"></param>
    /// <returns></returns>
    /// <response code="200">quiz</response>
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Produces("application/json")]
    public async Task<IEnumerable<QuizV2>> GetQuizzes(
        [FromQuery] int? limit,
        [FromQuery] string? category,
        [FromQuery] string? difficulty)
    {
        var query = _context.Quizzes.Include(q => q.Questions).AsQueryable();

        if (!string.IsNullOrEmpty(category))
        {
            query = query.Where(q => q.Category.ToLower().Equals(category.ToLower()));
        }

        if (!string.IsNullOrEmpty(difficulty))
        {
            query = query.Where(q => q.Difficulty.ToLower().Equals(difficulty.ToLower()));
        }

        if (limit != null)
        {
            return await query.Take((int)limit).ToListAsync();
        }

        return await query.ToListAsync();
    }

    /// <summary>
    /// Find quiz by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">Returns quiz</response>
    /// <response code="404">Specified quiz doesn't exists</response>
    /// <response code="401">User unauthorized</response>
    /// <response code="403">User haven't access</response>
    [HttpGet("{id:int}")]
    [TypeFilter(typeof(JwtTokenFilter))]
    [Produces("application/json")]
    [ProducesResponseType(typeof(QuizV2), 200)]
    [ProducesResponseType(typeof(NotFoundResult), 404)]
    [ProducesResponseType(typeof(UnauthorizedResult), 401)]
    [ProducesResponseType(typeof(ForbidResult), 403)]
    public async Task<ActionResult<QuizV2>> GetQuiz(int id)
    {
        var quiz = await _context.Quizzes
            .Where(q => q.Id == id)
            .Include(q => q.Questions)
            .SingleOrDefaultAsync();

        if (quiz == null) return NotFound();

        if (!ValidateToken(quiz, out var result)) return result;

        return quiz;
    }

    /// <summary>
    /// Create a new quiz
    /// </summary>
    /// <param name="quizV2"></param>
    /// <returns></returns>
    /// <response code="201">Quiz created</response>
    /// <response code="400">Request has incorrect values</response>
    /// <response code="401">User unauthorized</response>
    /// <response code="403">User haven't access</response>
    [HttpPost]
    [TypeFilter(typeof(JwtTokenFilter))]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(QuizV2), 201)]
    [ProducesResponseType(typeof(BadRequestResult), 400)]
    [ProducesResponseType(typeof(UnauthorizedResult), 401)]
    [ProducesResponseType(typeof(ForbidResult), 403)]
    public async Task<ActionResult> PostQuiz(QuizV2 quizV2)
    {
        if (!ValidateToken(quizV2, out var result)) return result;

        _context.Quizzes.Add(quizV2);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetQuiz), new { id = quizV2.Id }, quizV2);
    }

    /// <summary>
    /// Partly updates a existing quiz
    /// </summary>
    /// <param name="id"></param>
    /// <param name="json"></param>
    /// <returns></returns>
    /// <response code="200">Quiz updated</response>
    /// <response code="400">Request has incorrect values</response>
    /// <response code="401">User unauthorized</response>
    /// <response code="403">User haven't access</response>
    /// <response code="404">Specified quiz doesn't exists</response>
    [HttpPatch("{id:int}")]
    [TypeFilter(typeof(JwtTokenFilter))]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(OkResult), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 400)]
    [ProducesResponseType(typeof(UnauthorizedResult), 401)]
    [ProducesResponseType(typeof(ForbidResult), 403)]
    [ProducesResponseType(typeof(NotFoundResult), 404)]
    public async Task<IActionResult> PatchQuiz(int id, [FromBody] JsonElement json)
    {
        var quiz = await _context.Quizzes.FindAsync(id);
        if (quiz == null) return NotFound();

        var updateData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json.ToString());
        if (updateData == null) return BadRequest();

        if (!ValidateToken(quiz, out var result)) return result;

        if (updateData.ContainsKey("title"))
        {
            quiz.Title = updateData["title"].GetString();
        }

        if (updateData.ContainsKey("imagePath"))
        {
            quiz.ImagePath = updateData["imagePath"].GetString();
        }

        if (updateData.ContainsKey("category"))
        {
            quiz.Category = updateData["category"].GetString();
        }

        if (updateData.ContainsKey("difficulty"))
        {
            quiz.Difficulty = updateData["difficulty"].GetString();
        }

        if (updateData.ContainsKey("author"))
        {
            quiz.Author = updateData["author"].GetString();
        }

        await _context.SaveChangesAsync();
        return Ok();
    }

    /// <summary>
    /// Deletes a quiz
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">Quiz deleted</response>
    /// <response code="400">Request has incorrect values</response>
    /// <response code="401">User unauthorized</response>
    /// <response code="403">User haven't access</response>
    /// <response code="404">Specified quiz doesn't exists</response>
    [HttpDelete("{id:int}")]
    [TypeFilter(typeof(JwtTokenFilter))]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(OkResult), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 400)]
    [ProducesResponseType(typeof(UnauthorizedResult), 401)]
    [ProducesResponseType(typeof(ForbidResult), 403)]
    [ProducesResponseType(typeof(NotFoundResult), 404)]
    public async Task<IActionResult> DeleteQuiz(int id)
    {
        var quiz = await _context.Quizzes.FindAsync(id);
        if (quiz == null) return NotFound();

        if (!ValidateToken(quiz, out var result)) return result;

        _context.Quizzes.Remove(quiz);
        await _context.SaveChangesAsync();

        return Ok();
    }

    /// <summary>
    /// Update an existing quiz
    /// </summary>
    /// <remarks>Currently not supported yet</remarks>
    /// <param name="id"></param>
    /// <param name="quizV2"></param>
    /// <returns></returns>
    /// <response code="200">Quiz updated</response>
    /// <response code="400">Request has incorrect values</response>
    /// <response code="401">User unauthorized</response>
    /// <response code="403">User haven't access</response>
    /// <response code="404">Specified quiz doesn't exists</response>
    [HttpPut("{id:int}")]
    [TypeFilter(typeof(JwtTokenFilter))]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(OkResult), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 400)]
    [ProducesResponseType(typeof(UnauthorizedResult), 401)]
    [ProducesResponseType(typeof(ForbidResult), 403)]
    [ProducesResponseType(typeof(NotFoundResult), 404)]
    public async Task<IActionResult> PutQuiz(int id, [FromBody] QuizV2 quizV2) => Ok();

    private bool ValidateToken(QuizV2 quiz, out ActionResult result)
    {
        result = Ok();

        if (!HttpContext.Items.TryGetValue("JsonToken", out var jsonToken))
        {
            result = Unauthorized();
            return false;
        }

        if (jsonToken is not JwtSecurityToken token)
        {
            result = Unauthorized();
            return false;
        }

        var id = token.Claims.First(claim => claim.Type == "id").Value;
        var role = token.Claims.First(claim => claim.Type == "role").Value;

        if (role == "Admin") return true;

        var user = _userManager.FindByIdAsync(id).Result;
        if (user.Id == quiz.Author) return true;

        result = Forbid();
        return false;
    }
}