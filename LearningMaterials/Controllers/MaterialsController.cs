using AutoMapper;
using LearningMaterials.Entities;
using LearningMaterials.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningMaterials.Controllers
{
    [Route("api/materials")]
    [ApiController]
    [Authorize]
    public class MaterialsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMaterialRepository _repository;

        public MaterialsController(IMapper mapper, IMaterialRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        //GET api/materials/1
        [HttpGet("{id}", Name = "GetMaterial")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MaterialReadDto>> GetMaterial([FromRoute] int id)
        {
            var material = await _repository.GetSingle(id);

            if (material is null) return NotFound(new Response { Status = "Not Found", Message = "No such data in the database :(" });

            var materialDto = _mapper.Map<MaterialReadDto>(material);

            return Ok(materialDto);
        }

        //GET api/materials
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<MaterialReadDto>>> GetMaterials([FromQuery] MaterialsQueryModel query)
        {
            var materials = await _repository.GetAll();

            if (materials is null) return NotFound(new Response { Status = "Not Found", Message = "No such data in the database :(" });

            var materialDtos = _mapper.Map<List<MaterialReadDto>>(materials);

            var baseQuery = materialDtos
                .Where(m => query.SearchPhrase == null
                || m.MaterialTypeName.ToLower().Contains(query.SearchPhrase.ToLower()));

            if (query.SortByDate == true)
            {
                return Ok(baseQuery.OrderByDescending(m => m.PublishDate));
            }

            return Ok(baseQuery);
        }

        //POST api/materials
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MaterialReadDto>> CreateMaterial([FromBody] MaterialCreateDto createDto)
        {
            var materialModel = _mapper.Map<Material>(createDto);

            await _repository.Create(materialModel);
            await _repository.SaveAsync();

            var materialDto = _mapper.Map<MaterialReadDto>(materialModel);

            return CreatedAtRoute(nameof(GetMaterial), new { Id = materialDto.Id }, materialDto);
        }

        //DELETE api/materials/1
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteMaterial([FromRoute] int id)
        {
            var materialFromDb = _repository.GetSingle(id).Result;

            if (materialFromDb is null) return NotFound(new Response { Status = "Not Found", Message = "No such data in the database :(" });

            _repository.Delete(materialFromDb);
            await _repository.SaveAsync();

            return NoContent();
        }

        //PUT api/materials/1
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateMaterial([FromRoute] int id, [FromBody] MaterialUpdateDto updateDto)
        {
            var materialModel = _repository.GetSingle(id).Result;

            if (materialModel is null) return NotFound(new Response { Status = "Not Found", Message = "No such data in the database :(" });

            _mapper.Map(updateDto, materialModel);

            _repository.Update(materialModel);
            await _repository.SaveAsync();

            return Ok(materialModel);
        }
    }
}
