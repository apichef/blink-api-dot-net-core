using System;

namespace Blink.Models
{
    public record AuthorDto
    {
        public Guid Id { get; init; }
        
        public string Name { get; init; }
    }
}