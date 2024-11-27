namespace OpenAiBlazorApp.WebAPI.Models;
public class MonthlyAmount : BaseEntity
{
    public decimal JanAmount { get; private set; }
    public decimal FebAmount { get; private set; }
    public decimal MarAmount { get; private set; }
    public decimal AprAmount { get; private set; }
    public decimal MayAmount { get; private set; }
    public decimal JunAmount { get; private set; }
    public decimal JulAmount { get; private set; }
    public decimal AugAmount { get; private set; }
    public decimal SepAmount { get; private set; }
    public decimal OctAmount { get; private set; }
    public decimal NovAmount { get; private set; }
    public decimal DecAmount { get; private set; }

    public decimal TotalAmount => JanAmount + FebAmount + MarAmount + AprAmount + MayAmount + JunAmount + JulAmount + AugAmount + SepAmount + OctAmount + NovAmount + DecAmount;

    public MonthlyAmount(decimal janAmount, decimal febAmount, decimal marAmount, decimal aprAmount, decimal mayAmount, decimal junAmount, decimal julAmount, decimal augAmount, decimal sepAmount, decimal octAmount, decimal novAmount, decimal decAmount)
    {
        JanAmount = janAmount;
        FebAmount = febAmount;
        MarAmount = marAmount;
        AprAmount = aprAmount;
        MayAmount = mayAmount;
        JunAmount = junAmount;
        JulAmount = julAmount;
        AugAmount = augAmount;
        SepAmount = sepAmount;
        OctAmount = octAmount;
        NovAmount = novAmount;
        DecAmount = decAmount;
    }
}
