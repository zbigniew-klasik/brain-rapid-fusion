using Blazored.LocalStorage;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace BrainRapidFusion.Multiplication
{
    public class StateService : IStateService
    {
        private static readonly string multiplicationStateKey = "MultiplicationState";
        private readonly ILocalStorageService localStorageService;

        public StateService(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public async Task<State> Get()
        {
            var json = await localStorageService.GetItemAsStringAsync(multiplicationStateKey);
            
            if (string.IsNullOrEmpty(json))
                return new State();

            return JsonConvert.DeserializeObject<State>(json);
        }

        public async Task Set(State state)
        {
            var json = JsonConvert.SerializeObject(state);
            await localStorageService.SetItemAsync(multiplicationStateKey, json);
        }

        public async Task Sync()
        {
            // sync browser cache with server
            throw new NotImplementedException();
        }
    }
}
