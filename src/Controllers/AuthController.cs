using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Asp.Versioning;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuizzWebApi.Configuration;
using QuizzWebApi.Configuration.Filters;
using QuizzWebApi.Models;
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

    private readonly JwtConfig _jwtConfig;

    public AuthController(ILogger<AuthController> logger,
        UserManager<IdentityUser> userManager,
        IOptionsMonitor<JwtConfig> optionsMonitor)
    {
        _logger = logger;
        _userManager = userManager;
        _jwtConfig = optionsMonitor.CurrentValue;
    }

    [HttpGet]
    //get user
    public IActionResult TestV1()
    {
        return Ok("hashString");
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationDto registrationDto)
    {
        if (!ModelState.IsValid) return BadRequest("invalid data");

        var emailExists = await _userManager.FindByEmailAsync(registrationDto.Email);

        if (emailExists != null) return BadRequest("email already exists");

        var newUser = new IdentityUser()
        {
            Email = registrationDto.Email,
            UserName = registrationDto.Name,
            //NormalizedUserName = ,
            //NormalizedEmail = ,
        };

        var isCreated = await _userManager.CreateAsync(newUser, registrationDto.Password);

        if (isCreated.Succeeded)
        {
            return Ok(new RegistrationRequestResponse()
            {
                Result = true,
                Token = GenerateJwtToken(newUser)
            });
        }

        return BadRequest("error creating the user");
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
    {
        if (!ModelState.IsValid) return BadRequest("invalid data");

        //var e = _userManager.FindByEmailAsync(loginDto.Email);
        var exitingUser = await _userManager.FindByNameAsync(loginDto.Name);

        if (exitingUser == null) return BadRequest("Invalid login or password");

        var isPasswordValid = await _userManager.CheckPasswordAsync(exitingUser, loginDto.Password);

        if (!isPasswordValid) return BadRequest("Invalid login or password");
        
        return Ok(new LoginRequestResponse()
        {
            Token = GenerateJwtToken(exitingUser),
            Result = true
        });
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