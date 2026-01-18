using Mediator;
using Microsoft.EntityFrameworkCore;
using Noser_Fitness.Domain;
using Noser_Fitness.Domain.Abstractions;
using Noser_Fitness.Domain.Events;

namespace Noser_Fitness_Application.Courses.CreateCourse;

public class CreateCourseCommandHandler(INoserFitnessDbContext dbContext, IMediator mediator)
    : ICommandHandler<CreateCourseCommand>
{
    private readonly INoserFitnessDbContext _dbContext = dbContext;
    private readonly IMediator _mediator = mediator;

    public async ValueTask<Unit> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
    {
        var member =
            await _dbContext.Members.SingleAsync(x => x.Id == command.MemeberId, cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Member not found");

        var course = new Course
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            CreatedByMemberId = member.Id,
            Location = command.Location,
            CourseType = command.CourseType,
            CreatedAtUtc = DateTime.UtcNow,
            ScheduledAtUtc = command.ScheduledAtUtc,
            MaxAttendees = command.MaximumNumberOfAttendees ?? 10,
            InvitationsValidBeforeInHours = command.InvitationsValidBeforeInHours,
        };

        switch (command.CourseType)
        {
            case CourseType.Standard:
                course.MaxAttendees = 20;
                break;

            case CourseType.Trial:
                course.MaxAttendees = 1;
                break;

            case CourseType.Indoorcycling:
                course.MaxAttendees = 50;
                break;
        }

        _dbContext.Courses.Add(course);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var domainEvent = new CourseCreatedDomainEvent(course.Id);
        await _mediator.Send(domainEvent);

        return Unit.Value;
    }
}
