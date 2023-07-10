using MediatR;

namespace ERP.Services.Inventorying.Domain.Events;

public class InventoryStatusChangedToOutOfStockDomainEvent : INotification
{
    public int InventoryId { get; }

    public InventoryStatusChangedToOutOfStockDomainEvent(int inventoryId)
    {
        InventoryId = inventoryId;
    }

}