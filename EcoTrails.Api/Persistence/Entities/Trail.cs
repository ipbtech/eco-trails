using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoTrails.Api.Persistence.Entities;

public class Trail
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int TimeInMinutes { get; set; }
    public string TimeFormatted => $"{TimeInMinutes / 60}h {TimeInMinutes % 60}m";
    public int Length { get; set; }
    public IEnumerable<RouteInstruction> Route { get; set; } = new List<RouteInstruction>();
}

public class TrailDbConfig : IEntityTypeConfiguration<Trail>
{
    public void Configure(EntityTypeBuilder<Trail> builder)
    {
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Location).IsRequired();
        builder.Property(x => x.TimeInMinutes).IsRequired();
        builder.Property(x => x.Length).IsRequired();
    }
}
