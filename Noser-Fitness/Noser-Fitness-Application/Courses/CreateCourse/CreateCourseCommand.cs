using Mediator;
using Noser_Fitness.Domain;

namespace Noser_Fitness_Application.Courses.CreateCourse;

public record CreateCourseCommand(
    Guid MemeberId,
    CourseType CourseType,
    DateTime ScheduledAtUtc,
    string Name,
    string? Location,
    int? MaximumNumberOfAttendees,
    int? InvitationsValidBeforeInHours
) : IRequest;
