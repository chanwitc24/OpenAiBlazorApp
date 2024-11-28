using MongoDB.Driver;
using OpenAiBlazorApp.Core.Entities;
using OpenAiBlazorApp.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenAiBlazorApp.Infrastructure.Repositories
{
    public class CashFlowStatementRepository : ICashFlowStatementRepository
    {
        private readonly IMongoCollection<CashFlowStatement> _cashFlowStatements;

        public CashFlowStatementRepository(IMongoDatabase database)
        {
            _cashFlowStatements = database.GetCollection<CashFlowStatement>("CashFlowStatements");
        }

        public async Task<CashFlowStatement> GetByIdAsync(string id)
        {
            return await _cashFlowStatements.Find(cashFlowStatement => cashFlowStatement.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<CashFlowStatement>> GetAllAsync()
        {
            return await _cashFlowStatements.Find(cashFlowStatement => true).ToListAsync();
        }

        public async Task AddAsync(CashFlowStatement cashFlowStatement)
        {
            await _cashFlowStatements.InsertOneAsync(cashFlowStatement);
        }

        public async Task UpdateAsync(CashFlowStatement cashFlowStatement)
        {
            await _cashFlowStatements.ReplaceOneAsync(c => c.Id == cashFlowStatement.Id, cashFlowStatement);
        }

        public async Task DeleteAsync(string id)
        {
            await _cashFlowStatements.DeleteOneAsync(cashFlowStatement => cashFlowStatement.Id == id);
        }
    }
}





















