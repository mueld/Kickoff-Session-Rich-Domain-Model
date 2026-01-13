using Noser_Fitness.Domain.Events;

namespace Noser_Fitness.Domain;

public class Course : Aggregate
{
    private readonly List<Attendee> _attendees = [];
    private readonly List<Invitation> _invitations = [];

    public Guid CreatedByMemberId { get; init; }

    public string Name { get; init; } = string.Empty;
    public string? Location { get; set; }

    public CourseType CourseType { get; set; } = CourseType.Standard;
    public DateTime ScheduledAtUtc { get; init; }
    public DateTime CreatedAtUtc { get; init; } = DateTime.UtcNow;

    public int MaxAttendees { get; init; }
    public int? InvitationsValidBeforeInHours { get; init; }

    public IReadOnlyCollection<Attendee> Attendees => _attendees.AsReadOnly();
    public IReadOnlyCollection<Invitation> Invitations => _invitations.AsReadOnly();

    public Invitation? SendInvitation(Guid memberId)
    {
        if (CreatedByMemberId == memberId)
            throw new InvalidOperationException("Can't send invitation to the creator.");

        if (ScheduledAtUtc <= DateTime.UtcNow)
            throw new InvalidOperationException($"Can't send invitation for course in the past.");

        var invitation = Invitation.Create(memberId, Id);
        _invitations.Add(invitation);
        invitation.IsSend();

        Raise(new InvitationSentDomainEvent(Id, memberId));
        return invitation;
    }

    public Attendee? AcceptInvitation(Guid invitationId)
    {
        var invitation = _invitations.SingleOrDefault(x => x.Id == invitationId);
        if (invitation is null)
            return null;

        var isExpired =
            InvitationsValidBeforeInHours.HasValue
            && DateTime.UtcNow - ScheduledAtUtc > TimeSpan.FromHours(InvitationsValidBeforeInHours.Value);

        if (_attendees.Count >= MaxAttendees || isExpired)
        {
            invitation.IsExpired();
            return null;
        }

        invitation.IsAccepted();

        var attendee = new Attendee { CourseId = Id, MemberId = invitation.MemberId };
        _attendees.Add(attendee);

        Raise(new InvitationAcceptedDomainEvent(invitation.CourseId, invitation.MemberId));

        return attendee;
    }

    public static Course Create(
        string name,
        Guid memberId,
        CourseType courseType,
        DateTime scheduledAtUtc,
        int? maxAttendees,
        string? location,
        int? invitationsValidBeforeInHours
    )
    {
        maxAttendees ??= courseType switch
        {
            CourseType.Standard => 20,
            CourseType.Trial => 1,
            CourseType.Indoorcycling => 50,
            _ => 20,
        };

        var course = new Course
        {
            Id = Guid.NewGuid(),
            Name = name,
            CreatedByMemberId = memberId,
            Location = location,
            CourseType = courseType,
            CreatedAtUtc = DateTime.UtcNow,
            ScheduledAtUtc = scheduledAtUtc,
            MaxAttendees = maxAttendees.Value,
            InvitationsValidBeforeInHours = invitationsValidBeforeInHours,
        };

        course.Raise(new CourseCreatedDomainEvent(course.Id));
        return course;
    }
}
