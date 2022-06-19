using DeltaQuestion.Data;
using DeltaQuestion.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeltaQuestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly UserService _userService;
        public UserController(ITokenService tokenService, UserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("SignUp")]
        public ActionResult Signup([FromBody]User user)
        {
            var res = _userService.CreateAsync(user);
            //if (username != "admin" && password != "admin")
            //    return Unauthorized("Invalid Credentials");
           // else
                return new JsonResult(new { userName = user.UserName, token = _tokenService.CreateToken(user.UserName) });
        }

        [Authorize]
        [HttpPost("GetAll")]
        public async Task<List<User>> GetAll()
        {
            var res = await _userService.GetAsync();
            //if (username != "admin" && password != "admin")
            //    return Unauthorized("Invalid Credentials");
            // else
            return new List<User>(res);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var res = await _userService.GetAsync(username, password);
           // if (username != "admin" && password != "admin")
           if(res==null)
                return Unauthorized("Invalid Credentials");
            else
                return new JsonResult(new { userName = res.UserName, token = "Bearer " +_tokenService.CreateToken(res.UserName) });
        }
        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, User updatedUser)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            updatedUser.Id = user.Id;

            await _userService.UpdateAsync(id, updatedUser);

            return NoContent();
        }
    }
}
