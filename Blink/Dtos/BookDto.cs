using System;

namespace Blink.Dtos
{
    public record BookDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}