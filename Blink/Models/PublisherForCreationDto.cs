using System.ComponentModel.DataAnnotations;

namespace Blink.Models
{
    public record PublisherForCreationDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; init; }
    }
}