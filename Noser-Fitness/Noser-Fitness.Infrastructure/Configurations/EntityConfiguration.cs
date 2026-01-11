using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Noser_Fitness.Domain;

namespace Noser_Fitness.Infrastructure.Configurations;

internal class EntityConfiguration : IEntityTypeConfiguration<Entity>
{
    public void Configure(EntityTypeBuilder<Entity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Ignore(nameof(Entity.DomainEvents));
    }
}
