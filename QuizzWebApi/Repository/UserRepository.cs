using Microsoft.EntityFrameworkCore;
using QuizzWebApi.Data;
using QuizzWebApi.Models;

namespace QuizzWebApi.Repository;

public class UserRepository : IUserRepository
{
    private readonly UserIdentityContext _context;
    
    public UserRepository(UserIdentityContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetUserAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task PostUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task UpdateUserAsync(User user)
    {
        var userDb = await _context.Users.FindAsync(user.Id);
        if (userDb == null) return;

        userDb.Email = user.Email;
        /*userDb.FirstName = user.FirstName;
        userDb.SubName = user.SubName;*/
        userDb.PasswordHash = user.PasswordHash;
    }

    public async Task DeleteUserAsync(int id)
    {
        var userDb = await _context.Users.FindAsync(id);
        if (userDb == null) return;
        _context.Users.Remove(userDb);
    }

    public async Task SaveAsync() => await _context.SaveChangesAsync();
}