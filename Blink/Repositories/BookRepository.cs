using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public IEnumerable<Book> GetBooks(string genre, string publisher)
        {
            if (string.IsNullOrWhiteSpace(publisher) && string.IsNullOrWhiteSpace(publisher))
            {
                return GetBooks();
            }

            var books = _blinkContext.Books as IQueryable<Book>;

            if (!string.IsNullOrWhiteSpace(genre))
            {
                books = books.Where(book => book.Genre.Name == genre);
            }
            
            if (!string.IsNullOrWhiteSpace(publisher))
            {
                books = books.Where(book => book.Publisher.Name == publisher);
            }
            
            return books.ToList();
        }

        public Book GetBook(Guid id)
        {
            return _blinkContext.Books.Find(id);
        }

        public void AddBook(Book book)
        {
            _blinkContext.Books.Add(book);
        }

        public void UpdateBook(Book book)
        {
            // this does not need an implementation
        }

        public void DeleteBook(Book book)
        {
            _blinkContext.Books.Remove(book);
        }

        public void Save()
        {
            _blinkContext.SaveChanges();
        }
    }
}