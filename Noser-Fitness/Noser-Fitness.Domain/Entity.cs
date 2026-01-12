using Noser_Fitness.Domain.Events;

namespace Noser_Fitness.Domain;

public abstract class Entity
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    private readonly List<IDomainEvent> _domainEvents = [];

    public List<IDomainEvent> DomainEvents => [.. _domainEvents];

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
