namespace OpenAiBlazorApp.Core.ViewModels
{
    public class UserRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserRequest(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
        }
    }
}


