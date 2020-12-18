using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Blink.Dtos;
using Blink.Entities;
using Blink.TestSeed.Seeders;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace Blink.AcceptanceTests.Controllers
{
    public class AuthorCollectionsControllerTest : BaseIntegrationTest
    {
        [Fact]
        public async void can_fetch_a_list_of_authors_by_ids()
        {
            // Arrange
            var authors = CreateAuthor(2);

            // Act
            var authorIds = String.Join(",", authors.Select(a => a.Id).ToList());
            var response = await TestClient.GetAsync($"api/author-collections/({authorIds})");
            var data = await response.Content.ReadAsAsync<List<AuthorDto>>();

            // Assert
            response.StatusCode.Should().Be((int) HttpStatusCode.OK);
            data.Should().HaveCount(2);
            data.First().Id.Should().Be(authors.First().Id);
            data.First().Name.Should().Be(authors.First().Name);
        }
        
        [Fact]
        public async void can_create_a_list_of_authors()
        {
            // Arrange
            var authorsToBeCreated = new List<AuthorDto>
            {
                new AuthorDto() {Name = "Bill"},
                new AuthorDto() {Name = "Steve"},
            };

            var serializedAuthors = JsonConvert.SerializeObject(authorsToBeCreated, Formatting.Indented);
            HttpContent content = new StringContent(serializedAuthors);

            // Act
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var response = await TestClient.PostAsync("api/author-collections", content); 
            var data = await response.Content.ReadAsAsync<List<AuthorDto>>();

            // Assert
            response.StatusCode.Should().Be((int) HttpStatusCode.Created);
            
            data.Should().HaveCount(2);
            data.First().Name.Should().Be(authorsToBeCreated.First().Name);
            
            var ids = String.Join(",", data.Select(a => a.Id).ToList());
            response.Headers.Location.Should().Be($"http://localhost/api/author-collections/({ids})");
        }

        private List<Author> CreateAuthor(int count)
        {
            return (new AuthorSeeder(DbContext)).Create(count);
        }
    }
}