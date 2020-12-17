using AutoMapper;

namespace Blink.Mappers
{
    public class BooksMapper : Profile
    {
        public BooksMapper()
        {
            CreateMap<Entities.Book, Dtos.BookDto>();
            CreateMap<Dtos.BookForCreationDto, Entities.Book>();
            CreateMap<Dtos.BookForUpdateDto, Entities.Book>();
        }
    }
}