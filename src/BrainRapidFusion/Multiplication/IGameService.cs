namespace BrainRapidFusion.Multiplication
{
    public interface IGameService
    {
        public Question GetNextQuestion();
        public void ProcessAnsweredQuestion(Question question);
    }
}