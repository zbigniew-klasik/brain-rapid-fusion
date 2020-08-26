using Blazored.LocalStorage;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainRapidFusion.Multiplication
{
    public class ScoreRepository : IScoreRepository
    {
        private const string storageKey = "best-scores";
        private readonly ILocalStorageService localStorageService;

        public ScoreRepository(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public async Task<bool> IsNewBestScore(int score)
        {
            var bestScore = (await GetBestScores()).Max();
            return score > bestScore;
        }

        public async Task AddScore(int score)
        {
            var bestScores = (await GetBestScores()).ToList();
            bestScores.Add(score);
            bestScores = bestScores.OrderByDescending(s => s).Take(5).ToList();
            var json = JsonConvert.SerializeObject(bestScores);
            await localStorageService.SetItemAsync(storageKey, json);
        }

        public async Task<IReadOnlyList<int>> GetBestScores()
        {
            var json = await localStorageService.GetItemAsStringAsync(storageKey);

            var bestScores = new List<int>();

            if (!string.IsNullOrEmpty(json))
                bestScores = JsonConvert.DeserializeObject<List<int>>(json);

            while (bestScores.Count < 5)
                bestScores.Add(0);

            return bestScores;
        }
    }
}
