using System;
using System.Collections.Generic;
using System.Linq;

namespace BrainRapidFusion.Multiplication
{
    public class Question
    {
        private static Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF); // todo: refacotr as a service?

        private Question(int multiplicand, int multiplier, IList<Answer> proposedAnswers)
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

        public static Question CreateFromAdoption(Adoption adoption)
        {
            var proposedAnswers = GenerateAnswers(adoption.Multiplicand, adoption.Multiplier, adoption.Value);
            return new Question(adoption.Multiplicand, adoption.Multiplier, proposedAnswers); // todo: randomize order of multiplicand and multiplier
        }

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

        private static IList<Answer> GenerateAnswers(int multiplicand, int multiplier, decimal adoption)
        {
            adoption = adoption < 0 ? 0 : adoption;

            int numberOfAnswers = (int)Decimal.Ceiling(3 + adoption * 5);

            var correctAnswer = multiplicand * multiplier;

            var wrongAnswers = new List<int>
            {
                multiplicand * (multiplier + 1),
                multiplicand * (multiplier - 1),
                (multiplicand + 1) * multiplier,
                (multiplicand - 1) * multiplier,
                multiplicand * multiplier + 1,
                multiplicand * (multiplier + 1) + 1,
                multiplicand * (multiplier - 1) + 1,
                (multiplicand + 1) * multiplier + 1,
                (multiplicand - 1) * multiplier + 1,
                multiplicand * multiplier - 1,
                multiplicand * (multiplier + 1) - 1,
                multiplicand * (multiplier - 1) - 1,
                (multiplicand + 1) * multiplier - 1,
                (multiplicand - 1) * multiplier - 1,
                multiplicand * multiplier + 2,
                multiplicand * (multiplier + 1) + 2,
                multiplicand * (multiplier - 1) + 2,
                (multiplicand + 1) * multiplier + 2,
                (multiplicand - 1) * multiplier + 2,
                multiplicand * multiplier - 2,
                multiplicand * (multiplier + 1) - 2,
                multiplicand * (multiplier - 1) - 2,
                (multiplicand + 1) * multiplier - 2,
                (multiplicand - 1) * multiplier - 2,
            };

            wrongAnswers = wrongAnswers
                .Where(x => x != correctAnswer)
                .Where(x => x > 3)
                .Distinct()
                .OrderBy(x => random.Next())
                .Take(numberOfAnswers - 1)
                .ToList();

            wrongAnswers.Add(correctAnswer);

            return wrongAnswers
                .OrderBy(x => random.Next())
                .Select(x => new Answer(x, x == correctAnswer))
                .ToList();
        }
    }
}
