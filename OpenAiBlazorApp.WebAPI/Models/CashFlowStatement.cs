using System.ComponentModel.DataAnnotations;

namespace OpenAiBlazorApp.WebAPI.Models;
public class CashFlowStatement : BaseEntity
{
    [Required]
    public string UserOwner { get; private set; }

    [Required]
    public string Year { get; private set; }

    private IReadOnlyList<CashInflow> _cashInflows;
    public IReadOnlyList<CashInflow> CashInflows
    {
        get => _cashInflows;
        private set
        {
            _cashInflows = value;
            CalculateTotalCashInflows();
            CalculateNetCashFlow();
        }
    }

    private IReadOnlyList<CashOutflow> _cashOutflows;
    public IReadOnlyList<CashOutflow> CashOutflows
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

    public CashFlowStatement(string userOwner, string year, IReadOnlyList<CashInflow> cashInflows, IReadOnlyList<CashOutflow> cashOutflows)
    {
        UserOwner = userOwner ?? throw new ArgumentNullException(nameof(userOwner));
        Year = year ?? throw new ArgumentNullException(nameof(year));
        CashInflows = cashInflows ?? new List<CashInflow>();
        CashOutflows = cashOutflows ?? new List<CashOutflow>();
    }

    private void CalculateTotalCashInflows()
    {
        _totalCashInflows = _cashInflows?.Sum(x => x.TotalCashInflows) ?? 0;
    }

    private void CalculateTotalCashOutflows()
    {
        _totalCashOutflows = _cashOutflows?.Sum(x => x.TotalCashOutflows) ?? 0;
    }

    private void CalculateNetCashFlow()
    {
        _netCashFlow = _totalCashInflows - _totalCashOutflows;
    }
}
