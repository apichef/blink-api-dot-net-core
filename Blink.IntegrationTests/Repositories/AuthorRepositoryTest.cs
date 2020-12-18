using System;
using System.Collections.Generic;
using System.Linq;
using Blink.DbContexts;
using Blink.Entities;
using Blink.Repositories;
using Blink.TestSeed.Seeders;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Blink.IntegrationTests.Repositories
{
    public class AuthorRepositoryTest
    {
        private readonly AuthorRepository _authorRepository;
        private readonly BlinkContext _dbContext;

        public AuthorRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<BlinkContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _dbContext = new BlinkContext(options);
            _authorRepository = new AuthorRepository(_dbContext);
        }

        [Fact]
        public void test_can_fetch_list_if_authors_by_ids_via_GetAuthors()
        {
            // Arrange
            var authors = CreateAuthors(2);
            
            // Act
            var authorIds = authors.Select(a => a.Id).ToList();
            var result = _authorRepository.GetAuthors(authorIds);

            // Assert
            var idsInResult = result.Select(a => a.Id).ToList();
            idsInResult.Should().HaveCount(2);
            authors.Select(a => a.Id).ToList().Should().Equal(idsInResult);
        }

        [Fact]
        public void test_can_create_an_author_via_AddAuthor()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Bill Jobs";
            var author = new Author() { Id = id, Name = name };
            
            // Act
            _authorRepository.AddAuthor(author);
            _authorRepository.Save();

            // Assert
            var data = _dbContext.Authors.Where(a => a.Id == id && a.Name == name).ToList();
            data.Should().HaveCount(1);
        }

        private List<Author> CreateAuthors(int count)
        {
            return (new AuthorSeeder(_dbContext)).Create(count);
        }
    }
}