using MongoDB.Driver;
using OpenAiBlazorApp.Core.Entities;
using OpenAiBlazorApp.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenAiBlazorApp.Infrastructure.Repositories
{
    public class CashflowRepository : ICashflowRepository
    {
        private readonly IMongoCollection<Cashflow> _cashflows;

        public CashflowRepository(IMongoDatabase database)
        {
            _cashflows = database.GetCollection<Cashflow>("Cashflows");
        }

        public async Task<Cashflow> GetByIdAsync(string id)
        {
            return await _cashflows.Find(cashflow => cashflow.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Cashflow>> GetAllAsync()
        {
            return await _cashflows.Find(cashflow => true).ToListAsync();
        }

        public async Task AddAsync(Cashflow cashflow)
        {
            await _cashflows.InsertOneAsync(cashflow);
        }

        public async Task UpdateAsync(Cashflow cashflow)
        {
            await _cashflows.ReplaceOneAsync(c => c.Id == cashflow.Id, cashflow);
        }

        public async Task DeleteAsync(string id)
        {
            await _cashflows.DeleteOneAsync(cashflow => cashflow.Id == id);
        }
    }
}


















