using System;
using System.Collections.Generic;
using System.Linq;

namespace BrainRapidFusion.Multiplication
{
    public class GameService : IGameService
    {
        private static Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

        public GameService()
        {
        }

        public Question GetNextQuestion()
        {
            var multiplicand = random.Next(2, 11);
            var multiplier = random.Next(2, 11);
            var proposedAnswers = GenerateAnswers(multiplicand, multiplier, random.Next(3, 10));

            return new Question(multiplicand, multiplier, proposedAnswers);
        }

        public void ProcessAnsweredQuestion(Question question)
        {
        }

        private IList<Answer> GenerateAnswers(int multiplicand, int multiplier, int numberOfAnswers)
        {
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
