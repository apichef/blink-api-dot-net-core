using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Blink.Controllers;
using Blink.Dtos;
using Blink.Exceptions;
using Blink.Models;
using Blink.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Blink.IntegrationTests.Controllers
{
    public class BooksControllerTest
    {
        [Fact]
        public void GetBooks_can_fetch_books()
        {
            // Arrange
            var theBook = new Book() { Id = Guid.NewGuid(), Name = "The Book" };
            var anotherBook = new Book() { Id = Guid.NewGuid(), Name = "Another Book" };
            List<Book> books = new() { theBook, anotherBook };
            
            var mockRepo = new Mock<IBookRepository>();
            mockRepo.Setup(repo => repo.All()).Returns(books);
            
            var controller = new BooksController(mockRepo.Object);

            // Act
            var result = controller.GetBooks().Result as OkObjectResult;
            var data = result.Value as IEnumerable<BookDto>;

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int) HttpStatusCode.OK);

            data.Should().HaveCount(2);
            data.First(book => book.Id == theBook.Id).Should().NotBeNull();
            data.First(book => book.Id == anotherBook.Id).Should().NotBeNull();
        }
        
        [Fact]
        public void GetBooks_returns_empty_result_when_there_are_no_books()
        {
            // Arrange
            var emptyBooks = new List<Book>();
            
            var mockRepo = new Mock<IBookRepository>();
            mockRepo.Setup(repo => repo.All()).Returns(emptyBooks);
            var controller = new BooksController(mockRepo.Object);

            // Act
            var result = controller.GetBooks().Result as OkObjectResult;
            var data = result.Value as IEnumerable<BookDto>;
            
            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int) HttpStatusCode.OK);
            
            data.Should().BeEmpty();
        }
        
        [Fact]
        public void GetBook_can_fetch_book_by_id()
        {
            // Arrange
            var theBook = new Book() { Id = Guid.NewGuid(), Name = "The Book" };
            
            var mockRepo = new Mock<IBookRepository>();
            mockRepo.Setup(repo => repo.Find(theBook.Id)).Returns(theBook);
            var controller = new BooksController(mockRepo.Object);

            // Act
            var result = controller.GetBook(theBook.Id).Result as OkObjectResult;
            var data = result.Value as BookDto;
            
            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int) HttpStatusCode.OK);
            
            data.Name.Should().Be(theBook.Name);
        }
        
        [Fact]
        public void GetBook_returns_not_found_when_there_is_no_book_for_the_id()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            
            var mockRepo = new Mock<IBookRepository>();
            mockRepo.Setup(repo => repo.Find(bookId)).Throws(new ModelNotFoundException());
            var controller = new BooksController(mockRepo.Object);

            // Act
            var result = controller.GetBook(bookId).Result;
            
            // Assert
            result.Should().BeOfType<NotFoundResult>()
                .Which.StatusCode.Should().Be((int) HttpStatusCode.NotFound);
        }
        
        [Fact]
        public void PutBook_can_update_a_book()
        {
            // Arrange
            UpdateBookDto dto = new() { Name = "new name" };
            Book book = new() { Id = Guid.NewGuid(), Name = "old name" };
            
            var mockRepo = new Mock<IBookRepository>();
            mockRepo.Setup(repo => repo.Update(book.Id, dto));
            
            var controller = new BooksController(mockRepo.Object);

            // Act
            var result = controller.PutBook(book.Id, dto);

            // Assert
            result.Should().BeOfType<NoContentResult>()
                .Which.StatusCode.Should().Be((int) HttpStatusCode.NoContent);
        }
        
        [Fact]
        public void PutBook_returns_not_found_when_there_is_no_book_for_the_id()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            UpdateBookDto dto = new() { Name = "new name" };
            
            var mockRepo = new Mock<IBookRepository>();
            mockRepo.Setup(repo => repo.Update(bookId, dto)).Throws(new ModelNotFoundException());
            
            var controller = new BooksController(mockRepo.Object);

            // Act
            var result = controller.PutBook(bookId, dto);

            // Assert
            result.Should().BeOfType<NotFoundResult>()
                .Which.StatusCode.Should().Be((int) HttpStatusCode.NotFound);
        }
        
        [Fact]
        public void DeleteBook_can_delete_a_book()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            
            var mockRepo = new Mock<IBookRepository>();
            mockRepo.Setup(repo => repo.Delete(bookId));
            
            var controller = new BooksController(mockRepo.Object);

            // Act
            var result = controller.DeleteBook(bookId);

            // Assert
            result.Should().BeOfType<NoContentResult>()
                .Which.StatusCode.Should().Be((int) HttpStatusCode.NoContent);
        }
        
        [Fact]
        public void DeleteBook_returns_not_found_when_there_is_no_book_for_the_id()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            
            var mockRepo = new Mock<IBookRepository>();
            mockRepo.Setup(repo => repo.Delete(bookId)).Throws(new ModelNotFoundException());
            
            var controller = new BooksController(mockRepo.Object);

            // Act
            var result = controller.DeleteBook(bookId);

            // Assert
            result.Should().BeOfType<NotFoundResult>()
                .Which.StatusCode.Should().Be((int) HttpStatusCode.NotFound);
        }
    }
}