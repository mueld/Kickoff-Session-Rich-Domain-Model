using Mediator;

namespace Noser_Fitness.Domain.Events;

public record CourseCraetedDomainEvent(Guid CourseId) : INotification;
