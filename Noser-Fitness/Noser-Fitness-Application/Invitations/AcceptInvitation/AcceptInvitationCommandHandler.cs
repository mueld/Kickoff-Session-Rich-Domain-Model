using Mediator;
using Microsoft.EntityFrameworkCore;
using Noser_Fitness.Domain;
using Noser_Fitness.Domain.Abstractions;
using Noser_Fitness.Domain.Events;

namespace Noser_Fitness_Application.Invitations.AcceptInvitation;

public class AcceptInvitationCommandHandler(INoserFitnessDbContext dbContext, ISender mediator)
    : ICommandHandler<AcceptInvitationCommand>
{
    private readonly INoserFitnessDbContext _dbContext = dbContext;
    private readonly ISender _mediator = mediator;

    public async ValueTask<Unit> Handle(AcceptInvitationCommand command, CancellationToken cancellationToken)
    {
        var invitation =
            await _dbContext.Invitations.SingleAsync(x => x.Id == command.InvitationId)
            ?? throw new InvalidOperationException("Invitation not found");

        var course =
            await _dbContext.Courses.SingleAsync(x => x.Id == invitation.CourseId)
            ?? throw new InvalidOperationException("Course not found");

        var isExpired =
            course.InvitationsValidBeforeInHours.HasValue
            && DateTime.UtcNow - course.ScheduledAtUtc > TimeSpan.FromHours(course.InvitationsValidBeforeInHours.Value);

        if (course.MaxAttendees >= course.Attendees.Count)
        {
            invitation.State = InvitationState.Expired;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        if (isExpired)
        {
            invitation.State = InvitationState.Expired;
            invitation.ModifiedAtUtc = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        invitation.State = InvitationState.Accepted;
        invitation.ModifiedAtUtc = DateTime.UtcNow;

        var attendee = new Attendee
        {
            Id = Guid.NewGuid(),
            CourseId = course.Id,
            MemberId = invitation.MemberId,
        };

        course.Attendees.Add(attendee);

        await _dbContext.SaveChangesAsync(cancellationToken);

        var domainEvent = new InvitationAcceptedDomainEvent(invitation.CourseId, invitation.MemberId);
        await _mediator.Send(domainEvent);

        return Unit.Value;
    }
}
