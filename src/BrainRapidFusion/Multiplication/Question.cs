using BrainRapidFusion.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BrainRapidFusion.Multiplication
{
    public class Question
    {
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

        public static Question CreateFromAdoption(Adoption adoption, IRandomProvider randomProvider)
        {
            var parameters = (new List<int>() { adoption.Multiplicand, adoption.Multiplier }).Shuffle(randomProvider);
            var multiplicand = parameters.First();
            var multiplier = parameters.Last();
            var proposedAnswers = GenerateAnswers(adoption.Multiplicand, adoption.Multiplier, adoption.Value + 3, randomProvider);
            return new Question(multiplicand, multiplier, proposedAnswers);
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

        private static IList<Answer> GenerateAnswers(int multiplicand, int multiplier, int numberOfAnswers, IRandomProvider randomProvider)
        {
            numberOfAnswers = (numberOfAnswers < 3) ? 3 : numberOfAnswers;
            numberOfAnswers = (numberOfAnswers > 8) ? 8 : numberOfAnswers;

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
                .Shuffle(randomProvider)
                .Take(numberOfAnswers - 1)
                .ToList();

            wrongAnswers.Add(correctAnswer);

            return wrongAnswers
                .Shuffle(randomProvider)
                .Select(x => new Answer(x, x == correctAnswer))
                .ToList();
        }
    }
}
