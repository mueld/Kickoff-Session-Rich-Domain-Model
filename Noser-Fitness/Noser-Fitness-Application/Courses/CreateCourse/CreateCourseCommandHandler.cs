using Mediator;
using Microsoft.EntityFrameworkCore;
using Noser_Fitness.Domain;
using Noser_Fitness.Domain.Abstractions;

namespace Noser_Fitness_Application.Courses.CreateCourse;

public class CreateCourseCommandHandler(INoserFitnessDbContext dbContext) : ICommandHandler<CreateCourseCommand>
{
    private readonly INoserFitnessDbContext _dbContext = dbContext;

    public async ValueTask<Unit> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
    {
        var member =
            await _dbContext.Members.SingleAsync(x => x.Id == command.MemeberId, cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Member not found");

        var course = Course.Create(
            command.Name,
            member.Id,
            command.CourseType,
            command.ScheduledAtUtc,
            command.MaximumNumberOfAttendees,
            command.Location,
            command.InvitationsValidBeforeInHours
        );

        _dbContext.Courses.Add(course);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
