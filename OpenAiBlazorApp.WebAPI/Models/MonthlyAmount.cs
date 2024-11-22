using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OpenAiBlazorApp.WebAPI.Models;
public class MonthlyAmount
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string MonthlyAmountId { get; set; }

    private decimal[] _amounts = new decimal[12];

    public decimal JanAmount
    {
        get => _amounts[0];
        set => _amounts[0] = value;
    }
    public decimal FebAmount
    {
        get => _amounts[1];
        set => _amounts[1] = value;
    }
    public decimal MarAmount
    {
        get => _amounts[2];
        set => _amounts[2] = value;
    }
    public decimal AprAmount
    {
        get => _amounts[3];
        set => _amounts[3] = value;
    }
    public decimal MayAmount
    {
        get => _amounts[4];
        set => _amounts[4] = value;
    }
    public decimal JunAmount
    {
        get => _amounts[5];
        set => _amounts[5] = value;
    }
    public decimal JulAmount
    {
        get => _amounts[6];
        set => _amounts[6] = value;
    }
    public decimal AugAmount
    {
        get => _amounts[7];
        set => _amounts[7] = value;
    }
    public decimal SepAmount
    {
        get => _amounts[8];
        set => _amounts[8] = value;
    }
    public decimal OctAmount
    {
        get => _amounts[9];
        set => _amounts[9] = value;
    }
    public decimal NovAmount
    {
        get => _amounts[10];
        set => _amounts[10] = value;
    }
    public decimal DecAmount
    {
        get => _amounts[11];
        set => _amounts[11] = value;
    }

    public decimal TotalAmount => _amounts.Sum();
}
