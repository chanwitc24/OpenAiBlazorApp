using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OpenAiBlazorApp.WebAPI.Models;
public class CashOutflow
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string CashOutflowId { get; set; }
    public string? CashflowCategory { get; set; }
    private List<MonthlyAmount>? _monthlyAmountList;
    public List<MonthlyAmount>? MonthlyAmountList
    {
        get => _monthlyAmountList;
        set
        {
            _monthlyAmountList = value;
            _totalCashOutflows = _monthlyAmountList?.Sum(x => x.TotalAmount) ?? 0;
        }
    }
    private decimal _totalCashOutflows;
    public decimal TotalCashOutflows => _totalCashOutflows;
}
