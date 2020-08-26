using Blazored.LocalStorage;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrainRapidFusion.Multiplication
{
    public class AdoptionRepository : IAdoptionRepository
    {
        private const string storageKey = "adoptions";
        private readonly ILocalStorageService localStorageService;

        public AdoptionRepository(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public async Task<IReadOnlyList<Adoption>> Get()
        {
            var json = await localStorageService.GetItemAsStringAsync(storageKey);

            var adoptions = new List<Adoption>();

            if (!string.IsNullOrEmpty(json))
                adoptions = JsonConvert.DeserializeObject<List<Adoption>>(json);

            return adoptions;
        }

        public async Task Set(IEnumerable<Adoption> adoptions)
        {
            var json = JsonConvert.SerializeObject(adoptions);
            await localStorageService.SetItemAsync(storageKey, json);
        }
    }
}
