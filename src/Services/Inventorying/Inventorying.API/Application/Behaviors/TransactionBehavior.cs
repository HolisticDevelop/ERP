namespace ERP.Services.Inventorying.API.Application.Behaviors;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
    private readonly InventoryContext _dbContext;
    private readonly IInventoryIntegrationEventService _inventoryIntegrationEventService;

    public TransactionBehavior(ILogger<TransactionBehavior<TRequest, TResponse>> logger, InventoryContext dbContext,
        IInventoryIntegrationEventService inventoryIntegrationEventService)
    {
        _logger = logger ?? throw new ArgumentException(nameof(ILogger));
        _dbContext = dbContext ?? throw new ArgumentException(nameof(InventoryContext));
        _inventoryIntegrationEventService = inventoryIntegrationEventService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var response = default(TResponse);
        var typeName = request.GetGenericTypeName();

        try
        {
            if (_dbContext.HasActiveTransaction)
            {
                return await next();
            }

            var strategy = _dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                Guid transactionId;

                using (var transaction = await _dbContext.BeginTransactionAsync())
                using (LogContext.PushProperty("TransactionContext", transaction.TransactionId))
                {
                    _logger.LogInformation("----- Begin transaction {TransactionId} for {CommandName} ({@Command})",
                        transaction.TransactionId, typeName, request);

                    response = await next();

                    _logger.LogInformation("----- Commit transaction {TransactionId} for {CommandName}",
                        transaction.TransactionId, typeName);

                    await _dbContext.CommitTransactionAsync(transaction);

                    transactionId = transaction.TransactionId;
                }

                await _inventoryIntegrationEventService.PublishEventsThroughEventBusAsync(transactionId);
            });

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);

            throw;
        }
    }
}