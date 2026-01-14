using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Noser_Fitness.Domain.Abstractions;

public interface INoserFitnessDbContext
{
    DbSet<Member> Members { get; }
    DbSet<Course> Courses { get; }

    DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
