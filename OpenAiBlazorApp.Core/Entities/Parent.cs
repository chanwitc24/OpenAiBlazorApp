using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpenAiBlazorApp.Core.Entities
{
    public class Parent : BaseEntity
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        public List<User> Users { get; set; } = new List<User>();
    }
}
