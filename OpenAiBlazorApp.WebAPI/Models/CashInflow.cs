using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OpenAiBlazorApp.WebAPI.Models;
public class CashInflow
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string CashInflowId { get; set; }
    public string? CashflowCategory { get; set; }
    private List<MonthlyAmount>? _monthlyAmountList;
    public List<MonthlyAmount>? MonthlyAmountList
    {
        get => _monthlyAmountList;
        set
        {
            _monthlyAmountList = value;
            _totalCashInflows = _monthlyAmountList?.Sum(x => x.TotalAmount) ?? 0;
        }
    }
    private decimal _totalCashInflows;
    public decimal TotalCashInflows => _totalCashInflows;
}
