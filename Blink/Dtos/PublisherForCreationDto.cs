using System.ComponentModel.DataAnnotations;

namespace Blink.Dtos
{
    public record PublisherForCreationDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; init; }
    }
}