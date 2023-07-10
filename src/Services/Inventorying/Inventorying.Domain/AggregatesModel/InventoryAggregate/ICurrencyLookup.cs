using ERP.Services.Inventorying.Domain.SeedWork;

namespace ERP.Services.Inventorying.Domain.AggregatesModel.InventoryAggregate;
public interface ICurrencyLookup
{
    CurrencyDetails FindCurrency(string currencyCode);
}

public class CurrencyDetails : ValueObject
{
    public string CurrencyCode { get; set; }
    public bool InUse { get; set; }
    public int DecimalPlaces { get; set; }

    public static CurrencyDetails None = new CurrencyDetails { InUse = false };
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CurrencyCode;
        yield return InUse;
        yield return DecimalPlaces;
    }
}