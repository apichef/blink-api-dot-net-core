using System;
using System.Collections.Generic;

namespace Blink.Models
{
    public class Publisher
    {
        public Publisher()
        {
            Books = new List<Book>();
        }
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}