using System.ComponentModel.DataAnnotations;

namespace Blink.Models
{
    public abstract record BookForManipulationDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; init; }
    }
}