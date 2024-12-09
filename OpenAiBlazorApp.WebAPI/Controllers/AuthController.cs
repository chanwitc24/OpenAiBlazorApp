using Microsoft.AspNetCore.Mvc;
using OpenAiBlazorApp.Application.Services;
using OpenAiBlazorApp.Core.ViewModels;

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
                return Unauthorized(new ApiResponse<string>(false, "Invalid username or password", null));
            }

            var token = _jwtTokenService.GenerateToken(user.Id!, user.Username);
            return Ok(new ApiResponse<string>(true, "Login successful", token));
        }

        [HttpPost("generateKey")]
        public async Task<IActionResult> GenerateKey([FromBody] LoginRequest request)
        {
            var key = await _userService.GenerateKey();
            return Ok(new ApiResponse<string>(true, "Key generated successfully", key));
        }
    }    
}
