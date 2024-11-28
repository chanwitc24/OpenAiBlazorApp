using OpenAiBlazorApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenAiBlazorApp.Core.Interfaces
{
    public interface IMonthlyAmountRepository
    {
        Task<MonthlyAmount> GetByIdAsync(string id);
        Task<List<MonthlyAmount>> GetAllAsync();
        Task AddAsync(MonthlyAmount monthlyAmount);
        Task UpdateAsync(MonthlyAmount monthlyAmount);
        Task DeleteAsync(string id);
    }
}












