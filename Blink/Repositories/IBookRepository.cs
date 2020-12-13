using System;
using System.Collections.Generic;
using Blink.Entities;

namespace Blink.Repositories
{
    public interface IBookRepository
    {
        public IEnumerable<Book> GetBooks();
        public IEnumerable<Book> GetBooks(string genre, string publisher);
        public Book GetBook(Guid id);
        public void AddBook(Book book);
        public void UpdateBook(Book book);
        public void DeleteBook(Book book);
        public void Save();
    }
}