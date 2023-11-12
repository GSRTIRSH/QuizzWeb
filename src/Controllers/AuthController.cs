using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuizzWebApi.Configuration;
using QuizzWebApi.Models.Identity;

namespace QuizzWebApi.Controllers;

[ApiController]
[ApiVersion("1.0", Deprecated = true)]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    //private readonly ILogger<AuthController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JwtConfig _jwtConfig;

    public AuthController(ILogger<AuthController> logger,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptionsMonitor<JwtConfig> optionsMonitor)
    {
        //_logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtConfig = optionsMonitor.CurrentValue;
    }

    /// <summary>
    /// Finds users who belongs to the role
    /// </summary>
    /// <param name="role">role name</param>
    /// <returns></returns>
    [HttpGet]
    [Route("role")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public async Task<List<IdentityUser>> GetUsersWithRole([FromQuery] string role)
    {
        var c = await _userManager.GetUsersInRoleAsync(role);
        return c.ToList();
    }

    /// <summary>
    /// Returns user data
    /// </summary>
    /// <param name="id">user id</param>
    /// <param name="full">model view type is full</param>
    /// <returns></returns>
    /// <response code="200">Returns full user data</response>
    /// <response code="200">Returns short user data</response>
    /// <response code="401">User unauthorized</response>
    /// <response code="403">User haven't access</response>
    /// <response code="404">User with specified Id not exists</response> 
    [HttpGet]
    [Route("user")]
    [ProducesResponseType(typeof(UserDto), 200)]
    [ProducesResponseType(typeof(IdentityUser), 200)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public async Task<ActionResult> GetUser([FromQuery] string id, [FromQuery] bool full)
    {
        var u = await _userManager.FindByIdAsync(id);
        if (u is null) return NotFound();
        if (full) return Ok(u);

        var roles = await _userManager.GetRolesAsync(u);
        var userDto = new UserDto()
        {
            Id = u.Id,
            UserName = u.UserName,
            Email = u.Email,
            EmailConfirmed = u.EmailConfirmed,
            Roles = roles.ToList(),
        };

        return Ok(userDto);
    }

    /// <summary>
    /// Returns a list of all users
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public async Task<List<UserDto>> Get()
    {
        var users = await _userManager.Users.ToListAsync();
        var usersDto = new List<UserDto>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                Roles = roles.ToList()
            };
            usersDto.Add(userDto);
        }

        return usersDto;
    }

    /// <summary>
    /// Check the validity of the token
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Valid token</response>
    /// <response code="401">Unauthorized</response>
    [HttpGet]
    [Authorize]
    [Route("token")]
    public OkResult IsLogin()
    {
        return Ok();
    }

    /// <summary>
    /// Register a new user
    /// </summary>
    /// <param name="registrationDto">register data model</param>
    /// <returns>ActionResult</returns>
    /// <response code="201">User has created</response>
    /// <response code="400">Request has incorrect values</response>
    [HttpPost]
    [Route("register")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(RegistrationRequestResponse), 201)]
    [ProducesResponseType(typeof(AuthErrorResult), 400)]
    public async Task<ActionResult<AuthResult>> Register([FromBody] UserRegistrationDto registrationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new AuthErrorResult()
            {
                Result = false,
                Errors = new List<string>() { "Invalid ModelState values" }
            });
        }

        var emailExists = await _userManager.FindByEmailAsync(registrationDto.Email);
        if (emailExists != null)
            return BadRequest(new AuthErrorResult()
            {
                Result = false,
                Errors = new List<string>() { "email already exists" }
            });

        var newUser = new IdentityUser()
        {
            Email = registrationDto.Email,
            UserName = registrationDto.Name,
        };

        var roleExist = await _roleManager.RoleExistsAsync("User");
        if (!roleExist)
        {
            await _roleManager.CreateAsync(new IdentityRole("User"));
        }

        var isCreated = await _userManager.CreateAsync(newUser, registrationDto.Password);
        var isRoleAssigned = IdentityResult.Failed();

        if (isCreated.Succeeded)
        {
            isRoleAssigned = await _userManager.AddToRoleAsync(newUser, "User");

            if (isRoleAssigned.Succeeded)
            {
                return CreatedAtAction(nameof(Register), new RegistrationRequestResponse()
                {
                    Result = true,
                    Id = newUser.Id,
                    Token = await GenerateJwtToken(newUser)
                });
            }
        }

        var e1 = isCreated.Errors.Select(e => e.Description);
        var e2 = isRoleAssigned.Errors.Select(e => e.Description);

        var errors = e1.Concat(e2).ToList();

        return BadRequest(new AuthErrorResult()
        {
            Result = false,
            Errors = errors
        });
    }

    /// <summary>
    /// Login a exited user
    /// </summary>
    /// <param name="loginDto">login data model</param>
    /// <returns>ActionResult</returns>
    /// <response code="200">User has login</response>
    /// <response code="401">Request has incorrect values</response>
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(typeof(RegistrationRequestResponse), 200)]
    [ProducesResponseType(typeof(AuthErrorResult), 401)]
    public async Task<ActionResult<AuthResult>> Login([FromBody] UserLoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("invalid data");

        var exitingUser = await _userManager.FindByNameAsync(loginDto.Name);

        if (exitingUser == null)
            return Unauthorized(new AuthErrorResult()
            {
                Result = false,
                Errors = new List<string>() { "Invalid login or password" }
            });

        var isPasswordValid = await _userManager.CheckPasswordAsync(exitingUser, loginDto.Password);

        if (!isPasswordValid)
            return Unauthorized(new AuthErrorResult()
            {
                Result = false,
                Errors = new List<string>() { "Invalid login or password" }
            });

        return Ok(new LoginRequestResponse()
        {
            Token = await GenerateJwtToken(exitingUser),
            Id = exitingUser.Id,
            Result = true
        });
    }

    private async Task<string> GenerateJwtToken(IdentityUser user)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtConfig.Secret);
        var c = await _userManager.GetRolesAsync(user);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("role", c?.First() ?? "User")
            }),
            Issuer = _jwtConfig.Issuer,
            Audience = _jwtConfig.Audience,
            Expires = DateTime.Now.AddMinutes(15),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512)
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = jwtTokenHandler.WriteToken(token);
        return jwtToken;
    }
}