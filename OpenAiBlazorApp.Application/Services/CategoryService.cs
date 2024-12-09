using OpenAiBlazorApp.Core.Entities;
using OpenAiBlazorApp.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenAiBlazorApp.Application.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> GetCategoryByIdAsync(string id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task AddCategoryAsync(Category category)
        {
            // Validation logic here
            if (category == null) throw new System.ArgumentNullException(nameof(category));
            if (string.IsNullOrEmpty(category.Type)) throw new System.ArgumentException("Category type cannot be null or empty", nameof(category.Type));
            if (category.Names == null || category.Names.Count == 0) throw new System.ArgumentException("Category names cannot be null or empty", nameof(category.Names));
            
            await _categoryRepository.AddAsync(category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        public string GetCategoryName(Category category, string languageCode)
        {
            if (category.Names != null && category.Names.TryGetValue(languageCode, out var name))
            {
                return name;
            }
            return category.Names != null && category.Names.TryGetValue("en", out var defaultName) ? defaultName : "Unknown";
        }
    }
}









