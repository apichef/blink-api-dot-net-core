using System;
using System.Collections.Generic;
using Blink.Entities;

namespace Blink.Repositories
{
    public interface IAuthorRepository
    {
        public void AddAuthor(Author author);
        public void Save();
        public IEnumerable<Author> GetAuthors(IEnumerable<Guid> ids);
    }
}