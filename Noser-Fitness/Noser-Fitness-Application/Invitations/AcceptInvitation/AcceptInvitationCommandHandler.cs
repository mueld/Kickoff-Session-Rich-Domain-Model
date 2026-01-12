using Mediator;
using Microsoft.EntityFrameworkCore;
using Noser_Fitness.Domain;
using Noser_Fitness.Domain.Abstractions;
using Noser_Fitness.Domain.Events;

namespace Noser_Fitness_Application.Invitations.AcceptInvitation;

public class AcceptInvitationCommandHandler(INoserFitnessDbContext dbContext) : ICommandHandler<AcceptInvitationCommand>
{
    private readonly INoserFitnessDbContext _dbContext = dbContext;

    public async ValueTask<Unit> Handle(AcceptInvitationCommand command, CancellationToken cancellationToken)
    {
        var invitation =
            await _dbContext.Invitations.SingleAsync(x => x.Id == command.InvitationId)
            ?? throw new InvalidOperationException("Invitation not found");

        var course =
            await _dbContext.Courses.SingleAsync(x => x.Id == invitation.CourseId)
            ?? throw new InvalidOperationException("Course not found");

        course.AcceptInvitation(invitation);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
