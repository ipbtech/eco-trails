using EcoTrails.Api.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcoTrails.Api.Persistence;

public class EcoTrailsDbContext : DbContext
{
    public DbSet<Trail> Trails => Set<Trail>();
    public DbSet<RouteInstruction> RouteInstructions => Set<RouteInstruction>();
    public DbSet<Waypoint> Waypoints => Set<Waypoint>();

    public EcoTrailsDbContext(DbContextOptions<EcoTrailsDbContext> options) : base(options) 
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new TrailDbConfig());
        modelBuilder.ApplyConfiguration(new RouteInstructionDbConfig());
        modelBuilder.ApplyConfiguration(new WaypointConfig());
    }
}

