using ERP.Domain.AggregatesModel.InventoryAggregate;
using ERP.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace ERP.Framework.Repositories;

public class InventoryRepository : IInventoryRepository
{
    private readonly InventoryContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public InventoryRepository(InventoryContext context)
    {
        _context = context;
    }

    public Inventory Add(Inventory inventory)
    {
        return _context.Inventories.Add(inventory).Entity;
    }

    public void Update(Inventory inventory)
    {
        _context.Entry(inventory).State = EntityState.Modified;
    }

    public async Task<Inventory> GetAsync(int inventoryId)
    {
        var inventory = await _context
            .Inventories
            .Include(x => x.Price)
            .FirstOrDefaultAsync(o => o.Id == inventoryId);
        if (inventory == null)
        {
            inventory = _context
                .Inventories
                .Local
                .FirstOrDefault(i => i.Id == inventoryId);
        }
        if (inventory != null)
        {
            await _context.Entry(inventory)
                .Reference(i => i.InventoryStatus).LoadAsync();
        }

        return inventory;
    }
}