using MediatR;

namespace EcoTrails.Shared.Features.Home.Shared;

public record GetTrailsRequest : IRequest<GetTrailsRequest.Response>
{
    public const string RouteTemplate = "/api/trails";

    public record Trail(
        int Id, 
        string Name, 
        string? Image, 
        string Location, 
        int TimeInMinutes, 
        int Length, 
        string Description,
        string Owner,
        List<Waypoint> Waypoints,
        List<RouteInstruction> RouteInstructions);

    public record Waypoint(decimal Latitude, decimal Longitude);
    public record RouteInstruction(int Stage, string Description);
    public record Response(IEnumerable<Trail> Trails);
}
