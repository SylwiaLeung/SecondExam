using AutoMapper;
using LearningMaterials.Entities;
using LearningMaterials.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningMaterials.Controllers
{
    [Route("api/authors")]
    [ApiController]
    [Authorize]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuthorReadDto>> GetAuthor([FromRoute] int id)
        {
            var author = await _repository.GetSingle(id);

            if (author is null) return NotFound(new Response { Status = "Not Found", Message = "No such data on the database :(" });

            var authorDto = _mapper.Map<AuthorReadDto>(author);

            return Ok(authorDto);
        }

        //GET api/authors
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<AuthorReadDto>>> GetAuthors()
        {
            var authors = await _repository.GetAll();

            if (authors is null) return NotFound(new Response { Status = "Not Found", Message = "No such data on the database :(" });

            var authorDtos = _mapper.Map<List<AuthorReadDto>>(authors);

            return Ok(authorDtos);
        }

        //POST api/authors
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateAuthor([FromRoute] int id, [FromBody] AuthorUpdateDto updateDto)
        {
            var authorModel = _repository.GetSingle(id).Result;

            if (authorModel is null) return NotFound(new Response { Status = "Not Found", Message = "No such data on the database :(" });

            _mapper.Map(updateDto, authorModel);

            _repository.Update(authorModel);
            await _repository.SaveAsync();

            return Ok(authorModel);
        }

        //DELETE api/authors/1
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAuthor([FromRoute] int id)
        {
            var authorFromDb = _repository.GetSingle(id).Result;

            if (authorFromDb is null) return NotFound(new Response { Status = "Not Found", Message = "No such data on the database :(" });

            _repository.Delete(authorFromDb);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PartialUpdateAuthor(int id, JsonPatchDocument<AuthorUpdateDto> patchDoc)
        {
            var authorToUpdate = await _repository.GetSingle(id);

            if (authorToUpdate is null) return NotFound(new Response { Status = "Not Found", Message = "No such data on the database :(" });

            var authorToPatch = _mapper.Map<AuthorUpdateDto>(authorToUpdate);

            patchDoc.ApplyTo(authorToPatch, ModelState);
            if (!TryValidateModel(authorToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(authorToPatch, authorToUpdate);
            _repository.Update(authorToUpdate);
            await _repository.SaveAsync();

            return Ok(authorToUpdate);
        }
    }
}
