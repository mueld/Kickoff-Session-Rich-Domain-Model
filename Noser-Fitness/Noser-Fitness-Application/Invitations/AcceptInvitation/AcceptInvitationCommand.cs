using Mediator;

namespace Noser_Fitness_Application.Invitations.AcceptInvitation;

public record AcceptInvitationCommand(Guid CourseId, Guid InvitationId) : ICommand;
