using System;
using System.Collections.Generic;
using AutoMapper;
using Blink.Models;
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
        public ActionResult<IEnumerable<BookDto>> GetBooks()
        {
            var books = _bookRepository.GetBooks();
            
            return Ok(_mapper.Map<IEnumerable<BookDto>>(books));
        }
        
        [HttpGet("{bookId}")]
        public ActionResult<BookDto> GetBook(Guid bookId)
        {
            var book = _bookRepository.GetBook(bookId);

            if (book == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<BookDto>(book));
        }
    }
}