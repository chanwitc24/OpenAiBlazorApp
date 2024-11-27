using System.ComponentModel.DataAnnotations;

namespace OpenAiBlazorApp.WebAPI.Models;
public class CashOutflow : BaseEntity
{
    [Required]
    public string CashflowCategory { get; private set; }

    private IReadOnlyList<MonthlyAmount> _monthlyAmountList;
    public IReadOnlyList<MonthlyAmount> MonthlyAmountList
    {
        get => _monthlyAmountList;
        private set
        {
            _monthlyAmountList = value;
            CalculateTotalCashOutflows();
        }
    }

    private decimal _totalCashOutflows;
    public decimal TotalCashOutflows => _totalCashOutflows;

    public CashOutflow(string cashflowCategory, IReadOnlyList<MonthlyAmount> monthlyAmountList)
    {
        CashflowCategory = cashflowCategory ?? throw new ArgumentNullException(nameof(cashflowCategory));
        MonthlyAmountList = monthlyAmountList ?? new List<MonthlyAmount>();
    }

    private void CalculateTotalCashOutflows()
    {
        _totalCashOutflows = _monthlyAmountList?.Sum(x => x.TotalAmount) ?? 0;
    }
}
