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
        public async Task<ActionResult<AuthorReadDto>> GetMaterialType([FromRoute] int id)
        {
            var materialType = await _repository.GetSingle(id);

            if (materialType is null) return NotFound(new Response { Status = "Not Found", Message = "No such data in the database :(" });

            var materialTypeDto = _mapper.Map<materialTypeReadDto>(materialType);

            return Ok(materialTypeDto);
        }
    }
}
