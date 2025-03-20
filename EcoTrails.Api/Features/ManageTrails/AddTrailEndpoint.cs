using EcoTrails.Api.Persistence;
using EcoTrails.Api.Persistence.Entities;
using EcoTrails.Shared.Features.ManageTrails;
using Microsoft.AspNetCore.Mvc;

namespace EcoTrails.Api.Features.ManageTrails;

public class AddTrailEndpoint : Ardalis.ApiEndpoints.EndpointBaseAsync.WithRequest<AddTrailRequest>.WithActionResult<int>
{
    private readonly EcoTrailsDbContext _database;

    public AddTrailEndpoint(EcoTrailsDbContext database)
    {
        _database = database;
    }

    [HttpPost(AddTrailRequest.RouteTemplate)]
    public override async Task<ActionResult<int>> HandleAsync(AddTrailRequest request, CancellationToken cancellationToken = default)
    {
        var trail = new Trail
        {
            Name = request.Trail.Name,
            Description = request.Trail.Description,
            Location = request.Trail.Location,
            TimeInMinutes = 0,
            Length = request.Trail.Length
        };

        await _database.Trails.AddAsync(trail, cancellationToken);

        var routeInstructions = request.Trail.Route.Select(x => new Persistence.Entities.RouteInstruction
        {
            Stage = x.Stage,
            Description = x.Description,
            Trail = trail
        });

        await _database.RouteInstructions.AddRangeAsync(routeInstructions, cancellationToken);
        await _database.SaveChangesAsync(cancellationToken);

        return Ok(trail.Id);
    }
}
