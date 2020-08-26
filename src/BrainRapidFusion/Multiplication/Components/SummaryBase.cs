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

        public IEnumerable<int> BestScores { get; private set; } = new List<int> { 0, 0, 0, 0, 0 };

        public CssClass PulpitCssClass { get; set; } = new CssClass("pulpit");

        protected override void OnParametersSet()
        {
            _ = LoadScores();
            base.OnParametersSet();
        }

        private async Task LoadScores()
        {
            BestScores = await ScoreRepository.GetBestScores();
            this.StateHasChanged();
        }

        public void StartAnotherGame()
        {
            NavigationManager.NavigateTo("multiplication/game");
        }
    }
}
