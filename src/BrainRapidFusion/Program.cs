using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BrainRapidFusion.Multiplication;
using BrainRapidFusion.Shared;
using Blazored.LocalStorage;

namespace BrainRapidFusion
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddSingleton<ITimeProvider, TimeProvider>();
            builder.Services.AddSingleton<IRandomProvider, RandomProvider>();
            builder.Services.AddTransient<IContextProvider, ContextProvider>();
            builder.Services.AddTransient<IScoreRepository, ScoreRepository>();
            builder.Services.AddTransient<IStateService, StateService>();
            builder.Services.AddTransient<IGameService, GameService>();

            await builder.Build().RunAsync();
        }
    }
}
