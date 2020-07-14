namespace BrainRapidFusion.Multiplication
{
    public interface IGameService
    {
        void StartGame();
        void CancelGame();
        void FinishGame();
        Question GetNextQuestion();
        int ProcessAnsweredQuestion(Question question);
    }
}