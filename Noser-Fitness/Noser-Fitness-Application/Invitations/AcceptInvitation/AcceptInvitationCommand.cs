using Mediator;

namespace Noser_Fitness_Application.Invitations.AcceptInvitation;

public record AcceptInvitationCommand(Guid InvitationId) : ICommand;
