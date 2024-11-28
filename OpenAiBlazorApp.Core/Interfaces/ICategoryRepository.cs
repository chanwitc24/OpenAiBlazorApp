using OpenAiBlazorApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenAiBlazorApp.Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> GetByIdAsync(string id);
        Task<List<Category>> GetAllAsync();
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(string id);
    }
}








