using OpenAiBlazorApp.Core.Entities;

namespace OpenAiBlazorApp.Core.Interfaces;
public interface IUserRepository
{
    Task<User> GetByIdAsync(string id);
    Task<List<User>> GetAllAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(string id);
    Task<User> GetUserByUsernameAndPasswordAsync(string username, string password);
}
