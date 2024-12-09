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
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(CategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<Category>>>> GetV1()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(new ApiResponse<List<Category>>(true, "Categories retrieved successfully", categories));
        }

        [HttpGet, MapToApiVersion("2.0")]
        public async Task<ActionResult<ApiResponse<List<Category>>>> GetV2()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(new ApiResponse<List<Category>>(true, "Categories retrieved successfully", categories));
        }

        [HttpGet("{id:length(24)}", Name = "GetCategory")]
        public async Task<ActionResult<ApiResponse<Category>>> Get(string id, [FromQuery] string lang = "en")
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound(new ApiResponse<Category>(false, "Category not found", null!));
            }
            var localizedCategory = new Category
            {
                Id = category.Id,
                Type = category.Type,
                Names = new Dictionary<string, string> { { lang, _categoryService.GetCategoryName(category, lang) } }
            };
            return Ok(new ApiResponse<Category>(true, "Category retrieved successfully", localizedCategory));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Category>>> Create(CategoryRequest categoryRequest)
        {
            var category = _mapper.Map<Category>(categoryRequest);
            await _categoryService.AddCategoryAsync(category);
            return CreatedAtRoute("GetCategory", new { id = category.Id!.ToString() }, new ApiResponse<Category>(true, "Category created successfully", category));
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult<ApiResponse<Category>>> Update(string id, Category categoryIn)
        {
            if (id != categoryIn.Id)
            {
                return BadRequest(new ApiResponse<Category>(false, "Category ID mismatch", null!));
            }
            await _categoryService.UpdateCategoryAsync(categoryIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(string id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}







