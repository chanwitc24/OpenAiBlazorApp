using MongoDB.Driver;
using OpenAiBlazorApp.Core.Entities;
using OpenAiBlazorApp.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenAiBlazorApp.Infrastructure.Repositories
{
    public class MonthlyAmountRepository : IMonthlyAmountRepository
    {
        private readonly IMongoCollection<MonthlyAmount> _monthlyAmounts;

        public MonthlyAmountRepository(IMongoDatabase database)
        {
            _monthlyAmounts = database.GetCollection<MonthlyAmount>("MonthlyAmounts");
        }

        public async Task<MonthlyAmount> GetByIdAsync(string id)
        {
            return await _monthlyAmounts.Find(monthlyAmount => monthlyAmount.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<MonthlyAmount>> GetAllAsync()
        {
            return await _monthlyAmounts.Find(monthlyAmount => true).ToListAsync();
        }

        public async Task AddAsync(MonthlyAmount monthlyAmount)
        {
            await _monthlyAmounts.InsertOneAsync(monthlyAmount);
        }

        public async Task UpdateAsync(MonthlyAmount monthlyAmount)
        {
            await _monthlyAmounts.ReplaceOneAsync(m => m.Id == monthlyAmount.Id, monthlyAmount);
        }

        public async Task DeleteAsync(string id)
        {
            await _monthlyAmounts.DeleteOneAsync(monthlyAmount => monthlyAmount.Id == id);
        }
    }
}













