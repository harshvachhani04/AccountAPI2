using AccountAPI2.Common;
using AccountAPI2.Data;
using AccountAPI2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AccountAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly JWTGenerator _jWTGenerator;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository, JWTGenerator jWTGenerator)
        {
            this._logger = logger;
            this._userRepository = userRepository;
            this._jWTGenerator = jWTGenerator;
        }
        [Route("GetUsers")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            return Ok(users);
        }
        [Route("CreateUser")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]User user)
        {
            if (ModelState.IsValid)
            {
                User existUser = await _userRepository.GetCurrentUser(username: user.Username);
                if (existUser == null)
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    int result = await _userRepository.CreateUser(user);
                    if (result > 0)
                        return Ok(new { result = result });
                }
                else
                {
                    return Ok(new { message = "Already exist" });
                }
            }
            return NotFound();
        }
        [Route("LoginUser")]
        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody]LoginDTO loginDTO)
        {
            User user =  await _userRepository.GetCurrentUser(username: loginDTO.username);
            if(user != null)
            {
                var isValid = BCrypt.Net.BCrypt.Verify(loginDTO.password, user.Password);
                if (isValid)
                {
                    var token = _jWTGenerator.GenerateToken(user.UserId, user.Role);
                    return Ok(new { token = token });
                }
                return NotFound(new { message = "Username or password incorrect" });
            }
            return NotFound(new { message = "No user found" });
        }
        [Route("GetUser")]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if(userId > 0)
            {
                User user = await _userRepository.GetCurrentUser(userId: userId);
                return Ok(user);
            }
            return NoContent();
        }
    }
}
