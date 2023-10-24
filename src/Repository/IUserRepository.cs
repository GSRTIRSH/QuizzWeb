using QuizzWebApi.Models;

namespace QuizzWebApi.Repository;

public interface IUserRepository //: IDisposable
{
    Task<List<User>> GetUsersAsync();

    Task<User> GetUserAsync(int id);

    Task PostUserAsync(User user);

    Task UpdateUserAsync(User user);

    Task DeleteUserAsync(int id);

    Task SaveAsync();
}