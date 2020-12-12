using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blink.Entities
{
    public class Author
    {
        public Author()
        {
            Books = new List<AuthorBook>();
        }
        
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public List<AuthorBook> Books { get; set; }
    }
}