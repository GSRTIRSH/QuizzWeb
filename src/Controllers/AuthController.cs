using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Asp.Versioning;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuizzWebApi.Configuration;
using QuizzWebApi.Models.Identity;

namespace QuizzWebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;

    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    private readonly JwtConfig _jwtConfig;

    public AuthController(ILogger<AuthController> logger,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptionsMonitor<JwtConfig> optionsMonitor)
    {
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtConfig = optionsMonitor.CurrentValue;
    }


    [HttpGet]
    [Route("role")]
    public async Task<List<IdentityUser>> GetUsersWithRole([FromQuery] string role)
    {
        var c = await _userManager.GetUsersInRoleAsync(role);
        return c.ToList();
    }

    [HttpGet]
    [Route("user")]
    public async Task<ActionResult> GetUser([FromQuery] string id, [FromQuery] bool full)
    {
        var u = await _userManager.FindByIdAsync(id);

        var roles = await _userManager.GetRolesAsync(u);

        if (full) return Ok(u);

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

    [HttpGet]
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

    [HttpPost]
    [Route("Register")]
    public async Task<ActionResult<AuthResult>> Register([FromBody] UserRegistrationDto registrationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new RegistrationRequestResponse()
            {
                Result = false,
                Errors = new List<string>() { "Invalid ModelState values" }
            });
        }

        var emailExists = await _userManager.FindByEmailAsync(registrationDto.Email);

        if (emailExists != null)
            return BadRequest(new RegistrationRequestResponse()
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
                return new RegistrationRequestResponse()
                {
                    Result = true,
                    Id = newUser.Id,
                    Token = await GenerateJwtToken(newUser)
                };
            }
        }

        var e1 = isCreated.Errors.Select(e => e.Description);
        var e2 = isRoleAssigned.Errors.Select(e => e.Description);

        var errors = e1.Concat(e2).ToList();

        return BadRequest(new RegistrationRequestResponse()
        {
            Result = false,
            Errors = errors
        });
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<AuthResult>> Login([FromBody] UserLoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("invalid data");

        var exitingUser = await _userManager.FindByNameAsync(loginDto.Name);

        if (exitingUser == null)
            return BadRequest(new RegistrationRequestResponse()
            {
                Result = false,
                Errors = new List<string>() { "Invalid login or password" }
            });

        var isPasswordValid = await _userManager.CheckPasswordAsync(exitingUser, loginDto.Password);

        if (!isPasswordValid)
            return BadRequest(new RegistrationRequestResponse()
            {
                Result = false,
                Errors = new List<string>() { "Invalid login or password" }
            });

        return new LoginRequestResponse()
        {
            Token = await GenerateJwtToken(exitingUser),
            Id = exitingUser.Id,
            Result = true
        };
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
            Expires = DateTime.Now.AddHours(4),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512)
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = jwtTokenHandler.WriteToken(token);
        return jwtToken;
    }
}