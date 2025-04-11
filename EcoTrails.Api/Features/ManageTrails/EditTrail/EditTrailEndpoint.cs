using Ardalis.ApiEndpoints;
using EcoTrails.Api.Persistence;
using EcoTrails.Api.Persistence.Entities;
using EcoTrails.Shared.Features.ManageTrails.EditTrail;
using EcoTrails.Shared.Features.ManageTrails.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoTrails.Api.Features.ManageTrails.EditTrail;

public class EditTrailEndpoint : EndpointBaseAsync
    .WithRequest<EditTrailRequest>
    .WithActionResult<bool>
{
    private readonly EcoTrailsDbContext _database;

    public EditTrailEndpoint(EcoTrailsDbContext database)
    {
        _database = database;
    }

    [Authorize]
    [HttpPut(EditTrailRequest.RouteTemplate)]
    public override async Task<ActionResult<bool>> HandleAsync(EditTrailRequest request, CancellationToken cancellationToken = default)
    {
        var trail = await _database.Trails
            .Include(x => x.Route)
            .Include(x => x.Waypoints)
            .SingleOrDefaultAsync(x => x.Id == request.Trail.Id, cancellationToken: cancellationToken);

        if (trail is null)
        {
            return BadRequest("Trail could not be found.");
        }

        if (!trail.Owner.Equals(HttpContext.User.Identity!.Name, StringComparison.OrdinalIgnoreCase) &&
            !HttpContext.User.IsInRole("Administrator"))
        {
            return Unauthorized();
        }

        trail.Name = request.Trail.Name;
        trail.Description = request.Trail.Description;
        trail.Location = request.Trail.Location;
        trail.TimeInMinutes = request.Trail.TimeInMinutes;
        trail.Length = request.Trail.Length;
        trail.Route = request.Trail.Route.Select(ri => new Persistence.Entities.RouteInstruction
        {
            Stage = ri.Stage,
            Description = ri.Description,
            Trail = trail
        }).ToList();
        trail.Waypoints = request.Trail.Waypoints.Select(wp => new Waypoint()
        {
            Latitude = wp.Latitude,
            Longitude = wp.Longitude,
        }).ToList();

        if (request.Trail.ImageAction == ImageAction.Remove)
        {
            System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "Images", trail.Image!));
            trail.Image = null;
        }

        await _database.SaveChangesAsync(cancellationToken);

        return Ok(true);
    }
}
