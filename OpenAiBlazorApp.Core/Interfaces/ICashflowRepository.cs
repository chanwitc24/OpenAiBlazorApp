using OpenAiBlazorApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenAiBlazorApp.Core.Interfaces
{
    public interface ICashflowRepository
    {
        Task<Cashflow> GetByIdAsync(string id);
        Task<List<Cashflow>> GetAllAsync();
        Task AddAsync(Cashflow cashflow);
        Task UpdateAsync(Cashflow cashflow);
        Task DeleteAsync(string id);
    }
}

















