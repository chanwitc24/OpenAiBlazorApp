using System.ComponentModel.DataAnnotations;

namespace OpenAiBlazorApp.WebAPI.Models;
public class CashFlowStatement : BaseEntity
{
    [Required]
    public string UserId { get; private set; }

    [Required]
    public string Year { get; private set; }

    private IReadOnlyList<Cashflow> _cashInflows;
    public IReadOnlyList<Cashflow> CashInflows
    {
        get => _cashInflows;
        private set
        {
            _cashInflows = value;
            CalculateTotalCashInflows();
            CalculateNetCashFlow();
        }
    }

    private IReadOnlyList<Cashflow> _cashOutflows;
    public IReadOnlyList<Cashflow> CashOutflows
    {
        get => _cashOutflows;
        private set
        {
            _cashOutflows = value;
            CalculateTotalCashOutflows();
            CalculateNetCashFlow();
        }
    }

    private decimal _totalCashInflows;
    public decimal TotalCashInflows => _totalCashInflows;

    private decimal _totalCashOutflows;
    public decimal TotalCashOutflows => _totalCashOutflows;

    private decimal _netCashFlow;
    public decimal NetCashFlow => _netCashFlow;

    public CashFlowStatement(string userOwner, string year, IReadOnlyList<Cashflow> cashInflows, IReadOnlyList<Cashflow> cashOutflows)
    {
        UserId = userOwner ?? throw new ArgumentNullException(nameof(userOwner));
        Year = year ?? throw new ArgumentNullException(nameof(year));
        CashInflows = cashInflows ?? new List<Cashflow>();
        CashOutflows = cashOutflows ?? new List<Cashflow>();
    }

    private void CalculateTotalCashInflows()
    {
        _totalCashInflows = _cashInflows?.Sum(x => x.TotalCashflows) ?? 0;
    }

    private void CalculateTotalCashOutflows()
    {
        _totalCashOutflows = _cashOutflows?.Sum(x => x.TotalCashflows) ?? 0;
    }

    private void CalculateNetCashFlow()
    {
        _netCashFlow = _totalCashInflows - _totalCashOutflows;
    }
}
