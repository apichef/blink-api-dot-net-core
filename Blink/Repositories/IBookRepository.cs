using System;
using System.Collections.Generic;
using Blink.Dtos;
using Blink.Models;

namespace Blink.Repositories
{
    public interface IBookRepository
    {
        public IEnumerable<Book> All();
        public Book Find(Guid id);
        public Book Create(CreateBookDto dto);
        public void Update(Guid id, UpdateBookDto dto);
        public void Delete(Guid id);
    }
}