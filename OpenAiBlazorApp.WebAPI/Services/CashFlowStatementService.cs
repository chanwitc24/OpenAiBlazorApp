using MongoDB.Driver;
using OpenAiBlazorApp.WebAPI.Models;

namespace OpenAiBlazorApp.WebAPI.Services;
public class CashFlowStatementService
{
    private readonly IMongoCollection<CashFlowStatement> _cashFlowStatement;

    public CashFlowStatementService(IPerFinsDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _cashFlowStatement = database.GetCollection<CashFlowStatement>(settings.CashFlowStatementsCollectionName);
    }
}
