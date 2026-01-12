using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Noser_Fitness.Domain;

namespace Noser_Fitness.Infrastructure.Configurations;

internal class MemberConfiguration : EntityConfiguration<Member>
{
    public override void Configure(EntityTypeBuilder<Member> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Email).HasMaxLength(50);
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.Firstname).HasMaxLength(30);
        builder.Property(x => x.Lastname).HasMaxLength(30);
    }
}
