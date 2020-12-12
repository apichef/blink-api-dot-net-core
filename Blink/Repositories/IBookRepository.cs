using System;
using System.Collections.Generic;
using Blink.Entities;

namespace Blink.Repositories
{
    public interface IBookRepository
    {
        public IEnumerable<Book> GetBooks();
        public Book GetBook(Guid id);
    }
}