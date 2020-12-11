using Microsoft.EntityFrameworkCore;

namespace Blink.Models
{
    public class BlinkContext : DbContext
    {
        public BlinkContext(DbContextOptions<BlinkContext> options) : base(options) {}
        
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorBook>().HasKey(a => new { a.AuthorId, a.BookId });
        }
    }
}