using System;

namespace Blink.Dtos
{
    public record AuthorDto
    {
        public Guid Id { get; init; }
        
        public string Name { get; init; }
    }
}