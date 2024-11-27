using MongoDB.Bson;
using MongoDB.Driver;
using OpenAiBlazorApp.WebAPI.Models;

namespace OpenAiBlazorApp.WebAPI.Services;
public class CashFlowStatementService
{
    private readonly IMongoCollection<CashFlowStatement> _cashFlowStatement;

    public CashFlowStatementService(IPerFinsDatabaseSettings settings)
    {
        if (string.IsNullOrEmpty(settings.ConnectionString) ||
            string.IsNullOrEmpty(settings.DatabaseName) ||
            string.IsNullOrEmpty(settings.CashFlowStatementsCollectionName))
        {
            throw new ArgumentException("Database settings must be properly initialized.");
        }

        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _cashFlowStatement = database.GetCollection<CashFlowStatement>(settings.CashFlowStatementsCollectionName);
    }
    public List<CashFlowStatement> Get() =>
        _cashFlowStatement.Find(cashFlowStatement => true).ToList();
    public CashFlowStatement Get(string id) =>
        _cashFlowStatement.Find<CashFlowStatement>(cashFlowStatement => cashFlowStatement.Id == id).FirstOrDefault();
    public CashFlowStatement Create(CashFlowStatement cashFlowStatement)
    {
        cashFlowStatement.Id = ObjectId.GenerateNewId().ToString();
        if (cashFlowStatement.CashInflows != null)
        {
            foreach (var cashInflow in cashFlowStatement.CashInflows)
            {
                cashInflow.Id ??= ObjectId.GenerateNewId().ToString();
                if (cashInflow.MonthlyAmountList != null)
                {
                    foreach (var monthlyAmount in cashInflow.MonthlyAmountList)
                    {
                        monthlyAmount.Id ??= ObjectId.GenerateNewId().ToString();
                    }
                }
            }
        }
        if (cashFlowStatement.CashOutflows != null)
        {
            foreach (var cashOutflow in cashFlowStatement.CashOutflows)
            {
                cashOutflow.Id ??= ObjectId.GenerateNewId().ToString();
                if (cashOutflow.MonthlyAmountList != null)
                {
                    foreach (var monthlyAmount in cashOutflow.MonthlyAmountList)
                    {
                        monthlyAmount.Id ??= ObjectId.GenerateNewId().ToString();
                    }
                }
            }
        }
        _cashFlowStatement.InsertOne(cashFlowStatement);
        return cashFlowStatement;
    }
    public void Update(string id, CashFlowStatement cashFlowStatementIn) =>
        _cashFlowStatement.ReplaceOne(cashFlowStatement => cashFlowStatement.Id == id, cashFlowStatementIn);
    public void Remove(CashFlowStatement cashFlowStatementIn) =>
        _cashFlowStatement.DeleteOne(cashFlowStatement => cashFlowStatement.Id == cashFlowStatementIn.Id);
    public void Remove(string id) =>
        _cashFlowStatement.DeleteOne(cashFlowStatement => cashFlowStatement.Id == id);
}
