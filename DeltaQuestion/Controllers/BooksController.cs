// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
//
// namespace DeltaQuestion.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class BooksController : ControllerBase
//     {
//         private readonly BooksService _booksService;
//         private readonly ITokenService _tokenService;
//
//         public BooksController(BooksService booksService, ITokenService tokenService)
//         {
//             _booksService = booksService;
//             _tokenService = tokenService;
//         }
//         [HttpGet]
//         public async Task<List<Book>> Get() =>
//             await _booksService.GetAsync();
//
//         [Authorize]
//         [HttpGet("id")]
//         public async Task<ActionResult<Book>> Get(int id)
//         {
//            
//             var a =  Request.Headers["Authorization"];
//             var book = await _booksService.GetAsync(id);
//
//             if (book is null)
//             {
//                 return NotFound();
//             }
//
//             return book;
//         }
//
//         [HttpPost]
//         public async Task<IActionResult> Post(Book newBook)
//         {
//             await _booksService.CreateAsync(newBook);
//
//             return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
//         }
//
//         [HttpPut("id")]
//         public async Task<IActionResult> Update(int id, Book updatedBook)
//         {
//             var book = await _booksService.GetAsync(id);
//
//             if (book is null)
//             {
//                 return NotFound();
//             }
//
//             updatedBook.Id = book.Id;
//
//             await _booksService.UpdateAsync(id, updatedBook);
//
//             return NoContent();
//         }
//
//         [HttpDelete("id")]
//         public async Task<IActionResult> Delete(int id)
//         {
//             var book = await _booksService.GetAsync(id);
//
//             if (book is null)
//             {
//                 return NotFound();
//             }
//
//             await _booksService.RemoveAsync(id);
//
//             return NoContent();
//         }
//
//         [AllowAnonymous]
//         [HttpPost("login")]
//         public ActionResult Login(string username, string password)
//         {
//             if (username != "admin" && password != "admin")
//                 return Unauthorized("Invalid Credentials");
//             else
//                 return new JsonResult(new { userName = username, token = _tokenService.CreateToken(username) });
//         }
//
//
//     }
// }
