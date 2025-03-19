using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EcoTrails.Api.Persistence.Entities;

public class RouteInstruction
{
    public int Id { get; set; }
    public int Stage { get; set; }
    public string Description { get; set; } = string.Empty;

    public int TrailId { get; set; }

#nullable disable
    public Trail Trail { get; set; }
}

public class RouteInstructionDbConfig : IEntityTypeConfiguration<RouteInstruction>
{
    public void Configure(EntityTypeBuilder<RouteInstruction> builder)
    {
        builder.Property(x => x.Stage).IsRequired();
        builder.Property(x => x.Description).IsRequired();
    }
}
