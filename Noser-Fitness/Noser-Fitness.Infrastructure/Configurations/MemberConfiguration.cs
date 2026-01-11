using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Noser_Fitness.Domain;

namespace Noser_Fitness.Infrastructure.Configurations;

internal class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.Property(x => x.Email).HasMaxLength(50);
        builder.Property(x => x.Firstname).HasMaxLength(30);
        builder.Property(x => x.Lastname).HasMaxLength(30);
    }
}
