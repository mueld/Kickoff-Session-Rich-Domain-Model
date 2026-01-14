using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Noser_Fitness.Domain;

namespace Noser_Fitness.Infrastructure.Interceptors;

public class PublishDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    private readonly IMediator _mediator = mediator;

    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = new CancellationToken()
    )
    {
        DbContext? dbContext = eventData.Context;
        if (dbContext == null)
        {
            return await base.SavedChangesAsync(eventData, result, cancellationToken);
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
            await _mediator.Publish(domainEvent, cancellationToken);
        }
        var savedChangesResult = await base.SavedChangesAsync(eventData, result, cancellationToken);
        return savedChangesResult;
    }
}
