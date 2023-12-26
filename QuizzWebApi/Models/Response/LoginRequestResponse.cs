using QuizzWebApi.Models.Identity;

namespace QuizzWebApi.Models.Response;

public class LoginRequestResponse : AuthResult
{
    public string Token { get; set; }

    public string Id { get; set; }
}