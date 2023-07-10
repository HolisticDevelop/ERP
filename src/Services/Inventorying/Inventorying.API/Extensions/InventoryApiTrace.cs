namespace ERP.Services.Inventorying.API.Extensions;

internal static partial class InventoryApiTrace
{
    [LoggerMessage(EventId = 1, EventName = "InventoryStatusUpdated", Level = LogLevel.Trace,
        Message = "Inventory with Id: {InventoryId} has been successfully updated to status {Status} ({Id})")]
    public static partial void LogInventoryStatusUpdated(ILogger logger, int orderId, string status, int id);
}