using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using N5.Challenge.Domain.Entities;

namespace N5.Challenge.Infrastructure.Configurations;

public class PermissionTypeConfiguration : IEntityTypeConfiguration<PermissionType>
{
    public void Configure(EntityTypeBuilder<PermissionType> builder)
    {
        builder.HasKey(pt => pt.Id);
        builder.Property(pt => pt.Description).IsRequired();

        builder.HasData(
            new PermissionType { Id = 1, Description = "Read" },
            new PermissionType { Id = 2, Description = "Write" }
        );
    }
}
