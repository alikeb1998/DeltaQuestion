using DeltaQuestion.Data;
using DeltaQuestion.Models;
using DeltaQuestion.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeltaQuestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : Controller
    {
        private readonly QuestionService _questionService;

        public QuestionController(QuestionService questionService)
        {
            _questionService = questionService;
        }


        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, Question model)
        {
            var res = await _questionService.GetAsync(id);

            if (res is null)
            {
                return NotFound();
            }

            model.Id = res.Id;

            await _questionService.UpdateAsync(id, model);

            return NoContent();
        }
        [Authorize]
        [HttpGet("GetAll")]
        public async Task<List<Question>> Get() =>
            await _questionService.GetAsync();
        
        [Authorize]
        [HttpGet("id")]
        public async Task<ActionResult<Question>> Get(int id)
        {
            var res = await _questionService.GetAsync(id);

            if (res is null)
            {
                return NotFound();
            }

            return res;
        }
        
        [Authorize]
        [HttpGet("questionId")]
        public async Task<ActionResult<DisplayQuestion>> Get(long id)
        {
            var res = await _questionService.GetFullQuestionAsync(id);

            if (res is null)
            {
                return NotFound();
            }

            return res;
        }
        
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Post(Question model)
        {
            await _questionService.CreateAsync(model);

            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }
        
        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _questionService.GetAsync(id);

            if (res is null)
            {
                return NotFound();
            }

            await _questionService.RemoveAsync(id);

            return NoContent();
        }
    }
}