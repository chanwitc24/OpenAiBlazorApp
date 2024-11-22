namespace OpenAiBlazorApp.WebAPI.Models;
public class PerFinsDatabaseSettings : IPerFinsDatabaseSettings
{
    public required string CashFlowStatementsCollectionName { get; set; }
    public required string ConnectionString { get; set; }
    public required string DatabaseName { get; set; }
}

public interface IPerFinsDatabaseSettings
{
    string CashFlowStatementsCollectionName { get; set; }
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
}
