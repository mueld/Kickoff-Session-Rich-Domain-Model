using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Noser_Fitness.Domain;

namespace Noser_Fitness.Infrastructure.Configurations;

internal class CourseConfiguration : EntityConfiguration<Course>
{
    public override void Configure(EntityTypeBuilder<Course> builder)
    {
        base.Configure(builder);

        builder.HasMany(x => x.Invitations);
        builder.HasMany(x => x.Attendees);

        builder.Property(x => x.Name).HasMaxLength(50);
        builder.Property(x => x.Location).HasMaxLength(50);

        var courses = new List<Course>
        {
            new Course
            {
                Id = Guid.Parse("b7e2c4a9-3f6d-4c8e-9a1b-2d5f7c0e4a10"),
                CreatedByMemberId = Guid.Parse("7c9c9f51-6b2c-4f89-b5d4-1b4c8e2a1a01"),
                Name = "Morning Fitness",
                Location = "Main Gym",
                CourseType = CourseType.Standard,
                ScheduledAtUtc = new DateTime(2026, 03, 10, 08, 00, 00, DateTimeKind.Utc),
                CreatedAtUtc = new DateTime(2026, 01, 01, 00, 00, 00, DateTimeKind.Utc),
                MaxAttendees = 20,
                InvitationsValidBeforeInHours = 24,
            },
            new Course
            {
                Id = Guid.Parse("c2a9e5b1-7d3f-4b8c-9e6a-1f0d4c2b5e11"),
                CreatedByMemberId = Guid.Parse("a3f8d7c4-9e21-4c1f-b6e2-8d3c5b7a4f02"),
                Name = "Trial Workout",
                Location = "Studio A",
                CourseType = CourseType.Trial,
                ScheduledAtUtc = new DateTime(2026, 03, 10, 08, 00, 00, DateTimeKind.Utc),
                CreatedAtUtc = new DateTime(2026, 01, 01, 00, 00, 00, DateTimeKind.Utc),
                MaxAttendees = 1,
                InvitationsValidBeforeInHours = 12,
            },
            new Course
            {
                Id = Guid.Parse("f9a1c5e2-4b7d-4e6a-8c3f-0d1b2a9e7c12"),
                CreatedByMemberId = Guid.Parse("e1b4c9a2-6d5f-4a88-9e3b-2f7c8d1a0b03"),
                Name = "Indoor Cycling Blast",
                Location = "Cycling Room",
                CourseType = CourseType.Indoorcycling,
                ScheduledAtUtc = new DateTime(2026, 03, 10, 08, 00, 00, DateTimeKind.Utc),
                CreatedAtUtc = new DateTime(2026, 01, 01, 00, 00, 00, DateTimeKind.Utc),
                MaxAttendees = 50,
                InvitationsValidBeforeInHours = 48,
            },
        };

        builder.HasData(courses);
    }
}
