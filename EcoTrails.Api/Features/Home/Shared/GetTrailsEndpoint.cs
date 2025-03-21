using Ardalis.ApiEndpoints;
using EcoTrails.Api.Persistence;
using EcoTrails.Shared.Features.Home.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoTrails.Api.Features.Home.Shared;

public class GetTrailsEndpoint : EndpointBaseAsync
    .WithRequest<int>
    .WithActionResult<GetTrailsRequest.Response>
{
    private readonly EcoTrailsDbContext _context;

    public GetTrailsEndpoint(EcoTrailsDbContext context)
    {
        _context = context;
    }

    [HttpGet(GetTrailsRequest.RouteTemplate)]
    public override async Task<ActionResult<GetTrailsRequest.Response>> HandleAsync(int trailId, CancellationToken cancellationToken = default)
    {
        var trails = await _context.Trails.Include(x => x.Route).ToListAsync(cancellationToken);

        var response = new GetTrailsRequest.Response(trails.Select(trail => new GetTrailsRequest.Trail(
            trail.Id,
            trail.Name,
            trail.Image,
            trail.Location,
            trail.TimeInMinutes,
            trail.Length,
            trail.Description
        )));

        return Ok(response);
    }
}
