using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Blink.Models
{
    public class Author
    {
        public Author()
        {
            Books = new List<AuthorBook>();
        }
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<AuthorBook> Books { get; set; }
    }
}