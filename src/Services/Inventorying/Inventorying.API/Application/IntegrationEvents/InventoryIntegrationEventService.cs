namespace ERP.Services.Inventorying.API.Application.IntegrationEvents;

public class InventoryIntegrationEventService : IInventoryIntegrationEventService
{
    private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
    private readonly IEventBus _eventBus;
    private readonly InventoryContext _inventoryContext;
    private readonly IIntegrationEventLogService _eventLogService;
    private readonly ILogger<InventoryIntegrationEventService> _logger;

    public InventoryIntegrationEventService(Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory, IEventBus eventBus, InventoryContext inventoryContext, IIntegrationEventLogService eventLogService, ILogger<InventoryIntegrationEventService> logger)
    {
        _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
        _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        _inventoryContext = inventoryContext ?? throw new ArgumentNullException(nameof(inventoryContext));
        _eventLogService = _integrationEventLogServiceFactory(_inventoryContext.Database.GetDbConnection());
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task PublishEventsThroughEventBusAsync(Guid transactionId)
    {
        throw new NotImplementedException();
    }

    public Task AddAndSaveEventAsync(IntegrationEvent evt)
    {
        throw new NotImplementedException();
    }
}