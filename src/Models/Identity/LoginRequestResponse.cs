using Microsoft.AspNetCore.Mvc;

namespace QuizzWebApi.Models.Identity;

public class LoginRequestResponse : AuthResult
{
    public string Token { get; set; }

    public string Id { get; set; }
}