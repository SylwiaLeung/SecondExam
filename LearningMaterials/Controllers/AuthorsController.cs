using AutoMapper;
using LearningMaterials.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<AuthorReadDto>> GetAuthor([FromRoute] int id)
        {
            var author = await _repository.GetSingle(id);

            if (author is null) return NotFound();

            var authorDto = _mapper.Map<AuthorReadDto>(author);

            return Ok(authorDto);
        }

        //GET api/authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorReadDto>>> GetAuthors()
        {
            var authors = await _repository.GetAll();

            if (authors is null) return NotFound();

            var authorDtos = _mapper.Map<List<AuthorReadDto>>(authors);

            return Ok(authorDtos);
        }

        ////POST api/authors
        //[HttpPost]
        //public ActionResult<AuthorReadDto> CreateAuthor([FromBody] AuthorCreateDto createDto)
        //{

        //}

        ////PUT api/authors/1
        //[HttpPut("{id}")]
        //public ActionResult<AuthorReadDto> UpdateAuthor([FromRoute] int id, [FromBody] AuthorUpdateDto updateDto)
        //{

        //}

        ////DELETE api/authors/1
        //[HttpDelete("{id}")]
        //public ActionResult DeleteAuthor([FromRoute] int id)
        //{

        //}
    }
}
