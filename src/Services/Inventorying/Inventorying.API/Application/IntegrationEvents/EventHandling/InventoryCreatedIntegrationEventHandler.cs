namespace ERP.Services.Inventorying.API.Application.IntegrationEvents.EventHandling;

public class InventoryCreatedIntegrationEventHandler : IIntegrationEventHandler<InventoryCreatedIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<InventoryCreatedIntegrationEventHandler> _logger;

    public InventoryCreatedIntegrationEventHandler(IMediator mediator, ILogger<InventoryCreatedIntegrationEventHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    public async Task Handle(InventoryCreatedIntegrationEvent @event)
    {
        using (_logger.BeginScope(new List<KeyValuePair<string, object>> { new ("IntegrationEventContext", @event.Id) }))
        {
            _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

            var command = new CreateInventoryCommand(@event.InventoryId);

            _logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                command.GetGenericTypeName(),
                nameof(command.InventoryId),
                command);

            await _mediator.Send(command);
        }
    }
}