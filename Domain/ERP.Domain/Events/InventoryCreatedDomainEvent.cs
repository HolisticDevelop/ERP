using MediatR;

namespace ERP.Domain.Events;

public class InventoryCreatedDomainEvent : INotification
{

    public string Name { get; }

    public InventoryCreatedDomainEvent(string name)
    {
        Name = name;
    }
}