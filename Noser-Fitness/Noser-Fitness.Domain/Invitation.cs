namespace Noser_Fitness.Domain;

public class Invitation : Entity
{
    private Invitation() { }

    private Invitation(Guid memberId, Guid courseId)
    {
        MemberId = memberId;
        CourseId = courseId;
        State = InvitationStatus.Created();
    }

    public Guid MemberId { get; init; }
    public Guid CourseId { get; init; }

    public InvitationStatus State { get; private set; }

    public void IsExpired()
    {
        State = State.Expired();
    }

    public void IsAccepted()
    {
        State = State.Accepted();
    }

    public void IsSend()
    {
        State = State.Sent();
    }

    public static Invitation Create(Guid memberId, Guid courseId)
    {
        return new Invitation(memberId, courseId);
    }
}
