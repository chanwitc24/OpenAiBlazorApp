namespace OpenAiBlazorApp.WebAPI.Models;
public class PerFinsDatabaseSettings : IPerFinsDatabaseSettings
{
    public required string CashFlowStatementsCollectionName { get; set; }
    public required string CategoriesCollectionName { get; set; }
    public required string CashflowsCollectionName { get; set; }
    public required string UsersCollectionName { get; set; }
    public required string MonthlyAmountsCollectionName { get; set; }
    public required string ConnectionString { get; set; }
    public required string DatabaseName { get; set; }
}

public interface IPerFinsDatabaseSettings
{
    string CashFlowStatementsCollectionName { get; set; }
    string CategoriesCollectionName { get; set; }
    string CashflowsCollectionName { get; set; }
    string UsersCollectionName { get; set; }
    string MonthlyAmountsCollectionName { get; set; }
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
}
