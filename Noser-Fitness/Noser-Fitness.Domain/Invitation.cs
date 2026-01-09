namespace Noser_Fitness.Domain;

public class Invitation
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public Guid CourseId { get; set; }

    public InvitationState State { get; set; }
    public DateTime ModifiedAtUtc { get; set; }
}
