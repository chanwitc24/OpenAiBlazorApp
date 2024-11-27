using System.ComponentModel.DataAnnotations;

namespace OpenAiBlazorApp.WebAPI.Models;
public class CashInflow : BaseEntity
{
    [Required]
    public string CashflowCategoryId { get; private set; }

    private IReadOnlyList<MonthlyAmount> _monthlyAmountList;
    public IReadOnlyList<MonthlyAmount> MonthlyAmountList
    {
        get => _monthlyAmountList;
        private set
        {
            _monthlyAmountList = value;
            CalculateTotalCashInflows();
        }
    }

    private decimal _totalCashInflows;
    public decimal TotalCashInflows => _totalCashInflows;

    public CashInflow(string cashflowCategoryId, IReadOnlyList<MonthlyAmount> monthlyAmountList)
    {
        CashflowCategoryId = cashflowCategoryId ?? throw new ArgumentNullException(nameof(cashflowCategoryId));
        MonthlyAmountList = monthlyAmountList ?? new List<MonthlyAmount>();
    }

    private void CalculateTotalCashInflows()
    {
        _totalCashInflows = _monthlyAmountList?.Sum(x => x.TotalAmount) ?? 0;
    }
}
