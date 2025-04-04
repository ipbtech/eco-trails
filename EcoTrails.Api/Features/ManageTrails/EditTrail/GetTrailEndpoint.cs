﻿using Ardalis.ApiEndpoints;
using EcoTrails.Api.Persistence;
using EcoTrails.Shared.Features.ManageTrails.EditTrail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoTrails.Api.Features.ManageTrails.EditTrail;

public class GetTrailEndpoint : EndpointBaseAsync
    .WithRequest<int>
    .WithActionResult<GetTrailRequest.Response>
{
    private readonly EcoTrailsDbContext _context;

    public GetTrailEndpoint(EcoTrailsDbContext context)
    {
        _context = context;
    }

    [HttpGet(GetTrailRequest.RouteTemplate)]
    public override async Task<ActionResult<GetTrailRequest.Response>> HandleAsync(int trailId, CancellationToken cancellationToken = default)
    {
        var trail = await _context.Trails
            .Include(x => x.Route)
            .Include(x => x.Waypoints)
            .SingleOrDefaultAsync(x => x.Id == trailId, cancellationToken: cancellationToken);

        if (trail is null)
        {
            return BadRequest("Trail could not be found.");
        }

        var response = new GetTrailRequest.Response(new GetTrailRequest.Trail(trail.Id,
            trail.Name,
            trail.Location,
            trail.Image,
            trail.TimeInMinutes,
            trail.Length,
            trail.Description,
            trail.Route.Select(ri => new GetTrailRequest.RouteInstruction(ri.Id, ri.Stage, ri.Description)),
            trail.Waypoints.Select(wp => new GetTrailRequest.Waypoint(wp.Latitude, wp.Longitude))));

        return Ok(response);
    }
}
