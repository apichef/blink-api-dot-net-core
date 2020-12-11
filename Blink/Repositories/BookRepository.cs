using System;
using System.Collections.Generic;
using Blink.Dtos;
using Blink.Exceptions;
using Blink.Models;

namespace Blink.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BlinkContext _context;

        public BookRepository(BlinkContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Book> All()
        {
            return _context.Books;
        }

        public Book Find(Guid id)
        {
            return _context.Books.Find(id);
        }

        public Book Create(CreateBookDto dto)
        {
            Book book = new() { Name = dto.Name };
            _context.Books.Add(book);
            _context.SaveChanges();
            
            return book;
        }

        public void Update(Guid id, UpdateBookDto dto)
        {
            var book = Find(id);

            if (book == null)
            {
                throw new ModelNotFoundException();
            }
            
            book.Name = dto.Name;
            
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var book = Find(id);

            if (book == null)
            {
                throw new ModelNotFoundException();
            }
            
            _context.Books.Remove(book);
            
            _context.SaveChanges();
        }
    }
}