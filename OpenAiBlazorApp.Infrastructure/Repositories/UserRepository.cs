using MongoDB.Driver;
using OpenAiBlazorApp.Core.Entities;
using OpenAiBlazorApp.Core.Interfaces;
using OpenAiBlazorApp.Infrastructure.Security;

namespace OpenAiBlazorApp.Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;
    private readonly PasswordHasher _passwordHasher;

    public UserRepository(IMongoDatabase database)
    {
        _users = database.GetCollection<User>("Users");
        _passwordHasher = new PasswordHasher();
    }
    public async Task<User> GetByIdAsync(string id)
    {
        return await _users.Find(user => user.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _users.Find(user => true).ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        user.PasswordHash = _passwordHasher.HashPassword(user.PasswordHash);
        await _users.InsertOneAsync(user);
    }

    public async Task UpdateAsync(User user)
    {
        user.PasswordHash = _passwordHasher.HashPassword(user.PasswordHash);
        await _users.ReplaceOneAsync(u => u.Id == user.Id, user);
    }

    public async Task DeleteAsync(string id)
    {
        await _users.DeleteOneAsync(user => user.Id == id);
    }

    public async Task<User> GetUserByUsernameAndPasswordAsync(string username, string password)
    {
        var user = await _users.Find(user => user.Username == username).FirstOrDefaultAsync();
        if (user != null && _passwordHasher.VerifyPassword(password, user.PasswordHash))
        {
            return user;
        }
        return null;
    }

}
