namespace Noser_Fitness.Domain;

public class Invitation : Entity
{
    private Invitation() { }

    private Invitation(Guid memberId, Guid courseId)
    {
        MemberId = memberId;
        CourseId = courseId;
        State = InvitationState.Created;
        ModifiedAtUtc = DateTime.UtcNow;
    }

    public Guid MemberId { get; init; }
    public Guid CourseId { get; init; }

    public InvitationState State { get; private set; }
    public DateTime ModifiedAtUtc { get; private set; }

    public void IsExpired()
    {
        State = InvitationState.Expired;
        ModifiedAtUtc = DateTime.UtcNow;
    }

    public void IsAccepted()
    {
        State = InvitationState.Accepted;
        ModifiedAtUtc = DateTime.UtcNow;
    }

    public void IsSend()
    {
        State = InvitationState.Send;
        ModifiedAtUtc = DateTime.UtcNow;
    }

    public static Invitation Create(Guid MemberId, Guid CourseId)
    {
        return new Invitation(MemberId, CourseId);
    }
}
