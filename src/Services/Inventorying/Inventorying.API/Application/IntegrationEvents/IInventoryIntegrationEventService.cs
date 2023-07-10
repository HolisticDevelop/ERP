namespace ERP.Services.Inventorying.API.Application.IntegrationEvents;

public interface IInventoryIntegrationEventService
{
    Task PublishEventsThroughEventBusAsync(Guid transactionId);
    Task AddAndSaveEventAsync(IntegrationEvent evt);
}