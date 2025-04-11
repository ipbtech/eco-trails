using EcoTrails.ComponentLibrary.Map;

namespace EcoTrails.Client.Features.Shared;

public class Trail
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int TimeInMinutes { get; set; }
    public string TimeFormatted => $"{TimeInMinutes / 60}h {TimeInMinutes % 60}m";
    public int Length { get; set; }
    public IEnumerable<RouteInstruction> Route { get; set; } = new List<RouteInstruction>();
    public IEnumerable<LatLong> Waypoints { get; set; } = new List<LatLong>();
}

public class RouteInstruction
{
    public int Stage { get; set; }
    public string Description { get; set; } = string.Empty;
}

