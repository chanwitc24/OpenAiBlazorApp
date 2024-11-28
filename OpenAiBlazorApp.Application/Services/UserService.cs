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
}
