namespace Noser_Fitness.Domain.Events;

public record InvitationSentDomainEvent(Guid CourseId, Guid MemberId) : DomainEvent;
