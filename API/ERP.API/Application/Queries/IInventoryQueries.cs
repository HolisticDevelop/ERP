namespace ERP.API.Application.Queries;

public interface IInventoryQueries
{
    Task<Inventory> GetInventoryAsync(int id);
}