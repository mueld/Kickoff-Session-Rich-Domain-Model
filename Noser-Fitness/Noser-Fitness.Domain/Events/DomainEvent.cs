using Mediator;

namespace Noser_Fitness.Domain.Events;

public abstract record DomainEvent : IDomainEvent
{
    public Guid Id { get; init; }
}

public interface IDomainEvent : INotification
{
    public Guid Id { get; }
}
