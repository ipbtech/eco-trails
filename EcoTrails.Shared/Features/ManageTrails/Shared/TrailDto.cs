﻿using FluentValidation;

namespace EcoTrails.Shared.Features.ManageTrails.Shared;

public class TrailDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int TimeInMinutes { get; set; }
    public int Length { get; set; }
    public List<RouteInstruction> Route { get; set; } = new List<RouteInstruction>();
    public List<WaypointDto> Waypoints { get; set; } = new List<WaypointDto>();
    public string? Image { get; set; }
    public ImageAction ImageAction { get; set; }
}

public enum ImageAction
{
    None,
    Add,
    Remove
}

public class TrailValidator : AbstractValidator<TrailDto>
{
    public TrailValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter a name");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter a description");
        RuleFor(x => x.Location).NotEmpty().WithMessage("Please enter a location");
        RuleFor(x => x.Length).GreaterThan(0).WithMessage("Please enter a length");
        RuleFor(x => x.TimeInMinutes).GreaterThan(0).WithMessage("Please enter a time");
        RuleFor(x => x.Waypoints).NotEmpty().WithMessage("Please add a waypoint");
        RuleForEach(x => x.Route).SetValidator(new RouteInstructionValidator());
    }
}


public class RouteInstruction
{
    public int Stage { get; set; }
    public string Description { get; set; } = string.Empty;
}

public class RouteInstructionValidator : AbstractValidator<RouteInstruction>
{
    public RouteInstructionValidator()
    {
        RuleFor(x => x.Stage).NotEmpty().WithMessage("Please enter a stage");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter a description");
    }
}

public record WaypointDto(
    decimal Latitude,
    decimal Longitude);