using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrainRapidFusion.Multiplication
{
    public interface IScoreRepository
    {
        Task AddScore(int score);
        Task<IReadOnlyList<int>> GetBestScores();
        Task<bool> IsNewBestScore(int score);
    }
}