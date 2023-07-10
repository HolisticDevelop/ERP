using Microsoft.Data.SqlClient;

namespace ERP.Services.Inventorying.API.Infrastructure;

public class InventoryContextSeed
{
     public async Task SeedAsync(InventoryContext context, IWebHostEnvironment env, IOptions<InventorySettings> settings, ILogger<InventoryContextSeed> logger)
    {
        var policy = CreatePolicy(logger, nameof(InventoryContextSeed));

        await policy.ExecuteAsync(async () =>
        {

            var useCustomizationData = settings.Value
            .UseCustomizationData;

            var contentRootPath = env.ContentRootPath;


            using (context)
            {
                context.Database.Migrate();

                if (!context.InventoryStatus.Any())
                {
                    context.InventoryStatus.AddRange(useCustomizationData
                                            ? GetInventoryStatusFromFile(contentRootPath, logger)
                                            : GetPredefinedInventoryStatus());
                }

                await context.SaveChangesAsync();
            }
        });
    }
    

    private IEnumerable<InventoryStatus> GetInventoryStatusFromFile(string contentRootPath, ILogger<InventoryContextSeed> log)
    {
        string csvFileInventoryStatus = Path.Combine(contentRootPath, "Setup", "InventoryStatus.csv");

        if (!File.Exists(csvFileInventoryStatus))
        {
            return GetPredefinedInventoryStatus();
        }

        string[] csvheaders;
        try
        {
            string[] requiredHeaders = { "InventoryStatus" };
            csvheaders = GetHeaders(requiredHeaders, csvFileInventoryStatus);
        }
        catch (Exception ex)
        {
            log.LogError(ex, "Error reading CSV headers");
            return GetPredefinedInventoryStatus();
        }

        int id = 1;
        return File.ReadAllLines(csvFileInventoryStatus)
                                    .Skip(1) // skip header row
                                    .SelectTry(x => CreateInventoryStatus(x, ref id))
                                    .OnCaughtException(ex => { log.LogError(ex, "Error creating Inventory status while seeding database"); return null; })
                                    .Where(x => x != null);
    }

    private InventoryStatus CreateInventoryStatus(string value, ref int id)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new Exception("Inventorystatus is null or empty");
        }

        return new InventoryStatus(id++, value.Trim('"').Trim().ToLowerInvariant());
    }

    private IEnumerable<InventoryStatus> GetPredefinedInventoryStatus()
    {
        return new List<InventoryStatus>()
        {
            InventoryStatus.Available,
            InventoryStatus.OutOfStock
        };
    }

    private string[] GetHeaders(string[] requiredHeaders, string csvfile)
    {
        string[] csvheaders = File.ReadLines(csvfile).First().ToLowerInvariant().Split(',');

        if (csvheaders.Count() != requiredHeaders.Count())
        {
            throw new Exception($"requiredHeader count '{requiredHeaders.Count()}' is different then read header '{csvheaders.Count()}'");
        }

        foreach (var requiredHeader in requiredHeaders)
        {
            if (!csvheaders.Contains(requiredHeader))
            {
                throw new Exception($"does not contain required header '{requiredHeader}'");
            }
        }

        return csvheaders;
    }


    private AsyncRetryPolicy CreatePolicy(ILogger<InventoryContextSeed> logger, string prefix, int retries = 3)
    {
        return Policy.Handle<SqlException>().
            WaitAndRetryAsync(
                retryCount: retries,
                sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                onRetry: (exception, timeSpan, retry, ctx) =>
                {
                    logger.LogWarning(exception, "[{prefix}] Error seeding database (attempt {retry} of {retries})", prefix, retry, retries);
                }
            );
    }
}