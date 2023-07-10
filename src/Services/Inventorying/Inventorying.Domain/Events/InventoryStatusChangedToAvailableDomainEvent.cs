using MediatR;

namespace ERP.Services.Inventorying.Domain.Events;

public class InventoryStatusChangedToAvailableDomainEvent : INotification
{
    public int InventoryId { get; }

    public InventoryStatusChangedToAvailableDomainEvent(int inventoryId)
    {
        InventoryId = inventoryId;
    }
    
}