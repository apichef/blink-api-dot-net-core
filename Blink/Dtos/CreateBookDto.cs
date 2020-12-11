using System.ComponentModel.DataAnnotations;

namespace Blink.Dtos
{
    public class CreateBookDto
    {
        [Required]
        public string Name { get; init; }
    }
}