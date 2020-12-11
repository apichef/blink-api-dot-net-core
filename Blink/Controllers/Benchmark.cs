using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Blink.Dtos;
using Blink.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Blink.Controllers
{
    [ApiController]
    [Route("benchmark")]
    public class Benchmark : ControllerBase
    {
        private readonly BlinkContext _context;
        
        public Benchmark(BlinkContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<string> Seed()
        {
            await Clear();
            DateTimeOffset startedAt = DateTimeOffset.Now;
            
            DateTimeOffset pullStartedAt = DateTimeOffset.Now;
            var content = await PullData();
            DateTimeOffset pullFinishedAt = DateTimeOffset.Now;
            
            DateTimeOffset deserializeStartedAt = DateTimeOffset.Now;
            var data = Deserialize(content);
            DateTimeOffset deserializeFinishedAt = DateTimeOffset.Now;
            
            DateTimeOffset dataPersistenceStartedAt = DateTimeOffset.Now;
            Seed(data);
            DateTimeOffset dataPersistenceFinishedAt = DateTimeOffset.Now;
            
            DateTimeOffset finishedAt = DateTimeOffset.Now;
            
            TimeSpan pullDuration =  pullFinishedAt - pullStartedAt;
            TimeSpan deserializeDuration =  deserializeFinishedAt - deserializeStartedAt;
            TimeSpan dataPersistenceDuration =  dataPersistenceFinishedAt - dataPersistenceStartedAt;
            TimeSpan duration =  finishedAt - startedAt;

            return $"pull data from github in {pullDuration.Milliseconds} ms \n " +
                   $"deserialize json in {deserializeDuration.Milliseconds} ms \n " +
                   $"processed and stored data in DB in {dataPersistenceDuration.Milliseconds} ms \n " +
                   $"total duration {duration.Milliseconds} ms";
        }
        
        private async Task Clear()
        {
            _context.Authors.RemoveRange(_context.Authors);
            _context.Books.RemoveRange(_context.Books);
            _context.Genres.RemoveRange(_context.Genres);
            _context.Publishers.RemoveRange(_context.Publishers);
            await _context.SaveChangesAsync();
        }
        
        private async Task<string> PullData()
        {
            var url = "https://raw.githubusercontent.com/apichef/dummy-books-api/main/books.json";
            var client = new HttpClient();
            return await client.GetStringAsync(url);
        }
        
        private List<BookData> Deserialize(string content)
        {
            return JsonConvert.DeserializeObject<List<BookData>>(content);
        }
        
        private void Seed(List<BookData> booksData)
        {
            foreach (BookData data in booksData)
            {
                var book = new Book
                {
                    Id = Guid.NewGuid(),
                    Name = data.Title.Trim(),
                    Genre = GetGener(data),
                    Authors = GetAuthors(data),
                    Publisher = GetPublisher(data)
                };

                _context.Books.Add(book);
                _context.SaveChanges();
            }
        }

        private Publisher GetPublisher(BookData data)
        {
            var publisherName = data.Publisher.Trim();

            if (publisherName.Length == 0)
            {
                return null;
            }
            
            var publisher = _context.Publishers.SingleOrDefault(p => p.Name == data.Publisher.Trim());

            if (publisher == null)
            {
                publisher = new Publisher { Id = Guid.NewGuid(), Name = publisherName };
                _context.Publishers.Add(publisher);
            }

            return publisher;
        }

        private List<AuthorBook> GetAuthors(BookData data)
        {
            string[] authorNames = data.Author.Trim().Split(',');
            var authors = new List<AuthorBook>();

            foreach (string authorName in authorNames)
            {
                var name = authorName.Trim();
                if (name.Length != 0)
                {
                    var author = _context.Authors.SingleOrDefault(a => a.Name == name);

                    if (author == null)
                    {
                        author = new Author() { Id = Guid.NewGuid(), Name = name };
                        _context.Authors.Add(author);
                    }
                    
                    authors.Add(new AuthorBook() { AuthorId = author.Id });
                }
            }

            return authors;
        }

        private Genre GetGener(BookData data)
        {
            var genre = _context.Genres.SingleOrDefault(g => g.Name == data.Genre.Trim());

            if (genre == null)
            {
                genre = new Genre { Id = Guid.NewGuid(), Name = data.Genre.Trim() };
                _context.Genres.Add(genre);
            }

            return genre;
        }
    }
}