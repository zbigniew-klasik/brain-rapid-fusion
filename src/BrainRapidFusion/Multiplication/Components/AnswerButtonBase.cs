using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainRapidFusion.Multiplication.Components
{
    public class AnswerButtonBase : ComponentBase
    {
        [Parameter]
        public int Answer { get; set; }

        [Parameter]
        public bool IsCorrect { get; set; }

        [Parameter]
        public EventCallback<int> OnClickCallback { get; set; }

        public string Class { get; private set; } = "btn btn-primary";

        public Task OnClicked()
        {
            if (IsCorrect) Class = "btn btn-success";
            else Class = "btn btn-danger";

            return OnClickCallback.InvokeAsync(Answer);
        }
    }
}
