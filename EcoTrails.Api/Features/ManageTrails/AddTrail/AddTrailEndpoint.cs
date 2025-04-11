using Ardalis.ApiEndpoints;
using EcoTrails.Api.Persistence;
using EcoTrails.Api.Persistence.Entities;
using EcoTrails.Shared.Features.ManageTrails.AddTrail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoTrails.Api.Features.ManageTrails.AddTrail;

public class AddTrailEndpoint : EndpointBaseAsync
    .WithRequest<AddTrailRequest>
    .WithActionResult<int>
{
    private readonly EcoTrailsDbContext _database;

    public AddTrailEndpoint(EcoTrailsDbContext database)
    {
        _database = database;
    }

    [Authorize]
    [HttpPost(AddTrailRequest.RouteTemplate)]
    public override async Task<ActionResult<int>> HandleAsync(AddTrailRequest request, CancellationToken cancellationToken = default)
    {
        var trail = new Trail
        {
            Name = request.Trail.Name,
            Description = request.Trail.Description,
            Location = request.Trail.Location,
            TimeInMinutes = 0,
            Length = request.Trail.Length,
            Route = request.Trail.Route.Select(x => new RouteInstruction()
            {
                Stage = x.Stage,
                Description = x.Description,
            }).ToList(),
            Waypoints = request.Trail.Waypoints.Select(wp => new Waypoint()
            {
                Latitude = wp.Latitude,
                Longitude = wp.Longitude,
            }).ToList()
        };

        await _database.Trails.AddAsync(trail, cancellationToken);
        await _database.SaveChangesAsync(cancellationToken);

        return Ok(trail.Id);
    }
}
