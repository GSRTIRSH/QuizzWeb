namespace QuizzWebApi.Models.Identity;

public abstract class AuthResult
{
    public string Token { get; set; }

    public string Id { get; set; }

    public bool Result { get; set; }

    public List<string> Errors { get; set; }
}