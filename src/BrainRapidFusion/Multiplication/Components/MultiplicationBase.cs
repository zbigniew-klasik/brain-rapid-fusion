using BrainRapidFusion.Shared;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BrainRapidFusion.Multiplication.Components
{
    public class MultiplicationBase : ComponentBase
    {
        [Inject]
        public IGameService GameService { get; set; }

        public Question Question { get; private set; }

        public CssClass PulpitCssClass { get; set; } = new CssClass("pulpit");

        protected override void OnParametersSet()
        {
            Question = GameService.GetNextQuestion();
            base.OnParametersSet();
        }

        public void AnswerSelected(Answer answer)
        {
            var waitTime = answer.IsCorrect ? 200 : 2000;

            GameService.ProcessAnsweredQuestion(Question);
            this.StateHasChanged();

            _ = Task.Delay(waitTime)
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
            await Task.Delay(300);
            PulpitCssClass.Remove("show");
            this.StateHasChanged();
        }

        private async Task HideOldQuestion()
        {
            PulpitCssClass.Add("hide");
            this.StateHasChanged();
            await Task.Delay(300);
            Question = null;
            PulpitCssClass.Remove("hide");
            this.StateHasChanged();
        }
    }
}
