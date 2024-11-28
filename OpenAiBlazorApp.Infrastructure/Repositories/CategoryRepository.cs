using MongoDB.Driver;
using OpenAiBlazorApp.Core.Entities;
using OpenAiBlazorApp.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenAiBlazorApp.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Category> _categories;

        public CategoryRepository(IMongoDatabase database)
        {
            _categories = database.GetCollection<Category>("Categories");
        }

        public async Task<Category> GetByIdAsync(string id)
        {
            return await _categories.Find(category => category.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _categories.Find(category => true).ToListAsync();
        }

        public async Task AddAsync(Category category)
        {
            await _categories.InsertOneAsync(category);
        }

        public async Task UpdateAsync(Category category)
        {
            await _categories.ReplaceOneAsync(c => c.Id == category.Id, category);
        }

        public async Task DeleteAsync(string id)
        {
            await _categories.DeleteOneAsync(category => category.Id == id);
        }
    }
}











