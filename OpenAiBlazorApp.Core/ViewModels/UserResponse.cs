namespace OpenAiBlazorApp.Core.ViewModels
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public UserResponse(string id, string username, string email)
        {
            Id = id;
            Username = username;
            Email = email;
        }
    }
}

