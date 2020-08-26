using BrainRapidFusion.Shared;
using Microsoft.AspNetCore.Components;

namespace BrainRapidFusion.Multiplication.Components
{
    public class StartBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public CssClass PulpitCssClass { get; set; } = new CssClass("pulpit");

        public void StartGame()
        {
            NavigationManager.NavigateTo("multiplication/game");
        }
    }
}
