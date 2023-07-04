using MediatR;

namespace ERP.Domain.Events;

public class InventoryStatusChangedToOutOfStockDomainEvent : INotification
{
    public int InventoryId { get; }

    public InventoryStatusChangedToOutOfStockDomainEvent(int inventoryId)
    {
        InventoryId = inventoryId;
    }

}