namespace OpenAiBlazorApp.WebAPI.Models;
public class Category : BaseEntity
{
    public string? Name { get; set; }
    public string? Type { get; set; } // "inflow" or "outflow"
}
