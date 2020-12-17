namespace Blink.Dtos
{
    public record BookForCreationDto : BookForManipulationDto
    {
        public PublisherForCreationDto Publisher { get; set; }
    }
}