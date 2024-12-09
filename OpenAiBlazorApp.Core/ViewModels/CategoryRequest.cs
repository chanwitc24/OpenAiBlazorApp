using System.ComponentModel.DataAnnotations;

namespace OpenAiBlazorApp.Core.ViewModels;
public class CategoryRequest
{
    [Required]
    public required string UserId { get; set; }
    public Dictionary<string, string>? Names { get; set; } // Key: language code, Value: category name
    public string? Type { get; set; } // "inflow" or "outflow"
}
