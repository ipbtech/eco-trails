﻿using MediatR;

namespace EcoTrails.Shared.Features.ManageTrails.EditTrail;

public record GetTrailRequest(int TrailId) : IRequest<GetTrailRequest.Response>
{
    public const string RouteTemplate = "/api/trails/{trailId}";

    public record Response(Trail Trail);
    public record Trail(
        int Id, 
        string Name, 
        string Location, 
        string? Image, 
        int TimeInMinutes, 
        int Length, 
        string Description, 
        IEnumerable<RouteInstruction> RouteInstructions,
        IEnumerable<Waypoint> Waypoints);
    public record RouteInstruction(int Id, int Stage, string Description);
    public record Waypoint(decimal Latitude, decimal Longitude);
}
