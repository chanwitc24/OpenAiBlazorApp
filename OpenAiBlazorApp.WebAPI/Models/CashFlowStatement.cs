using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OpenAiBlazorApp.WebAPI.Models;
public class CashFlowStatement
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; }
    private List<CashInflow>? _cashInflows;
    public List<CashInflow>? CashInflows
    {
        get => _cashInflows;
        set
        {
            _cashInflows = value;
            _totalCashInflows = _cashInflows?.Sum(x => x.TotalCashInflows) ?? 0;
            _netCashFlow = _totalCashInflows - _totalCashOutflows;
        }
    }
    private List<CashOutflow>? _cashOutflows;
    public List<CashOutflow>? CashOutflows
    {
        get => _cashOutflows;
        set
        {
            _cashOutflows = value;
            _totalCashOutflows = _cashOutflows?.Sum(x => x.TotalCashOutflows) ?? 0;
            _netCashFlow = _totalCashInflows - _totalCashOutflows;
        }
    }
    private decimal _totalCashInflows;
    public decimal TotalCashInflows => _totalCashInflows;
    private decimal _totalCashOutflows;
    public decimal TotalCashOutflows => _totalCashOutflows;
    private decimal _netCashFlow;
    public decimal NetCashFlow => _netCashFlow;
}
