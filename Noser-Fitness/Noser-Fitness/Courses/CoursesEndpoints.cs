using Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noser_Fitness_Application.Courses.CreateCourse;
using Noser_Fitness.Domain.Abstractions;

namespace Noser_Fitness.Courses;

public static class CoursesEndpoints
{
    public static void MapCourseEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("courses/all", OnGetAllCourses);
        app.MapPut("courses/create", OnCreateCourse);
    }

    private static async Task<IResult> OnGetAllCourses([FromServices] INoserFitnessDbContext dbContext)
    {
        var courses = await dbContext.Courses.Include(x => x.Invitations).Include(x => x.Attendees).ToListAsync();
        return Results.Ok(courses);
    }

    private static async Task<IResult> OnCreateCourse(
        [FromBody] CreateCourseCommand command,
        [FromServices] ISender mediator
    )
    {
        await mediator.Send(command);
        return Results.Ok();
    }
}
