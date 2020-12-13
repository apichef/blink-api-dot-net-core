using AutoMapper;

namespace Blink.Profiles
{
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            CreateMap<Entities.Book, Models.BookDto>();
            CreateMap<Models.BookForCreationDto, Entities.Book>();
        }
    }
}