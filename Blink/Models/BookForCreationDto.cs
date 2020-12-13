using System.ComponentModel.DataAnnotations;

namespace Blink.Models
{
    public record BookForCreationDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; init; }

        public PublisherForCreationDto Publisher { get; set; }
    }
}