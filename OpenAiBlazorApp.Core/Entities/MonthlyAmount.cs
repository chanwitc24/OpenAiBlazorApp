namespace OpenAiBlazorApp.Core.Entities;
public class MonthlyAmount : BaseEntity
{
    public string CashflowId { get; private set; }
    public int Month { get; private set; }
    public decimal Amount { get; private set; }

    public MonthlyAmount(string cashflowId, int month, decimal amount)
    {
        CashflowId = cashflowId ?? throw new ArgumentNullException(nameof(cashflowId));
        Month = month;
        Amount = amount;
    }

}
