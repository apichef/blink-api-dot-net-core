using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Blink.Entities
{
    public class Book
    {
        public Book()
        {
            Authors = new List<AuthorBook>();
        }
        
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public Guid? GenreId { get; set; }
        
        public Guid? PublisherId { get; set; }
        
        public List<AuthorBook> Authors { get; set; }
        
        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }
        
        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }
    }
}