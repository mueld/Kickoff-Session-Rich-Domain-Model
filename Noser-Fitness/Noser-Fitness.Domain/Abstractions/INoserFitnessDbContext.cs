using Microsoft.EntityFrameworkCore;

namespace Noser_Fitness.Domain.Abstractions;

public interface INoserFitnessDbContext
{
    DbSet<Member> Members { get; }

    //DbSet<Attendee> Attendees { get; }
    //DbSet<Invitation> Invitations { get; }
    DbSet<Course> Courses { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
