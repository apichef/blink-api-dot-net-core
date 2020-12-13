using System.ComponentModel.DataAnnotations;

namespace Blink.Models
{
    public record PublisherForCreationDto
    {
        [Required]
        public string Name { get; init; }
    }
}