namespace Domain;

public class ProductId
{
    private readonly Guid _value;

    public ProductId(Guid value)
    {
        if (value == default)
            throw new ArgumentNullException(nameof(value), "Product id cannot be empty");

        _value = value;
    }

    public static implicit operator Guid(ProductId self) => self._value;
}