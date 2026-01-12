using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Noser_Fitness.Domain;

namespace Noser_Fitness.Infrastructure.Configurations;

internal class CourseConfiguration : EntityConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder) { }
}
