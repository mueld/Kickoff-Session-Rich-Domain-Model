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
    }
}
