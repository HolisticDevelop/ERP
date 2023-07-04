using MediatR;

namespace ERP.Domain.Events;

public class InventoryStatusChangedToAvailableDomainEvent : INotification
{
    public int InventoryId { get; }

    public InventoryStatusChangedToAvailableDomainEvent(int inventoryId)
    {
        InventoryId = inventoryId;
    }
    
}