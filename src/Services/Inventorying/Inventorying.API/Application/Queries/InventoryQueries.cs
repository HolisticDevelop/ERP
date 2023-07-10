using Microsoft.Data.SqlClient;

namespace ERP.Services.Inventorying.API.Application.Queries;

public class InventoryQueries : IInventoryQueries
{
    private string _connectionString = string.Empty;

    public InventoryQueries(string constr)
    {
        _connectionString = !string.IsNullOrWhiteSpace(constr)
            ? constr
            : throw new ArgumentNullException(nameof(constr));
    }

    public async Task<Inventory> GetInventoryAsync(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            var result = await connection.QueryAsync<dynamic>(
                @"select i.[Id] as inventoryid,i.Name as name, o.Description as description,WHERE i.Id=@id"
                , new { id }
            );

            if (result.AsList().Count == 0)
                throw new KeyNotFoundException();

            return MapOrderItems(result);
        }
    }

    private Inventory MapOrderItems(dynamic result)
    {
        var inventory = new Inventory
        {
            Name = result[0].name,
            Quantity = result[0].quantity
        };

        return inventory;
    }
}