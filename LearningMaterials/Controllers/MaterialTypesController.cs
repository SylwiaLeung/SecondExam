using AutoMapper;
using LearningMaterials.Entities;
using LearningMaterials.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningMaterials.Controllers
{
    [Route("api/materialtypes")]
    [ApiController]
    public class MaterialTypesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMaterialTypeRepository _repository;

        public MaterialTypesController(IMapper mapper, IMaterialTypeRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        //GET api/materialtypes/1
        [HttpGet("{id}", Name = "GetMaterialType")]
        public async Task<ActionResult<MaterialTypeReadDto>> GetMaterialType([FromRoute] int id)
        {
            var materialType = await _repository.GetSingle(id);

            if (materialType is null) return NotFound(new Response { Status = "Not Found", Message = "No such data in the database :(" });

            var materialTypeDto = _mapper.Map<MaterialTypeReadDto>(materialType);

            return Ok(materialTypeDto);
        }

        //GET api/materialtypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialTypeReadDto>>> GetMaterialTypes()
        {
            var materialTypes = await _repository.GetAll();

            if (materialTypes is null) return NotFound(new Response { Status = "Not Found", Message = "No such data in the database :(" });

            var materialTypeDtos = _mapper.Map<List<MaterialTypeReadDto>>(materialTypes);

            return Ok(materialTypeDtos);
        }

        //POST api/materialtypes
        [HttpPost]
        public async Task<ActionResult<MaterialTypeReadDto>> CreateMaterialType([FromBody] MaterialTypeCreateDto createDto)
        {
            var materialTypeModel = _mapper.Map<MaterialType>(createDto);

            await _repository.Create(materialTypeModel);
            await _repository.SaveAsync();

            var materialTypeDto = _mapper.Map<MaterialTypeReadDto>(materialTypeModel);

            return CreatedAtRoute(nameof(GetMaterialType), new { Id = materialTypeDto.Id }, materialTypeDto);
        }
    }
}
