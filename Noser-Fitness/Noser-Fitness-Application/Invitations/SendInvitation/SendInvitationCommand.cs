using Mediator;

namespace Noser_Fitness_Application.Invitations.SendInvitation;

public record SendInvitationCommand(Guid CourseId, Guid MemberId) : ICommand;
