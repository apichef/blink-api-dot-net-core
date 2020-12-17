using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Blink.Entities;
using Blink.Helpers;
using Blink.Dtos;
using Blink.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blink.Controllers
{
    [ApiController]
    [Route("api/author-collections")]
    public class AuthorCollectionsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        
        public AuthorCollectionsController(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository ??
                throw new ArgumentNullException(nameof(authorRepository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("({ids})", Name = "GetAuthorCollection")]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthorCollection(
            [FromRoute]
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids
        )
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var authors = _authorRepository.GetAuthors(ids);

            if (ids.Count() != authors.Count())
            {
                return NotFound();
            }

            var authorDtos = _mapper.Map<IEnumerable<AuthorDto>>(authors);

            return Ok(authorDtos);
        }

        [HttpPost]
        public ActionResult<AuthorDto> CreateAuthorCollection(IEnumerable<AuthorForCreationDto> authorCollection)
        {
            var authors = _mapper.Map<IEnumerable<Author>>(authorCollection);

            foreach (var author in authors)
            {
                _authorRepository.AddAuthor(author);
            }
            
            _authorRepository.Save();

            var authorDtos = _mapper.Map<IEnumerable<AuthorDto>>(authors);
            var ids = string.Join(",", authorDtos.Select(a => a.Id));

            return CreatedAtRoute("GetAuthorCollection", new { ids = ids }, authorDtos);
        }
    }
}