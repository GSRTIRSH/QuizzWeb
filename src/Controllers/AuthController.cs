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

    /*
    [HttpGet]
    [Route("swagger")]
    public AuthResult GetResponse()
    {
        return new LoginRequestResponse();
    }
    */

    [HttpGet()]
    [Route("user")]
    public async Task<ActionResult<UserDto>> GetUser([FromQuery] string id)
    {
        if (!Request.Headers.TryGetValue("Authorization", out var authHeaderValue))
        {
            return BadRequest("Authorization header is missing");
        }

        var token = authHeaderValue.ToString().Replace("Bearer ", string.Empty);

        var hu = await _userManager.FindByIdAsync(id);

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken.Subject != hu.Email) return new ForbidResult();

            var username = jsonToken?.Claims?.FirstOrDefault(c => c.Type == "username")?.Value;

            Ok($"Username in token: {username}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to read token: {ex.Message}");
        }

        var u = await _userManager.FindByIdAsync(id);

        var roles = await _userManager.GetRolesAsync(u);

        var userDto = new UserDto()
        {
            Id = u.Id,
            UserName = u.UserName,
            Email = u.Email,
            EmailConfirmed = u.EmailConfirmed,
            Roles = roles.ToList(),
        };

        return userDto;
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
            var roleResult = await _roleManager.CreateAsync(new IdentityRole("User"));
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
                    Token = GenerateJwtToken(newUser)
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
            Token = GenerateJwtToken(exitingUser),
            Id = exitingUser.Id,
            Result = true
        };
    }

    private string GenerateJwtToken(IdentityUser user)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.UTF8.GetBytes(_jwtConfig.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
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