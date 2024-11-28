using Microsoft.AspNetCore.Mvc;
using OpenAiBlazorApp.Application.Services;
using OpenAiBlazorApp.Core.Entities;

namespace OpenAiBlazorApp.WebAPI.Controllers
{    
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtTokenService _jwtTokenService;

        public AuthController(UserService userService, JwtTokenService jwtTokenService)
        {
            _userService = userService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userService.AuthenticateAsync(request.Username, request.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var token = _jwtTokenService.GenerateToken(user.Id, user.Username);
            return Ok(new { Token = token });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
