using Mediator;
using Microsoft.EntityFrameworkCore;
using Noser_Fitness.Domain;
using Noser_Fitness.Domain.Abstractions;

namespace Noser_Fitness_Application.Invitations.AcceptInvitation;

internal class AcceptInvitationCommandHandler(INoserFitnessDbContext dbContext, ISender mediator)
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

        if (course.MaxAttendees >= course.Attendees.Count)
        {
            invitation.State = InvitationState.Expired;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
