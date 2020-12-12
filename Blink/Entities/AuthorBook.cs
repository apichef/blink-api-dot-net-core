using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blink.Entities
{
    public class AuthorBook
    {
        [Key, Column(Order = 0)]
        public Guid AuthorId { get; set; }
        
        [Key, Column(Order = 0)]
        public Guid BookId { get; set; }
        
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
        
        [ForeignKey("BookId")]
        public Book Book { get; set; }
    }
}