using Mediator;
using Microsoft.EntityFrameworkCore;
using Noser_Fitness.Domain.Abstractions;

namespace Noser_Fitness_Application.Invitations.AcceptInvitation;

public class AcceptInvitationCommandHandler(INoserFitnessDbContext dbContext) : ICommandHandler<AcceptInvitationCommand>
{
    private readonly INoserFitnessDbContext _dbContext = dbContext;

    public async ValueTask<Unit> Handle(AcceptInvitationCommand command, CancellationToken cancellationToken)
    {
        var course =
            await _dbContext.Courses.SingleAsync(x => x.Id == command.CourseId)
            ?? throw new InvalidOperationException("Course not found");

        course.AcceptInvitation(command.InvitationId);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
