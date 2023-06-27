using Domain.VO;
using ERPApi.Framework;

namespace Domain;

public class Product : Entity
{

    public ProductId Id { get; private set; }
    public Price Price { get; private set; }
    public ProductState State { get; private set; }

    public Product(ProductId id) =>
        Apply(new Events.ProductCreated
        {
            Id = id
        });

    protected override void When(object @event)
    {
        switch (@event)
        {
            case Events.ProductCreated e:
                Id = new ProductId(e.Id);
                break;
        }
    }

    protected override void EnsureValidState()
    {
        bool valid = Id != null;
        switch (State)
        {
            case ProductState.Active:
                valid = valid
                    && Price?.Amount > 0;
                break;
                
        }
    }

    public enum ProductState
    {
        InStock,
        OutStock,
        Active
    }
}