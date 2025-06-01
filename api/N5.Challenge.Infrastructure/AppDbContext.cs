using Microsoft.EntityFrameworkCore;

using N5.Challenge.Domain.Entities;
using N5.Challenge.Infrastructure.Configurations;

namespace N5.Challenge.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<PermissionType> PermissionTypes => Set<PermissionType>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PermissionTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
    }
}