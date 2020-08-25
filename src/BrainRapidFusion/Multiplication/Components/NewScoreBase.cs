using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainRapidFusion.Multiplication.Components
{
    public class NewScoreBase : ComponentBase
    {
        [Inject]
        public IContextProvider ContextProvider { get; set; }

        [Inject]
        public IScoreRepository ScoreRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public bool IsNewBestScore { get; private set; }

        protected override async Task OnParametersSetAsync()
        {
            IsNewBestScore = await ScoreRepository.IsNewBestScore(ContextProvider.Get().Score);
            base.OnParametersSet();
        }
        public void Continue()
        {
            NavigationManager.NavigateTo("multiplication/summary");
        }
    }
}
