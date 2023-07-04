using ERP.Domain.SeedWork;

namespace ERP.Domain.AggregatesModel.InventoryAggregate;

public interface IInventoryRepository : IRepository<Inventory>
{
    Inventory Add(Inventory inventory);
    
    void Update(Inventory inventory);

    Task<Inventory> GetAsync(int inventoryId);
}