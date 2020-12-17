using System;
using System.Net;
using Blink.Entities;
using FluentAssertions;
using Xunit;

namespace Blink.IntegrationTests.Controllers
{
    public class AuthorCollectionsControllerTest : BaseIntegrationTest
    {
        [Fact]
        public async void CreateAuthorCollection_can_create_a_list_of_authors()
        {
            // Arrange
            var author = new Author() { Id = Guid.NewGuid(), Name = "John" };
            await DbContext.Authors.AddAsync(author);
            
            // Act
            var result = await TestClient.GetAsync($"api/author-collections/({author.Id.ToString()})");

            // Assert
            result.StatusCode.Should().Be((int) HttpStatusCode.OK);
        }
    }
}