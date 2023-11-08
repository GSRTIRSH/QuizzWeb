namespace QuizzWebApi.Models.Identity;

public class RegistrationRequestResponse : AuthResult
{
    public string Token { get; set; }

    public string Id { get; set; }
}