using Microsoft.EntityFrameworkCore;
using Trackem.ERT.Infra.EFCore.Entities;

namespace Trackem.ERT.Infra.EFCore;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ClaimUIAccess> ClaimUIAccess { get; set; }

    public DbSet<Claims> Claims { get; set; }

    public DbSet<Material> Materials { get; set; }

    public DbSet<Schedule> Schedules { get; set; }

    public DbSet<Company> Companies { get; set; }

}