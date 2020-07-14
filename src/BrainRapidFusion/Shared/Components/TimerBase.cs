using Microsoft.AspNetCore.Components;
using System;
using System.Timers;

namespace BrainRapidFusion.Shared.Components
{
    public class TimerBase : ComponentBase
    {
        private System.Timers.Timer timer;

        public TimerBase()
        {
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += TimerElapsed;
            timer.Start();
        }

        [Parameter]
        public EventCallback OnTimeout { get; set; }

        [Parameter]
        public int TimeoutInSeconds { get; set; }

        public int RemainingTimeInSeconds { get; set; } = int.MinValue;

        public string RemainingTime { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (RemainingTimeInSeconds == int.MinValue)
                RemainingTimeInSeconds = TimeoutInSeconds;

            ShowRemainingTime();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            RemainingTimeInSeconds--;
            ShowRemainingTime();

            if (RemainingTimeInSeconds > 0)
                return;

            timer.Stop();
            timer.Dispose();
            _ = OnTimeout.InvokeAsync(OnTimeout);
        }

        private void ShowRemainingTime()
        {
            RemainingTime = TimeSpan.FromSeconds(RemainingTimeInSeconds).ToString(@"m\:ss");
            this.StateHasChanged();
        }
    }
}
