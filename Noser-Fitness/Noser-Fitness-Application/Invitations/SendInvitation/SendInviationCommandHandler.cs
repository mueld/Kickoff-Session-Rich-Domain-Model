using Mediator;
using Microsoft.EntityFrameworkCore;
using Noser_Fitness_Application.Abstractions;
using Noser_Fitness.Domain.Abstractions;

namespace Noser_Fitness_Application.Invitations.SendInvitation;

public class SendInviationCommandHandler(INoserFitnessDbContext dbContext, IEmailService emailService)
    : ICommandHandler<SendInvitationCommand>
{
    private readonly INoserFitnessDbContext _dbContext = dbContext;
    private readonly IEmailService _emailService = emailService;

    public async ValueTask<Unit> Handle(SendInvitationCommand command, CancellationToken cancellationToken)
    {
        var course =
            await _dbContext.Courses.SingleAsync(x => x.Id == command.CourseId, cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Course not found.");

        var member =
            await _dbContext.Members.SingleAsync(x => x.Id == command.MemberId, cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Member not found.");

        var invitation = course.SendInvitation(command.MemberId);
        await _emailService.SendEmail(member, course);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
