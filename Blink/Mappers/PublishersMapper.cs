using AutoMapper;

namespace Blink.Mappers
{
    public class PublishersMapper : Profile
    {
        public PublishersMapper()
        {
            CreateMap<Dtos.PublisherForCreationDto, Entities.Publisher>();
        }
    }
}