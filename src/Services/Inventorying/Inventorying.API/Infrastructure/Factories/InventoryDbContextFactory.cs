namespace ERP.Services.Inventorying.API.Infrastructure.Factories;

public class InventoryDbContextFactory
{
    public InventoryContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<InventoryContext>();

        optionsBuilder.UseSqlServer(config["ConnectionString"], sqlServerOptionsAction: o => o.MigrationsAssembly("Ordering.API"));

        return new InventoryContext(optionsBuilder.Options);
    }
}