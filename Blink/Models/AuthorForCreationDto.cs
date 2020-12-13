using System.ComponentModel.DataAnnotations;

namespace Blink.Models
{
    public record AuthorForCreationDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; init; }
    }
}