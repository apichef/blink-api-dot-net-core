using Blink.Dtos;
using Blink.Models;

namespace Blink.Extensions
{
    public static class BookExtension
    {
        public static BookDto AsDto(this Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Name = book.Name
            };
        }
    }
}