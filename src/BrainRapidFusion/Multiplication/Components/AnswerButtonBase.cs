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
        public int Count { get; set; }

        [Parameter]
        public int Answer { get; set; }

        [Parameter]
        public bool IsCorrect { get; set; }

        [Parameter]
        public bool IsRevealed { get; set; }

        public CssClass ButtonCssClass { get; set; } = new CssClass("answer-button");

        [Parameter]
        public EventCallback<int> OnClickCallback { get; set; }

        protected double PositionTop { get; private set; }

        protected double PositionLeft { get; private set; }

        private bool IsClicked { get; set; }

        protected override void OnParametersSet()
        {
            SetAnswerPosition();

            if (IsRevealed && (IsClicked || IsCorrect))
            {
                if (IsCorrect) ButtonCssClass.Add("correct");
                else ButtonCssClass.Add("wrong");
            }
            else
            {
                ButtonCssClass.Remove("correct");
                ButtonCssClass.Remove("wront");
            }

            base.OnParametersSet();
        }

        public Task OnClicked()
        {
            if (IsClicked)
                return Task.CompletedTask;

            IsClicked = true;
            return OnClickCallback.InvokeAsync(Answer);
        }

        private void SetAnswerPosition()
        {
            var center = 50d;
            var radius = 35d;
            var angle = Math.PI * 2 * Index / Count + Math.PI / 2;

            PositionTop = Math.Round(center + radius * Math.Sin(angle));
            PositionLeft = Math.Round(center + radius * Math.Cos(angle));
        }
    }
}
