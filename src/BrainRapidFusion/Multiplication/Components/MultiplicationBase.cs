using Microsoft.AspNetCore.Components;

namespace BrainRapidFusion.Multiplication.Components
{
    public class MultiplicationBase : ComponentBase
    {
        [Inject]
        public IGameService GameService { get; set; }

        public Question Question { get; private set; }

        protected override void OnParametersSet()
        {
            ShowNextQuestion();
            base.OnParametersSet();
        }

        public void AnswerSelected(int answer)
        {
            Question.SelectAnswer(answer);
            GameService.ProcessAnsweredQuestion(Question);
            ShowNextQuestion();
        }

        private void ShowNextQuestion()
        {
            Question = GameService.GetNextQuestion();
        }
    }
}
