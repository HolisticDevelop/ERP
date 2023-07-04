using ERP.Domain.Exceptions;
using ERP.Domain.SeedWork;

namespace ERP.Domain.AggregatesModel.InventoryAggregate;

public class InventoryStatus : Enumeration
{
    public static InventoryStatus Available = new InventoryStatus(1, nameof(Available).ToLowerInvariant());
    public static InventoryStatus OutOfStock = new InventoryStatus(2, nameof(OutOfStock).ToLowerInvariant());
    
    public InventoryStatus(int id, string name) : base(id, name)
    {
    }
    
    public static IEnumerable<InventoryStatus> List() =>
        new[] { Available, OutOfStock };

    public static InventoryStatus FromName(string name)
    {
        var state = List()
            .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

        if (state == null)
        {
            throw new InventoryDomainException(
                $"Possible values for ProductStatus: {String.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }

    public static InventoryStatus From(int id)
    {
        var state = List().SingleOrDefault(s => s.Id == id);

        if (state == null)
        {
            throw new InventoryDomainException(
                $"Possible values for InventoryStatus: {String.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }
}