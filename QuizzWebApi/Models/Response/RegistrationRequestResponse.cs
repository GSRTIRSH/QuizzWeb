using QuizzWebApi.Models.Identity;

namespace QuizzWebApi.Models.Response;

public class RegistrationRequestResponse : AuthResult
{
    public string Token { get; set; }

    public string Id { get; set; }
}