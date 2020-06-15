using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BrainRapidFusion.Multiplication.Components
{
    public class AnswerButtonBase : ComponentBase
    {
        [Parameter]
        public int Answer { get; set; }

        [Parameter]
        public EventCallback<int> OnClickCallback { get; set; }

        public Task OnClicked()
        {
            return OnClickCallback.InvokeAsync(Answer);
        }
    }
}
