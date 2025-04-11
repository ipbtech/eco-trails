using EcoTrails.Shared.Features.ManageTrails.Shared;

namespace EcoTrails.Client.State;

public class AppState
{
    public NewTrailState NewTrailState { get; } = new();
}