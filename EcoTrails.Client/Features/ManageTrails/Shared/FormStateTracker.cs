using EcoTrails.Client.State;
using EcoTrails.Shared.Features.ManageTrails.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;


namespace EcoTrails.Client.Features.ManageTrails.Shared;

public class FormStateTracker : ComponentBase
{
    [Inject]
    public AppState AppState { get; set; }

    [CascadingParameter]
    private EditContext CascadedEditContext { get; set; }

    protected override void OnInitialized()
    {
        if (CascadedEditContext is null)
        {
            throw new InvalidOperationException(
                $"{nameof(FormStateTracker)} requires a cascading parameter of type {nameof(EditContext)}");
        }

        CascadedEditContext.OnFieldChanged += CascadedEditContext_OnFieldChanged;
    }

    private void CascadedEditContext_OnFieldChanged(object sender, FieldChangedEventArgs e)
    {
        var trail = e.FieldIdentifier.Model as TrailDto;
        if (trail?.Id == 0)
        {
            AppState.NewTrailState.SaveTrail(trail);
        }
    }
}