using System.ComponentModel.DataAnnotations;

namespace OpenAiBlazorApp.Core.Entities;
public class User : BaseEntity
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string PasswordHash { get; set; }

    public string? ParentId { get; set; }
    public Parent? Parent { get; set; }
}
