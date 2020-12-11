using System;
using System.Collections.Generic;
using System.Linq;
using Blink.Dtos;
using Blink.Exceptions;
using Blink.Extensions;
using Blink.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blink.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _repository;
        
        public BooksController(IBookRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<BookDto>> GetBooks()
        {
            var books = _repository.All();
            
            return Ok(books.Select(book => book.AsDto()));
        }
        
        [HttpGet("{id}")]
        public ActionResult<BookDto> GetBook(Guid id)
        {
            try
            {
                var book = _repository.Find(id);
                
                return Ok(book.AsDto());
            }
            catch (ModelNotFoundException)
            {
                return NotFound();
            }
        }
        
        [HttpPost]
        public ActionResult<BookDto> PostBook(CreateBookDto bookDto)
        {
            var book = _repository.Create(bookDto);
            
            return CreatedAtAction("GetBook", new { Id = book.Id }, book.AsDto());
        }

        [HttpPut("{id}")]
        public ActionResult PutBook(Guid id, UpdateBookDto bookDto)
        {
            try
            {
                _repository.Update(id, bookDto);
            }
            catch (ModelNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public ActionResult DeleteBook(Guid id)
        {
            try
            {
                _repository.Delete(id);
            }
            catch (ModelNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}