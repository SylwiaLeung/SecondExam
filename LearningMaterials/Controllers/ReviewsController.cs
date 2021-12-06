using AutoMapper;
using LearningMaterials.Entities;
using LearningMaterials.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningMaterials.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReviewRepository _repository;

        public ReviewsController(IMapper mapper, IReviewRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        //GET api/reviews/1
        [HttpGet("{id}", Name = "GetReview")]
        public async Task<ActionResult<ReviewReadDto>> GetReview([FromRoute] int id)
        {
            var review = await _repository.GetSingle(id);

            if (review is null) return NotFound(new Response { Status = "Not Found", Message = "No such data in the database :(" });

            var reviewDto = _mapper.Map<ReviewReadDto>(review);

            return Ok(reviewDto);
        }

        //GET api/reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewReadDto>>> GetReviews()
        {
            var reviews = await _repository.GetAll();

            if (reviews is null) return NotFound(new Response { Status = "Not Found", Message = "No such data in the database :(" });

            var reviewDtos = _mapper.Map<List<ReviewReadDto>>(reviews);

            return Ok(reviewDtos);
        }

        //POST api/reviews
        [HttpPost]
        public async Task<ActionResult<ReviewReadDto>> CreateReview([FromBody] ReviewCreateDto createDto)
        {
            var reviewModel = _mapper.Map<Review>(createDto);

            await _repository.Create(reviewModel);
            await _repository.SaveAsync();

            var reviewDto = _mapper.Map<ReviewReadDto>(reviewModel);

            return CreatedAtRoute(nameof(GetReview), new { Id = reviewDto.Id }, reviewDto);
        }

        //DELETE api/reviews/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReview([FromRoute] int id)
        {
            var reviewFromDb = _repository.GetSingle(id).Result;

            if (reviewFromDb is null) return NotFound(new Response { Status = "Not Found", Message = "No such data in the database :(" });

            _repository.Delete(reviewFromDb);
            await _repository.SaveAsync();

            return NoContent();
        }

        //PUT api/reviews/1
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReview([FromRoute] int id, [FromBody] ReviewUpdateDto updateDto)
        {
            var reviewModel = _repository.GetSingle(id).Result;

            if (reviewModel is null) return NotFound(new Response { Status = "Not Found", Message = "No such data in the database :(" });

            _mapper.Map(updateDto, reviewModel);

            _repository.Update(reviewModel);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}