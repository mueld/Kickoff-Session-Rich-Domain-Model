namespace Noser_Fitness.Domain.Events;

public record InvitationAcceptedDomainEvent(Guid CourseId, Guid MemberId) : DomainEvent;
