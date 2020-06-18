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
        public EventCallback<int> OnClickCallback { get; set; }

        protected double PositionTop { get; private set; }

        protected double PositionLeft { get; private set; }

        protected override void OnParametersSet()
        {
            SetAnswerPosition();
            base.OnParametersSet();
        }

        public Task OnClicked()
        {
            return OnClickCallback.InvokeAsync(Answer);
        }

        private void SetAnswerPosition()
        {
            var center = 50d;
            var radius = 35d;
            var angle = Math.PI * 2 * Index / Count + Math.PI / 2;

            PositionTop = Math.Round(center + radius * Math.Sin(angle), 2);
            PositionLeft = Math.Round(center + radius * Math.Cos(angle), 2);
        }
    }
}
