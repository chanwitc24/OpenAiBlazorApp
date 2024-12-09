using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenAiBlazorApp.Application.Services;
using OpenAiBlazorApp.Core.Entities;
using OpenAiBlazorApp.Core.ViewModels;

namespace OpenAiBlazorApp.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public UserController(UserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new ApiResponse<string?>(false, "User not found", null));
            }

            var userResponse = _mapper.Map<UserResponse>(user);
            return Ok(new ApiResponse<UserResponse>(true, "User retrieved successfully", userResponse));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            var userResponses = _mapper.Map<List<UserResponse>>(users);
            return Ok(new ApiResponse<List<UserResponse>>(true, "Users retrieved successfully", userResponses));
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserRequest userRequest)
        {
            var user = _mapper.Map<User>(userRequest);
            user.Id = Guid.NewGuid().ToString(); // Ensure Id is set            
            await _userService.AddUserAsync(user);
            var userResponse = _mapper.Map<UserResponse>(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, new ApiResponse<UserResponse>(true, "User added successfully", userResponse));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, UserRequest userRequest)
        {
            var user = _mapper.Map<User>(userRequest);
            user.Id = id;
            if (id != user.Id)
            {
                return BadRequest(new ApiResponse<string?>(false, "User ID mismatch", null));
            }
            await _userService.UpdateUserAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpGet("parent/{parentId:length(24)}")]
        public async Task<ActionResult<ApiResponse<List<User>>>> GetUsersByParentId(string parentId)
        {
            var users = await _userService.GetUsersByParentIdAsync(parentId);
            if (users == null || users.Count == 0)
            {
                return NotFound(new ApiResponse<List<User>>(false, "No users found for the specified parent", null!));
            }
            return Ok(new ApiResponse<List<User>>(true, "Users retrieved successfully", users));
        }
    }
}






