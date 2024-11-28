using OpenAiBlazorApp.Core.Entities;
using OpenAiBlazorApp.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenAiBlazorApp.Application.Services
{
    public class CashFlowStatementService
    {
        private readonly ICashFlowStatementRepository _cashFlowStatementRepository;

        public CashFlowStatementService(ICashFlowStatementRepository cashFlowStatementRepository)
        {
            _cashFlowStatementRepository = cashFlowStatementRepository;
        }

        public async Task<CashFlowStatement> GetCashFlowStatementByIdAsync(string id)
        {
            return await _cashFlowStatementRepository.GetByIdAsync(id);
        }

        public async Task<List<CashFlowStatement>> GetAllCashFlowStatementsAsync()
        {
            return await _cashFlowStatementRepository.GetAllAsync();
        }

        public async Task AddCashFlowStatementAsync(CashFlowStatement cashFlowStatement)
        {
            await _cashFlowStatementRepository.AddAsync(cashFlowStatement);
        }

        public async Task UpdateCashFlowStatementAsync(CashFlowStatement cashFlowStatement)
        {
            await _cashFlowStatementRepository.UpdateAsync(cashFlowStatement);
        }

        public async Task DeleteCashFlowStatementAsync(string id)
        {
            await _cashFlowStatementRepository.DeleteAsync(id);
        }
    }
}




















