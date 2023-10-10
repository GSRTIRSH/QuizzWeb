namespace CMS.Models;

public class User
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string SubName { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string Token { get; set; }

    public int[]? MyQuizzes { get; set; }
}