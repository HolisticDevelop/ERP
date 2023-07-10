using System.Data;
using ERP.Services.Inventorying.Domain.AggregatesModel.InventoryAggregate;
using ERP.Services.Inventorying.Domain.SeedWork;
using ERP.Services.Inventorying.Infrastructure.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;

namespace ERP.Services.Inventorying.Infrastructure;

public class InventoryContext : DbContext, IUnitOfWork
{
    public const string DEFAULT_SCHEMA = "inventory";
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<InventoryStatus> InventoryStatus { get; set; }

    private readonly IMediator _mediator;
    private IDbContextTransaction _currentTransaction;

    public InventoryContext(DbContextOptions<InventoryContext> options) : base(options) { }

    public IDbContextTransaction GetCurrenTransaction() => _currentTransaction;

    public bool HasActiveTransaction => _currentTransaction != null;

    public InventoryContext(DbContextOptions<InventoryContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        System.Diagnostics.Debug.WriteLine("InventoryContext::ctor ->" + this.GetHashCode());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new InventoryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new InventoryStatusTypeConfiguration());
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
        await _mediator.DispatchDomainEventsAsync(this);

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        var result = await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction)
            throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();
            transaction.Commit();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
}

public class InventoryContextDesignFactory : IDesignTimeDbContextFactory<InventoryContext>
{
    public InventoryContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<InventoryContext>()
            .UseSqlServer(
                "Server=.;Initial Catalog=Microsoft.eShopOnContainers.Services.OrderingDb;Integrated Security=true");

        return new InventoryContext(optionsBuilder.Options, new NoMediator());
    }

    class NoMediator : IMediator
    {
        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.FromResult<TResponse>(default);
        }

        public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = new CancellationToken()) where TRequest : IRequest
        {
            return Task.CompletedTask;
        }

        public Task<object?> Send(object request, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.FromResult(default(object));
        }

        public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return default;
        }

        public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = new CancellationToken())
        {
            return default;
        }

        public Task Publish(object notification, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.CompletedTask;
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = new CancellationToken()) where TNotification : INotification
        {
            return Task.CompletedTask;
        }
    }
}