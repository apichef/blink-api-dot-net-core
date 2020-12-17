using System;
using System.Collections.Generic;
using AutoMapper;
using Blink.Entities;
using Blink.Dtos;
using Blink.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blink.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        
        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository ??
                throw new ArgumentNullException(nameof(bookRepository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }
        
        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<BookDto>> GetBooks(
            [FromQuery(Name = "filter[genre]")] string genre,
            [FromQuery(Name = "filter[publisher]")] string publisher
        )
        {
            var books = _bookRepository.GetBooks(genre, publisher);
            
            return Ok(_mapper.Map<IEnumerable<BookDto>>(books));
        }
        
        [HttpGet("{bookId}", Name = "GetBook")]
        public ActionResult<BookDto> GetBook(Guid bookId)
        {
            var book = _bookRepository.GetBook(bookId);

            if (book == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<BookDto>(book));
        }

        [HttpPost]
        public ActionResult<BookDto> CreateBook(BookForCreationDto bookForCreationDto)
        {
            var book = _mapper.Map<Book>(bookForCreationDto);
            _bookRepository.AddBook(book);
            _bookRepository.Save();

            var bookDto = _mapper.Map<BookDto>(book);

            return CreatedAtRoute("GetBook", new { bookId = book.Id }, bookDto);
        }

        [HttpOptions]
        public ActionResult GetOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");

            return Ok();
        }
        
        [HttpPut("{bookId}")]
        public ActionResult UpdateBook(Guid bookId, BookForUpdateDto bookDto)
        {
            var book = _bookRepository.GetBook(bookId);

            if (book == null)
            {
                return NotFound();
            }

            _mapper.Map(bookDto, book);
            _bookRepository.UpdateBook(book);
            _bookRepository.Save();

            return NoContent();
        }
        
        [HttpDelete("{bookId}")]
        public ActionResult DeleteBook(Guid bookId)
        {
            var book = _bookRepository.GetBook(bookId);

            if (book == null)
            {
                return NotFound();
            }

            _bookRepository.DeleteBook(book);
            _bookRepository.Save();

            return NoContent();
        }
    }
}