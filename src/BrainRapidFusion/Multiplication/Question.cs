using System;
using System.Collections.Generic;

namespace BrainRapidFusion.Multiplication
{
    public class Question
    {
        public Question(int multiplicand, int multiplier, IList<Answer> proposedAnswers)
        {
            Multiplicand = multiplicand;
            Multiplier = multiplier;
            ProposedAnswers = proposedAnswers;
        }

        public int Multiplicand { get; }

        public int Multiplier { get; }

        public IList<Answer> ProposedAnswers { get; }

        public Answer SelectedAnswer { get; private set; }

        public bool IsAnswerSelected { get; private set; }

        public void SelectAnswer(Answer answer)
        {
            if (IsAnswerSelected)
                throw new InvalidOperationException("Answer already selected.");

            if (!ProposedAnswers.Contains(answer))
                throw new ArgumentException("Unknown answer.");

            SelectedAnswer = answer;
            IsAnswerSelected = true;
        }

        public override string ToString() => $"{Multiplicand} × {Multiplier}";
    }
}
