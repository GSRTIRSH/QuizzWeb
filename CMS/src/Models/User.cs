namespace CMS.Models;

public class User
{
    public string Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public bool EmailConfirmed { get; set; }

    public List<string> Roles { get; set; }
}