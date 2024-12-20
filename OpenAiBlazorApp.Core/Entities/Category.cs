﻿using System.ComponentModel.DataAnnotations;

namespace OpenAiBlazorApp.Core.Entities;
public class Category : BaseEntity
{
    [Required]
    public string UserId { get; private set; }
    public Dictionary<string, string>? Names { get; set; } // Key: language code, Value: category name
    public string? Type { get; set; } // "inflow" or "outflow"
}
