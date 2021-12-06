using AutoMapper;
using LearningMaterials.Entities;
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
        [HttpGet("{id}", Name = "GetAuthor")]
        public async Task<ActionResult<AuthorReadDto>> GetAuthor([FromRoute] int id)
        {
            var author = await _repository.GetSingle(id);

            if (author is null) return NotFound(new Response { Status = "Not Found", Message = "No such data in the database :(" });

            var authorDto = _mapper.Map<AuthorReadDto>(author);

            return Ok(authorDto);
        }

        //GET api/authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorReadDto>>> GetAuthors()
        {
            var authors = await _repository.GetAll();

            if (authors is null) return NotFound(new Response { Status = "Not Found", Message = "No such data in the database :(" });

            var authorDtos = _mapper.Map<List<AuthorReadDto>>(authors);

            return Ok(authorDtos);
        }

        //POST api/authors
        [HttpPost]
        public async Task<ActionResult<AuthorReadDto>> CreateAuthor([FromBody] AuthorCreateDto createDto)
        {
            var authorModel = _mapper.Map<Author>(createDto);

            await _repository.Create(authorModel);
            await _repository.SaveAsync();

            var authorDto = _mapper.Map<AuthorReadDto>(authorModel);

            return CreatedAtRoute(nameof(GetAuthor), new { Id = authorDto.Id }, authorDto);
        }

        //PUT api/authors/1
        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorReadDto>> UpdateAuthor([FromRoute] int id, [FromBody] AuthorUpdateDto updateDto)
        {
            var authorModel = _repository.GetSingle(id).Result;

            if (authorModel is null) return NotFound(new Response { Status = "Not Found", Message = "No such data in the database :(" });

            _mapper.Map(updateDto, authorModel);

            _repository.Update(authorModel);
            await _repository.SaveAsync();

            return NoContent();
        }

        //DELETE api/authors/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor([FromRoute] int id)
        {
            var authorFromDb = _repository.GetSingle(id).Result;

            if (authorFromDb is null) return NotFound(new Response { Status = "Not Found", Message = "No such data in the database :(" });

            _repository.Delete(authorFromDb);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
