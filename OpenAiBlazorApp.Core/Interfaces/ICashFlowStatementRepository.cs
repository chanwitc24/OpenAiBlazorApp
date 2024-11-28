using OpenAiBlazorApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenAiBlazorApp.Core.Interfaces
{
    public interface ICashFlowStatementRepository
    {
        Task<CashFlowStatement> GetByIdAsync(string id);
        Task<List<CashFlowStatement>> GetAllAsync();
        Task AddAsync(CashFlowStatement cashFlowStatement);
        Task UpdateAsync(CashFlowStatement cashFlowStatement);
        Task DeleteAsync(string id);
    }
}




















