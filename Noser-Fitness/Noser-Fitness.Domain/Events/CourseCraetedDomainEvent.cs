using Mediator;

namespace Noser_Fitness.Domain.Events;

public record CourseCreatedDomainEvent(Guid CourseId) : DomainEvent;
