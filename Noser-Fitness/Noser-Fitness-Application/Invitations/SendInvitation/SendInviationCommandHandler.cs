using Mediator;
using Noser_Fitness.Domain.Abstractions;

namespace Noser_Fitness_Application.Invitations.SendInvitation;

internal class SendInviationCommandHandler(INoserFitnessDbContext dbContext, ISender mediator)
    : ICommandHandler<SendInvitationCommand>
{
    private readonly INoserFitnessDbContext _dbContext = dbContext;
    private readonly ISender _mediator = mediator;

    public ValueTask<Unit> Handle(SendInvitationCommand command, CancellationToken cancellationToken) { }
}
