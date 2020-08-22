namespace BrainRapidFusion.Multiplication
{
    public class Context
    {
        public static Context CreateNew()
        {
            return new Context();
        }

        public int Score { get; private set; }

        public void AddPoints(int points)
        {
            Score += points;
        }
    }
}
