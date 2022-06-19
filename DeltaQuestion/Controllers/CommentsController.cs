using DeltaComment.Services;
using DeltaQuestion.Data;
using DeltaQuestion.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeltaQuestion.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : Controller
{
     private readonly CommentService _commentsService;
    
            public CommentsController(CommentService commentsService)
            {
                _commentsService = commentsService;
            }
            
            [Authorize]
            [HttpPut("update")]
            public async Task<IActionResult> Update(int id, Comment model)
            {
                var res = await _commentsService.GetAsync(id);
    
                if (res is null)
                {
                    return NotFound();
                }
    
                model.Id = res.Id;
    
                await _commentsService.UpdateAsync(id, model);
    
                return NoContent();
            }
            [Authorize]
            [HttpGet("GetAll")]
            public async Task<List<Comment>> Get() =>
                await _commentsService.GetAsync();
            
            [Authorize]
            [HttpGet("id")]
            public async Task<ActionResult<Comment>> Get(int id)
            {
                var res = await _commentsService.GetAsync(id);
    
                if (res is null)
                {
                    return NotFound();
                }
    
                return res;
            }
            
            [Authorize]
            [HttpPost("create")]
            public async Task<IActionResult> Post(Comment model)
            {
                await _commentsService.CreateAsync(model);
                return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
            }
            
            [HttpDelete("id")]
            public async Task<IActionResult> Delete(int id)
            {
                var res = await _commentsService.GetAsync(id);
    
                if (res is null)
                {
                    return NotFound();
                }
    
                await _commentsService.RemoveAsync(id);
    
                return NoContent();
            }
}