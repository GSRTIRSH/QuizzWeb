namespace QuizzWebApi.Models.Identity;

public class AuthErrorResult : AuthResult
{
    public List<string> Errors { get; set; }
}