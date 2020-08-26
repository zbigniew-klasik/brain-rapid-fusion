using BrainRapidFusion.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainRapidFusion.Multiplication.Components
{
    public class SummaryBase : ComponentBase
    {
        [Inject]
        public IContextProvider ContextProvider { get; set; }

        [Inject]
        public IScoreRepository ScoreRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public IEnumerable<int> BestScores { get; private set; }

        public CssClass PulpitCssClass { get; set; } = new CssClass("pulpit");

        protected override async Task OnParametersSetAsync()
        {
            BestScores = new List<int> { 123, 23, 45, 0, 0 }; // await ScoreRepository.GetBestScores();
            base.OnParametersSet();
        }

        public void StartAnotherGame()
        {
            NavigationManager.NavigateTo("multiplication/game");
        }
    }
}
