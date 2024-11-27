using System.ComponentModel.DataAnnotations;

namespace OpenAiBlazorApp.WebAPI.Models;
public class Cashflow : BaseEntity
{
    [Required]
    public string CashflowStatementId { get; private set; }
    [Required]
    public string CashflowCategoryId { get; private set; }

    private IReadOnlyList<MonthlyAmount> _monthlyAmounts;
    public IReadOnlyList<MonthlyAmount> MonthlyAmounts
    {
        get => _monthlyAmounts;
        private set
        {
            _monthlyAmounts = value;
            CalculateTotalCashflows();
        }
    }

    private decimal _totalCashflows;
    public decimal TotalCashflows => _totalCashflows;

    public Cashflow(string cashflowStatementId ,string cashflowCategory, IReadOnlyList<MonthlyAmount> monthlyAmounts)
    {
        CashflowStatementId = cashflowStatementId ?? throw new ArgumentNullException(nameof(cashflowStatementId));
        CashflowCategoryId = cashflowCategory ?? throw new ArgumentNullException(nameof(cashflowCategory));
        MonthlyAmounts = monthlyAmounts ?? new List<MonthlyAmount>();
    }

    private void CalculateTotalCashflows()
    {
        _totalCashflows = _monthlyAmounts?.Sum(x => x.Amount) ?? 0;
    }
}
