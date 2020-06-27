namespace BrainRapidFusion.Multiplication
{
    public class Answer
    {
        public Answer(int value, bool isCorrect)
        {
            Value = value;
            IsCorrect = isCorrect;
        }

        public int Value { get; }
        public bool IsCorrect { get; }

        public override string ToString() => Value.ToString();
    }
}
