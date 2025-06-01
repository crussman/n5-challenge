using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using N5.Challenge.Domain.Entities;

namespace N5.Challenge.Infrastructure.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.EmployeeFirstName).IsRequired();
        builder.Property(p => p.EmployeeLastName).IsRequired();
        builder.Property(p => p.PermissionDate).IsRequired();

        builder.HasOne(p => p.PermissionType)
               .WithMany(pt => pt.Permissions)
               .HasForeignKey(p => p.PermissionTypeId)
               .IsRequired();
    }
}