namespace Noser_Fitness.Domain;

public class Attendee : Entity
{
    public Guid CourseId { get; init; }
    public Guid MemberId { get; init; }
}
