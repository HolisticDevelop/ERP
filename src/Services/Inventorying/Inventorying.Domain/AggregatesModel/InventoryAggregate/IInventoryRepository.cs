using ERP.Services.Inventorying.Domain.SeedWork;

namespace ERP.Services.Inventorying.Domain.AggregatesModel.InventoryAggregate;

public interface IInventoryRepository : IRepository<Inventory>
{
    Inventory Add(Inventory inventory);
    
    void Update(Inventory inventory);

    Task<Inventory> GetAsync(int inventoryId);
}