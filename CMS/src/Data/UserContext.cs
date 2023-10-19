using CMS.Models;

namespace CMS.Data;

public class UserContext
{
    public UserContext()
    {
        Users = new List<User>();
        var client = new HttpClient();

        const string baseUrl = "http://localhost:5200/api/user";

        var users = client.GetFromJsonAsync<List<User>>(baseUrl).Result;

        if (users != null)
        {
            Users = users;
        }
    }
    public List<User>? Users { get; set; }
}