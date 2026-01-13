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

        var members = new List<Member>
        {
            new Member
            {
                Id = Guid.Parse("7c9c9f51-6b2c-4f89-b5d4-1b4c8e2a1a01"),
                Firstname = "John",
                Lastname = "Doe",
                Email = "john.doe@example.com",
            },
            new Member
            {
                Id = Guid.Parse("a3f8d7c4-9e21-4c1f-b6e2-8d3c5b7a4f02"),
                Firstname = "Jane",
                Lastname = "Smith",
                Email = "jane.smith@example.com",
            },
            new Member
            {
                Id = Guid.Parse("e1b4c9a2-6d5f-4a88-9e3b-2f7c8d1a0b03"),
                Firstname = "Alex",
                Lastname = "Miller",
                Email = "alex.miller@example.com",
            },
        };
        builder.HasData(members);
    }
}
