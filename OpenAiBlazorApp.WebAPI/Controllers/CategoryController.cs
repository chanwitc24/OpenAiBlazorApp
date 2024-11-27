using Microsoft.AspNetCore.Mvc;
using OpenAiBlazorApp.WebAPI.Models;
using OpenAiBlazorApp.WebAPI.Services;

namespace OpenAiBlazorApp.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult<List<Category>> GetV1() =>
            _categoryService.Get();

        [HttpGet, MapToApiVersion("2.0")]
        public ActionResult<List<Category>> GetV2() =>
            _categoryService.Get();

        [HttpGet("{id:length(24)}", Name = "GetCategory")]
        public ActionResult<Category> Get(string id)
        {
            var category = _categoryService.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            return category;
        }

        [HttpPost]
        public ActionResult<Category> Create(Category category)
        {
            _categoryService.Create(category);
            return CreatedAtRoute("GetCategory", new { id = category.Id!.ToString() }, category);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Category categoryIn)
        {
            var category = _categoryService.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            _categoryService.Update(id, categoryIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var category = _categoryService.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            _categoryService.Remove(category.Id!);
            return NoContent();
        }
    }
}







