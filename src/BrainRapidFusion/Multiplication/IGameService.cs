using System.Threading.Tasks;

namespace BrainRapidFusion.Multiplication
{
    public interface IGameService
    {
        Task StartGame();
        Task CancelGame();
        Task FinishGame();
        Question GetNextQuestion();
        void ProcessAnsweredQuestion(Question question);
    }
}