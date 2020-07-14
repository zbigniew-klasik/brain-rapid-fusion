using BrainRapidFusion.Shared;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BrainRapidFusion.Multiplication.Components
{
    public class MultiplicationBase : ComponentBase
    {
        private const int animationDuration = 300;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IGameService GameService { get; set; }

        public Question Question { get; private set; }

        public int Score { get; private set; }

        public CssClass PulpitCssClass { get; set; } = new CssClass("pulpit");

        protected override void OnParametersSet()
        {
            GameService.StartGame();
            Question = GameService.GetNextQuestion();
            base.OnParametersSet();
        }

        public void AnswerSelected(Answer answer)
        {
            var delay = answer.IsCorrect
                ? animationDuration
                : 5 * animationDuration;

            Score += GameService.ProcessAnsweredQuestion(Question);

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
            GameService.FinishGame();
            NavigationManager.NavigateTo("/multiplication/finish");
            await Task.CompletedTask;
        }
    }
}
