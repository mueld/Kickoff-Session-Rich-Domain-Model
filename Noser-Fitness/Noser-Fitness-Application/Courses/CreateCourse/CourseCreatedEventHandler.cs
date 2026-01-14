using Mediator;
using Microsoft.EntityFrameworkCore;
using Noser_Fitness.Domain.Abstractions;
using Noser_Fitness.Domain.Events;

namespace Noser_Fitness_Application.Courses.CreateCourse;

public class CourseCreatedEventHandler(INoserFitnessDbContext dbContext)
    : INotificationHandler<CourseCreatedDomainEvent>
{
    public async ValueTask Handle(CourseCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var course = await dbContext.Courses.SingleAsync(x => x.Id == notification.CourseId, cancellationToken);
        throw new Exception("test");
        return;
    }
}
