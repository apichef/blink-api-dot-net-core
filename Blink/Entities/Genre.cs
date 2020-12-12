using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blink.Entities
{
    public class Genre
    {
        public Genre()
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