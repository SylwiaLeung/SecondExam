using AutoMapper;
using LearningMaterials.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LearningMaterials.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _repository;

        public AuthorsController(IMapper mapper, IAuthorRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        //GET api/authors/1
        [HttpGet("{id}")]
        public ActionResult<AuthorReadDto> GetAuthor([FromRoute] int id)
        {

        }

        //GET api/authors
        [HttpGet]
        public ActionResult<IEnumerable<AuthorReadDto>> GetAuthors()
        {
            var authors = _repository.GetAll();

            if (authors is null) return NotFound();

            var authorDtos = _mapper.Map<List<AuthorReadDto>>(authors);

            return Ok(authorDtos);
        }

        //POST api/authors
        [HttpPost]
        public ActionResult<AuthorReadDto> CreateAuthor([FromBody] AuthorCreateDto createDto)
        {

        }

        //PUT api/authors/1
        [HttpPut("{id}")]
        public ActionResult<AuthorReadDto> UpdateAuthor([FromRoute] int id, [FromBody] AuthorUpdateDto updateDto)
        {

        }

        //DELETE api/authors/1
        [HttpDelete("{id}")]
        public ActionResult DeleteAuthor([FromRoute] int id)
        {

        }
    }
}
