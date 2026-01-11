using Mediator;
using Microsoft.EntityFrameworkCore;
using Noser_Fitness_Application.Abstractions;
using Noser_Fitness.Domain;
using Noser_Fitness.Domain.Abstractions;
using Noser_Fitness.Domain.Events;

namespace Noser_Fitness_Application.Invitations.SendInvitation;

public class SendInviationCommandHandler(INoserFitnessDbContext dbContext, ISender mediator, IEmailService emailService)
    : ICommandHandler<SendInvitationCommand>
{
    private readonly INoserFitnessDbContext _dbContext = dbContext;
    private readonly ISender _mediator = mediator;
    private readonly IEmailService _emailService = emailService;

    public async ValueTask<Unit> Handle(SendInvitationCommand command, CancellationToken cancellationToken)
    {
        var course =
            await _dbContext.Courses.SingleAsync(x => x.Id == command.CourseId, cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Course not found.");

        var member =
            await _dbContext.Members.SingleAsync(x => x.Id == command.MemberId, cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Member not found.");

        if (course.CreatedByMemberId == command.MemberId)
            throw new InvalidOperationException("Can't send invitation to the creator.");

        if (course.ScheduledAtUtc <= DateTime.UtcNow)
            throw new InvalidOperationException($"Can't send invitation for course in the past.");

        var invitation = new Invitation
        {
            Id = Guid.NewGuid(),
            CourseId = course.Id,
            MemberId = command.MemberId,
            ModifiedAtUtc = DateTime.UtcNow,
            State = InvitationState.Send,
        };
        course.Invitations.Add(invitation);
        await _dbContext.SaveChangesAsync(cancellationToken);

        await _emailService.SendEmail(member, course);

        var domainEvent = new InvitationSentDomainEvent(course.Id, member.Id);
        await _mediator.Send(domainEvent, cancellationToken);

        return Unit.Value;
    }
}
