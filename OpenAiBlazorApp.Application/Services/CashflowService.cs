using OpenAiBlazorApp.Core.Entities;
using OpenAiBlazorApp.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenAiBlazorApp.Application.Services
{
    public class CashflowService
    {
        private readonly ICashflowRepository _cashflowRepository;

        public CashflowService(ICashflowRepository cashflowRepository)
        {
            _cashflowRepository = cashflowRepository;
        }

        public async Task<Cashflow> GetCashflowByIdAsync(string id)
        {
            return await _cashflowRepository.GetByIdAsync(id);
        }

        public async Task<List<Cashflow>> GetAllCashflowsAsync()
        {
            return await _cashflowRepository.GetAllAsync();
        }

        public async Task AddCashflowAsync(Cashflow cashflow)
        {
            await _cashflowRepository.AddAsync(cashflow);
        }

        public async Task UpdateCashflowAsync(Cashflow cashflow)
        {
            await _cashflowRepository.UpdateAsync(cashflow);
        }

        public async Task DeleteCashflowAsync(string id)
        {
            await _cashflowRepository.DeleteAsync(id);
        }
    }
}

















