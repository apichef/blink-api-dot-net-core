using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;
using Blink.Controllers;
using Blink.Models;
using Blink.Entities;
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
            var mockMapper = new Mock<IMapper>();
            var controller = new BooksController(mockRepo.Object, mockMapper.Object);
            
            mockRepo.Setup(repo => repo.GetBooks()).Returns(books);
            
            // Act
            var result = controller.GetBooks(null, null).Result as OkObjectResult;
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
            var mockMapper = new Mock<IMapper>();
            var controller = new BooksController(mockRepo.Object, mockMapper.Object);
            
            mockRepo.Setup(repo => repo.GetBooks()).Returns(emptyBooks);

            // Act
            var result = controller.GetBooks(null, null).Result as OkObjectResult;
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
            var mockMapper = new Mock<IMapper>();
            var controller = new BooksController(mockRepo.Object, mockMapper.Object);
            
            mockRepo.Setup(repo => repo.GetBook(theBook.Id)).Returns(theBook);

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
            var mockMapper = new Mock<IMapper>();
            var controller = new BooksController(mockRepo.Object, mockMapper.Object);
            
            mockRepo.Setup(repo => repo.GetBook(bookId)).Returns((Book) null);

            // Act
            var result = controller.GetBook(bookId).Result;
            
            // Assert
            result.Should().BeOfType<NotFoundResult>()
                .Which.StatusCode.Should().Be((int) HttpStatusCode.NotFound);
        }
    }
}