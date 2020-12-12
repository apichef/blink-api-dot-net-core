using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blink.Entities
{
    public class Publisher
    {
        public Publisher()
        {
            Books = new List<Book>();
        }
        
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public List<Book> Books { get; set; }
    }
}