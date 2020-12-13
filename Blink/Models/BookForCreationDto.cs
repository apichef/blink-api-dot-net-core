using System.ComponentModel.DataAnnotations;

namespace Blink.Models
{
    public record BookForCreationDto : BookForManipulationDto
    {
        public PublisherForCreationDto Publisher { get; set; }
    }
}