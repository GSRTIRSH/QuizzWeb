using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuizzWebApi.Configuration.Filters;
using QuizzWebApi.Models;
using QuizzWebApi.Models.Entities;
using QuizzWebApi.Services.Interfaces;

namespace QuizzWebApi.Controllers.v2;

[ApiController]
[ApiVersion("2.0")]
//[QuizExceptionFilter]
[ServiceFilter(typeof(ApiAuthFilter))]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
public class QuizzesController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IQuizService _quizService;

    public QuizzesController(UserManager<User> userManager, IQuizService quizService)
    {
        _quizService = quizService;
        _userManager = userManager;
    }

    /// <summary>
    /// Returns a list of quizzes that satisfy the conditions
    /// </summary>
    /// <remarks>Not require administrator role</remarks>
    /// <param name="pagination"></param>
    /// <param name="category"></param>
    /// <param name="difficulty"></param>
    /// <returns></returns>
    /// <response code="200">quiz</response>
    [HttpGet]
    [Produces("application/json")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public async Task<IEnumerable<QuizV2>> GetQuizzes(
        [FromQuery] PaginationParameters pagination,
        [FromQuery] string? category,
        [FromQuery] string? difficulty)
    {
        var result = await _quizService.GetQuizzes(pagination.PageNumber, pagination.PageSize, category, difficulty);

        var metadata = new
        {
            result.TotalCount,
            result.PageSize,
            result.CurrentPage,
            result.TotalPages,
        };
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

        return result;
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
    public async Task<ActionResult<QuizV2>> GetQuiz(int id)
    {
        var quiz = await _quizService.GetQuiz(id);

        if (quiz == null) return NotFound();

        if (!ValidateAccess(quiz, out var result)) return result;

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
    public async Task<ActionResult> PostQuiz(QuizV2 quizV2)
    {
        if (!ValidateAccess(quizV2, out var result)) return result;
        await _quizService.PostQuiz(quizV2);

        return CreatedAtAction(nameof(PostQuiz), new { id = quizV2.Id }, quizV2);
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
    [Produces("text/plain")]
    [Consumes("application/json")]
    public async Task<IActionResult> PatchQuiz(int id, [FromBody] JsonElement json)
    {
        var quiz = await _quizService.GetQuiz(id);
        if (quiz == null) return NotFound();

        var updateData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json.ToString());
        if (updateData == null) return BadRequest();

        if (!ValidateAccess(quiz, out var result)) return result;

        await _quizService.PatchQuiz(id, updateData);
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
    [Produces("text/plain")]
    [Consumes("application/json")]
    public async Task<IActionResult> DeleteQuiz(int id)
    {
        var quiz = await _quizService.GetQuiz(id);
        if (quiz == null) return NotFound();

        if (!ValidateAccess(quiz, out var result)) return result;
        await _quizService.DeleteQuiz(id);

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
    [Produces("text/plain")]
    [Consumes("application/json")]
    public async Task<IActionResult> PutQuiz(int id, [FromBody] QuizV2 quizV2) => Ok();

    private bool ValidateAccess(QuizV2 quiz, out ActionResult result)
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