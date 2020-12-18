using Faker;
using System;
using System.Collections.Generic;
using Blink.DbContexts;
using Blink.Entities;

namespace Blink.TestSeed.Seeders
{
    public class AuthorSeeder
    {
        private readonly BlinkContext _blinkContext;

        public AuthorSeeder(BlinkContext blinkContext)
        {
            _blinkContext = blinkContext;
        }
        
        public Author Create()
        {
            var author = new Author() { Id = Guid.NewGuid(), Name = Name.FullName() };
            _blinkContext.Authors.Add(author);
            Save();
            
            return author;
        }
        
        public List<Author> Create(int count)
        {
            var authors = new List<Author>();

            for (int i = 0; i < count; i++)
            {
                authors.Add(new Author() { Id = Guid.NewGuid(), Name = Name.FullName() });
            }

            _blinkContext.Authors.AddRange(authors);
            Save();

            return authors;
        }

        private void Save()
        {
            _blinkContext.SaveChanges();
        }
    }
}