using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApi.DTOs.Users;
using TestApi.Helpers;
using TestApi.Interfaces;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        // variable
        private readonly IAuthService _authService;
        // Constructor
        public UserController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            var response = await _authService.RegisterUser(userDto);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            var response = await _authService.LoginUser(userDto);
            if (!response.Success)
                return Unauthorized(response);

            return Ok(response);
        }
    }
}
