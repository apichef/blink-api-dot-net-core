using System.ComponentModel.DataAnnotations;

namespace Blink.Models
{
    public record AuthorForCreationDto
    {
        [Required]
        public string Name { get; init; }
    }
}