using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainRapidFusion.Multiplication
{
    public class Question
    {
        public int Multiplicand { get; private set; }

        public int Multiplier { get; private set; }

        public IList<int> ProposedAnswers { get; private set; }

        public int SelectedAnswer { get; private set; }

        public Question(int multiplicand, int multiplier, IList<int> proposedAnswers)
        {
            Multiplicand = multiplicand;
            Multiplier = multiplier;
            ProposedAnswers = proposedAnswers;
        }

        public void SelectAnswer(int answer)
        {
            if (!ProposedAnswers.Contains(answer))
                throw new ArgumentException("Unknown answer.");

            SelectedAnswer = answer;
        }

        public override string ToString() => $"{Multiplicand} × {Multiplier}";
    }
}
