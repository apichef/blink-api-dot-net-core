using System;
using System.Collections.Generic;

namespace Blink.Models
{
    public class Book
    {
        public Book()
        {
            Authors = new List<AuthorBook>();
        }
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<AuthorBook> Authors { get; set; }
        public Genre Genre { get; set; }
        public Publisher Publisher { get; set; }
    }
}