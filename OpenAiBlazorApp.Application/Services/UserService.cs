using OpenAiBlazorApp.Core.Entities;
using OpenAiBlazorApp.Core.Interfaces;

namespace OpenAiBlazorApp.Application.Services;
public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<User> AuthenticateAsync(string username, string password)
    {
        // Add your authentication logic here
        var user = await _userRepository.GetUserByUsernameAndPasswordAsync(username, password);
        return user;
    }
    public async Task<string> GenerateKey()
    {
        return await _userRepository.GenerateKey();
    }

    public async Task<User> GetUserByIdAsync(string id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task AddUserAsync(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (string.IsNullOrEmpty(user.Username)) throw new ArgumentException("Username cannot be null or empty", nameof(user.Username));
        if (string.IsNullOrEmpty(user.Email)) throw new ArgumentException("Email cannot be null or empty", nameof(user.Email));
        if (string.IsNullOrEmpty(user.PasswordHash)) throw new ArgumentException("Password cannot be null or empty", nameof(user.PasswordHash));

        await _userRepository.AddAsync(user);
    }

    public async Task UpdateUserAsync(User user)
    {
        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(string id)
    {
        await _userRepository.DeleteAsync(id);
    }
    public async Task<List<User>> GetUsersByParentIdAsync(string parentId)
    {
        return await _userRepository.GetUsersByParentIdAsync(parentId);
    }
}
