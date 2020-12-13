using System;
using System.Collections.Generic;
using System.Linq;
using Blink.DbContexts;
using Blink.Entities;

namespace Blink.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BlinkContext _blinkContext;

        public AuthorRepository(BlinkContext blinkContext)
        {
            _blinkContext = blinkContext ??
                            throw new ArgumentNullException(nameof(blinkContext));
        }
        
        public IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds)
        {
            if (authorIds == null)
            {
                throw new ArgumentNullException(nameof(authorIds));
            }

            return _blinkContext.Authors.Where(author => authorIds.Contains(author.Id))
                .ToList();
        }
        
        public void AddAuthor(Author author)
        {
            _blinkContext.Authors.Add(author);
        }
        
        public void Save()
        {
            _blinkContext.SaveChanges();
        }
    }
}