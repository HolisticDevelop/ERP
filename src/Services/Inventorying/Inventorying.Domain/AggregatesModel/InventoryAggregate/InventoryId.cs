namespace ERP.Services.Inventorying.Domain.AggregatesModel.InventoryAggregate;

public class InventoryId
{
    private readonly Guid _value;

    public InventoryId(Guid value)
    {
        if (value == default)
            throw new ArgumentNullException(nameof(value), "Product id cannot be empty");

        _value = value;
    }

    public static implicit operator Guid(InventoryId self) => self._value;
}