using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizzWebApi.Configuration.Filters;
using QuizzWebApi.Models;
using QuizzWebApi.Repository;

namespace QuizzWebApi.Controllers;

[ApiController]
[QuizExceptionFilter]
[Route("api/[controller]")]
[ServiceFilter(typeof(ApiAuthFilter))]
[ServiceFilter(typeof(JwtTokenFilter))]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserController : ControllerBase
{
    //private readonly UserContext _context;
    private readonly IUserRepository _repository;

    public UserController(IUserRepository repository)
    {
        //_context = context;
        _repository = repository;
    }

    //GET: api/user
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _repository.GetUsersAsync();
    }

    // GET: api/user/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _repository.GetUserAsync(0);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    // POST api/user
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        await _repository.PostUserAsync(user);
        await _repository.SaveAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    // PUT api/user/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutUser(int id, User user)
    {
        /*if (id.ToString() != user.Id)
        {
            return BadRequest();
        }*/

        //_context.Entry(user).State = EntityState.Modified;

        await _repository.UpdateUserAsync(user);
        await _repository.SaveAsync();

        return NoContent();
    }

    // DELETE api/user/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _repository.DeleteUserAsync(id);
        await _repository.SaveAsync();

        return NoContent();
    }
}