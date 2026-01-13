using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Noser_Fitness.Domain;

namespace Noser_Fitness.Infrastructure.Interceptors;

public class PublishDomainEventsInterceptor(ISender mediator) : SaveChangesInterceptor
{
    private readonly ISender _mediator = mediator;

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
    )
    {
        DbContext? dbContext = eventData.Context;
        if (dbContext == null)
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var domainEvents = dbContext
            .ChangeTracker.Entries<Entity>()
            .Select(e => e.Entity)
            .SelectMany(e =>
            {
                var domainEvents = e.DomainEvents;
                e.ClearDomainEvents();
                return domainEvents;
            })
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Send(domainEvent, cancellationToken);
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
