namespace Noser_Fitness.Domain;

public class Course
{
    public Guid Id { get; set; }
    public Guid CreatedByMemberId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string? Location { get; set; }

    public CourseType CourseType { get; set; } = CourseType.Standard;
    public DateTime ScheduledAtUtc { get; set; }

    public int MaxAttendees { get; set; }
    public int? InvitationsValidBeforeInHours { get; set; }

    public List<Attendee> Attendees { get; set; } = new();
    public List<Invitation> Invitations { get; set; } = new();

    public DateTime CreatedAtUtc { get; set; }
}
