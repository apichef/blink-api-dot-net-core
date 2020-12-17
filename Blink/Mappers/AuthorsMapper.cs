using AutoMapper;

namespace Blink.Mappers
{
    public class AuthorsMapper : Profile
    {
        public AuthorsMapper()
        {
            CreateMap<Dtos.AuthorForCreationDto, Entities.Author>();
            CreateMap<Entities.Author, Dtos.AuthorDto>();
        }
    }
}