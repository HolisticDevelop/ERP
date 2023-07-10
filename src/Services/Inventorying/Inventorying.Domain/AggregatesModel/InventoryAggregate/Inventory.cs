using ERP.Services.Inventorying.Domain.Events;
using ERP.Services.Inventorying.Domain.Exceptions;
using ERP.Services.Inventorying.Domain.SeedWork;

namespace ERP.Services.Inventorying.Domain.AggregatesModel.InventoryAggregate;

public class Inventory : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public Price Price { get; private set; }
    public int Quantity { get; private set; }
    public string? Barcode { get; private set; }
    public InventoryStatus InventoryStatus { get; private set; }


    private DateTime _createdDate;
    private int _inventoryStatusId;
    private string _description;

    public Inventory(string name, Price price, int quantity, string? barcode)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
        Barcode = barcode;
        _inventoryStatusId = InventoryStatus.Available.Id;
        _createdDate = DateTime.UtcNow;
        
    }

    public void SetAvailableStatus()
    {
        if (_inventoryStatusId == InventoryStatus.Available.Id)
        {
            StatusChangeException(InventoryStatus.Available);
        }

        _inventoryStatusId = InventoryStatus.Available.Id;
        _description = $"The inventory is available.";
        AddDomainEvent(new InventoryStatusChangedToAvailableDomainEvent(Id));
    }

    public void SetOutOfStockStatus()
    {
        if (_inventoryStatusId == InventoryStatus.OutOfStock.Id)
        {
            StatusChangeException(InventoryStatus.OutOfStock);
        }

        _inventoryStatusId = InventoryStatus.OutOfStock.Id;
        _description = $"The inventory is out of stock";
        AddDomainEvent(new InventoryStatusChangedToOutOfStockDomainEvent(Id));
    }
    
    private void StatusChangeException(InventoryStatus orderStatusToChange)
    {
        throw new InventoryDomainException($"Is not possible to change the inventory status from {InventoryStatus.Name} to {orderStatusToChange.Name}.");
    }
}