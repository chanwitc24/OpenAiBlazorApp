using OpenAiBlazorApp.Core.Entities;
using OpenAiBlazorApp.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenAiBlazorApp.Application.Services
{
    public class MonthlyAmountService
    {
        private readonly IMonthlyAmountRepository _monthlyAmountRepository;

        public MonthlyAmountService(IMonthlyAmountRepository monthlyAmountRepository)
        {
            _monthlyAmountRepository = monthlyAmountRepository;
        }

        public async Task<MonthlyAmount> GetMonthlyAmountByIdAsync(string id)
        {
            return await _monthlyAmountRepository.GetByIdAsync(id);
        }

        public async Task<List<MonthlyAmount>> GetAllMonthlyAmountsAsync()
        {
            return await _monthlyAmountRepository.GetAllAsync();
        }

        public async Task AddMonthlyAmountAsync(MonthlyAmount monthlyAmount)
        {
            await _monthlyAmountRepository.AddAsync(monthlyAmount);
        }

        public async Task UpdateMonthlyAmountAsync(MonthlyAmount monthlyAmount)
        {
            await _monthlyAmountRepository.UpdateAsync(monthlyAmount);
        }

        public async Task DeleteMonthlyAmountAsync(string id)
        {
            await _monthlyAmountRepository.DeleteAsync(id);
        }
    }
}












