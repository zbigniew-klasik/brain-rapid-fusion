using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainRapidFusion.Multiplication.Components
{
    public class FinishBase : ComponentBase
    {
        [Inject]
        public IContextProvider ContextProvider { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public void StartAnotherGame()
        {
            NavigationManager.NavigateTo("multiplication/game");
        }
    }
}
