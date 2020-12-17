using System.ComponentModel.DataAnnotations;

namespace Blink.Dtos
{
    public abstract record BookForManipulationDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; init; }
    }
}