using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenAiBlazorApp.Application.Services;
using OpenAiBlazorApp.Core.Entities;

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

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetV1()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet, MapToApiVersion("2.0")]
        public async Task<ActionResult<List<Category>>> GetV2()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id:length(24)}", Name = "GetCategory")]
        public async Task<ActionResult<Category>> Get(string id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Category category)
        {
            await _categoryService.AddCategoryAsync(category);
            return CreatedAtRoute("GetCategory", new { id = category.Id!.ToString() }, category);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> Update(string id, Category categoryIn)
        {
            if (id != categoryIn.Id)
            {
                return BadRequest();
            }
            await _categoryService.UpdateCategoryAsync(categoryIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}







