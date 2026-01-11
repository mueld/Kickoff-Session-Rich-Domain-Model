using Microsoft.EntityFrameworkCore;
using Noser_Fitness.Domain;
using Noser_Fitness.Domain.Abstractions;

namespace Noser_Fitness.Infrastructure;

internal class NoserFitnessDbContext(DbContextOptions<NoserFitnessDbContext> options)
    : DbContext(options),
        INoserFitnessDbContext
{
    public DbSet<Member> Members { get; set; }

    public DbSet<Attendee> Attendees { get; set; }

    public DbSet<Invitation> Invitations { get; set; }

    public DbSet<Course> Courses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NoserFitnessDbContext).Assembly);
        BuildIdConvention(modelBuilder);
    }

    private static void BuildIdConvention(ModelBuilder modelBuilder)
    {
        foreach (
            var entityType in modelBuilder.Model.GetEntityTypes().Where(t => typeof(Entity).IsAssignableFrom(t.ClrType))
        )
        {
            modelBuilder.Entity(entityType.ClrType).HasKey(nameof(Entity.Id));
            modelBuilder.Entity(entityType.ClrType).Property(nameof(Entity.Id)).ValueGeneratedNever();
        }
    }
}
