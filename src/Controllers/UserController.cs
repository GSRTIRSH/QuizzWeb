using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuizzWebApi.Configuration.Filters;
using QuizzWebApi.Models;
using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using QuizzWebApi.Data;

namespace QuizzWebApi.Controllers;

[ApiController]
[ApiVersion("1.0", Deprecated = true)]
[ApiVersion("2.0")]
[QuizExceptionFilter]
[Route("api/v{version:apiVersion}/[controller]/{id:guid}")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly QuizContextV2 _quizContext;

    public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
        QuizContextV2 quizContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _quizContext = quizContext;
    }

    /// <summary>
    /// Uploads user avatar
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="201"></response>
    /// <response code="400"></response>
    /// <response code="404"></response>
    [HttpPost("avatar")]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult> UploadImage(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null) return NotFound();

        var file = Request?.Form?.Files?[0] ?? null;
        if (file is null) return BadRequest();

        var nameSplit = file.FileName.Split(".");
        var ext = nameSplit[1];
        var guid = Guid.NewGuid().ToString();
        var name = $"{guid}.{ext}";
        var path = Path.Combine("static/images", name);

        user.ImagePath = path;
        await _userManager.UpdateAsync(user);

        await using (var fileStream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return CreatedAtAction(nameof(UploadImage), null);
    }

    [HttpGet("quizzes")]
    public async Task<ActionResult> GetQuizzes(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null) return BadRequest();

        var q = _quizContext.Quizzes.Include(c => c.Questions).AsQueryable();
        var f = q.Where(q => user.MyQuizzes != null && user.MyQuizzes.Single(c => c == q.Id) == q.Id);

        return Ok(await f.ToListAsync());
    }

    /// <summary>
    /// Returns user avatar in png format
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200"></response>
    /// <response code="404"></response>
    [HttpGet("avatar")]
    [Produces("image/png")]
    public async Task<ActionResult> LoadImage(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user is null) return NotFound();
        if (user.ImagePath is null) return NotFound();

        var fileBytes = await System.IO.File.ReadAllBytesAsync(user.ImagePath);
        return File(fileBytes, "image/png");
    }
}