namespace ERP.Services.Inventorying.API.Application.IntegrationEvents.Events;

public class InventoryCreatedIntegrationEvent
{
    public string InventoryId { get; init; }

    public InventoryCreatedIntegrationEvent(string inventoryId) => InventoryId = inventoryId;
}