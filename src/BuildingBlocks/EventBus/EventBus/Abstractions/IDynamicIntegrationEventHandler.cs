namespace ERP.BuildingBlocks.EventBus.Abstractions;

public interface IDynamicIntegrationEventHandler
{
    Task Handle(dynamic eventData);
}