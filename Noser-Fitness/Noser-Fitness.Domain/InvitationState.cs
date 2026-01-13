namespace Noser_Fitness.Domain;

public sealed record InvitationStatus
{
    public InvitationState Value { get; private init; }
    public DateTime ModifiedAtUtc { get; private init; }

    private InvitationStatus() { }

    private InvitationStatus(InvitationState value, DateTime modifiedAtUtc)
    {
        Value = value;
        ModifiedAtUtc = modifiedAtUtc;
    }

    public static InvitationStatus Created() => new(InvitationState.Created, DateTime.UtcNow);

    public InvitationStatus Expired() => TransitionTo(InvitationState.Expired);

    public InvitationStatus Accepted() => TransitionTo(InvitationState.Accepted);

    public InvitationStatus Sent() => TransitionTo(InvitationState.Send);

    private InvitationStatus TransitionTo(InvitationState next)
    {
        return new InvitationStatus(next, DateTime.UtcNow);
    }
}

public enum InvitationState
{
    Created,
    Send,
    Accepted,
    Expired,
}
