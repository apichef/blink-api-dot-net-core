using AutoMapper;

namespace Blink.Profiles
{
    public class AuthorsProfile : Profile
    {
        public AuthorsProfile()
        {
            CreateMap<Models.AuthorForCreationDto, Entities.Author>();
            CreateMap<Entities.Author, Models.AuthorDto>();
        }
    }
}