using System.ComponentModel.DataAnnotations;

namespace Blink.Models
{
    public record BookForCreationDto
    {
        [Required]
        public string Name { get; init; }

        public PublisherForCreationDto Publisher { get; set; }
    }
}