using System;

namespace Blink.Models
{
    public record BookDto
    {
        public Guid Id { get; init; }
        
        public string Name { get; init; }
    }
}