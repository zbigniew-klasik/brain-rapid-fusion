using BrainRapidFusion.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BrainRapidFusion.Multiplication.Components
{
    public class AnswerButtonBase : ComponentBase
    {
        [Parameter]
        public int Index { get; set; }

        [Parameter]
        public Question Question { get; set; }

        [Parameter]
        public Answer Answer { get; set; }

        public CssClass ButtonCssClass { get; private set; } = new CssClass("answer-button");

        public string ButtonPosition { get; private set; }

        [Parameter]
        public EventCallback<Answer> OnClickCallback { get; set; }

        protected override void OnParametersSet()
        {
            SetAnswerPosition();

            if (Question.IsAnswerSelected && Answer.IsCorrect)
                Reveal();

            base.OnParametersSet();
        }

        public Task OnClicked()
        {
            if (Question.IsAnswerSelected)
                return Task.CompletedTask;

            Reveal();

            Question.SelectAnswer(Answer);

            return OnClickCallback.InvokeAsync(Answer);
        }

        private void SetAnswerPosition()
        {
            var center = 50d;
            var radius = 35d;
            var angle = Math.PI * 2 * Index / Question.ProposedAnswers.Count + Math.PI / 2;
            var top = (int)Math.Round(center + radius * Math.Sin(angle));
            var left = (int)Math.Round(center + radius * Math.Cos(angle));
            ButtonPosition = $"top: {top}%; left: {left}%";
        }

        private void Reveal()
        {
            if (Answer.IsCorrect)
                ButtonCssClass.Add("correct");
            else
                ButtonCssClass.Add("wrong");
        }
    }
}
