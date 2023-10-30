using CMS.Models;
using Microsoft.AspNetCore.Authorization;

namespace CMS.Data;

[Authorize]
public class UserContext
{
    public UserContext()
    {
        Users = new List<User>();
        const string baseUrl = "http://localhost:5200/api";
        var clientApi = new ClientApi(baseUrl, new HttpClient());

        var users = clientApi.GetAsync<List<User>>("v1/auth").Result;

        if (users != null)
        {
            Users = users;
        }
    }

    public List<User>? Users { get; set; }
}