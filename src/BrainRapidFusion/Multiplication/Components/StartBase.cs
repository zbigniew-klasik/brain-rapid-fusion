using Microsoft.AspNetCore.Components;

namespace BrainRapidFusion.Multiplication.Components
{
    public class StartBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public void StartGame()
        {
            NavigationManager.NavigateTo("multiplication/game");
        }
    }
}
