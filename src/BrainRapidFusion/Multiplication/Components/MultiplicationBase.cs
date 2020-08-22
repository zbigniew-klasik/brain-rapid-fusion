using BrainRapidFusion.Shared;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BrainRapidFusion.Multiplication.Components
{
    public class MultiplicationBase : ComponentBase
    {
        private const int animationDuration = 300;

        [Inject]
        public IContextProvider ContextProvider { get; set; }

        [Inject]
        public IGameService GameService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Question Question { get; private set; }

        public bool IsMuted { get; private set; }

        public CssClass PulpitCssClass { get; set; } = new CssClass("pulpit");

        protected override async Task OnParametersSetAsync()
        {
            await GameService.StartGame();
            Question = GameService.GetNextQuestion();
            base.OnParametersSet();
        }

        public void SetVolume(bool isMuted) => IsMuted = isMuted;

        public void AnswerSelected(Answer answer)
        {
            var delay = answer.IsCorrect
                ? animationDuration
                : 5 * animationDuration;

            GameService.ProcessAnsweredQuestion(Question);

            this.StateHasChanged();

            _ = Task.Delay(delay)
                .ContinueWith(t => HideOldQuestion())
                .Unwrap()
                .ContinueWith(t => ShowNewQuestion())
                .Unwrap();
        }

        private async Task ShowNewQuestion()
        {
            Question = GameService.GetNextQuestion();
            PulpitCssClass.Add("show");
            this.StateHasChanged();
            await Task.Delay(animationDuration);
            PulpitCssClass.Remove("show");
            this.StateHasChanged();
        }

        private async Task HideOldQuestion()
        {
            PulpitCssClass.Add("hide");
            this.StateHasChanged();
            await Task.Delay(animationDuration);
            Question = null;
            PulpitCssClass.Remove("hide");
            this.StateHasChanged();
        }

        public async Task FinishGame()
        {
            await GameService.FinishGame();
            NavigationManager.NavigateTo("/multiplication/finish");
        }
    }
}
