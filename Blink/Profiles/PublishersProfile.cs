using AutoMapper;

namespace Blink.Profiles
{
    public class PublishersProfile : Profile
    {
        public PublishersProfile()
        {
            CreateMap<Models.PublisherForCreationDto, Entities.Publisher>();
        }
    }
}