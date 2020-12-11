using System.ComponentModel.DataAnnotations;

namespace Blink.Dtos
{
    public class UpdateBookDto
    {
        [Required]
        public string Name { get; init; }
    }
}