using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            IEnumerable<int> proposedAnswers = GenerateAnswers(multiplicand, multiplier, 6);

            return new Question(multiplicand, multiplier, proposedAnswers);
        }

        public void ProcessAnsweredQuestion(Question question)
        {
        }

        private IEnumerable<int> GenerateAnswers(int multiplicand, int multiplier, int numberOfAnswers)
        {
            var answers = new List<int>
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

            answers = answers
                .OrderBy(x => random.Next())
                .Take(numberOfAnswers - 1)
                .ToList();

            answers.Add(multiplicand * multiplier);

            return answers
                .OrderBy(x => random.Next())
                .ToList();
        }
    }
}
