using MediatR;

namespace ERP.Services.Inventorying.Domain.Events;

public class InventoryCreatedDomainEvent : INotification
{

    public string Name { get; }

    public InventoryCreatedDomainEvent(string name)
    {
        Name = name;
    }
}