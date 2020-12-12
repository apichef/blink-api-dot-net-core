using System;
using System.Collections.Generic;
using Blink.DbContexts;
using Blink.Entities;

namespace Blink.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BlinkContext _blinkContext;

        public BookRepository(BlinkContext blinkContext)
        {
            _blinkContext = blinkContext ??
                throw new ArgumentNullException(nameof(blinkContext));
        }
        
        public IEnumerable<Book> GetBooks()
        {
            return _blinkContext.Books;
        }

        public Book GetBook(Guid id)
        {
            return _blinkContext.Books.Find(id);
        }

        public void Create(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            
            _blinkContext.Books.Add(book);
            _blinkContext.SaveChanges();
        }

        public void Update(Book book)
        {
            _blinkContext.SaveChanges();
        }

        public void Delete(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            
            _blinkContext.Books.Remove(book);
            _blinkContext.SaveChanges();
        }
    }
}