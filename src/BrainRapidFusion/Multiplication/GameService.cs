using BrainRapidFusion.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BrainRapidFusion.Multiplication
{
    public class GameService : IGameService
    {
        private static State state = new State();
        private static List<Adoption> usedAdoptions = new List<Adoption>();
        private readonly IRandomProvider randomProvider;
        private readonly ITimeProvider timeProvider;

        public GameService(IRandomProvider randomProvider, ITimeProvider timeProvider)
        {
            this.randomProvider = randomProvider;
            this.timeProvider = timeProvider;
            RefillAddoptions();
        }

        public Question GetNextQuestion()
        {
            if (usedAdoptions.Count >= 10)
                usedAdoptions.Remove(usedAdoptions.First());

            Console.WriteLine($"Adoptions: {string.Join(" | ", state.Adoptions)}");

            Question question = null;

            if (randomProvider.NextDouble() > 0.3)
            {
                question = GetAdoptedQuestion();
            }

            if (question is null)
            {
                question = GetFreshQuestion();
            }

            if (question is null)
            {
                question = GetRrandomQuestion();
            }

            Console.WriteLine("====================");

            return question;
        }

        private Question GetAdoptedQuestion()
        {
            Console.WriteLine("GetAdoptedQuestion");

            var adoptions = state.Adoptions
                .Where(x => !usedAdoptions.Contains(x))
                .Where(x => x.Value > 0)
                .OrderBy(x => x.Value)
                .ToList();

            if (!adoptions.Any())
                return null;

            var totalAdoption = adoptions.Sum(x => 1 - x.Value);
            var r = (decimal)randomProvider.NextDouble() * totalAdoption;

            var selected = adoptions.Last();
            decimal sum = 0;
            for (int id = 0; id < adoptions.Count; id++)
            {
                sum += 1 - adoptions[id].Value;
                if (r < sum)
                {
                    usedAdoptions.Add(adoptions[id]);
                    Console.WriteLine(adoptions[id]);
                    return Question.CreateFromAdoption(adoptions[id]);
                } 
            }

            return null;
        }

        private Question GetFreshQuestion()
        {
            RefillAddoptions();

            Console.WriteLine("GetFreshQuestion");

            var adoptions = state.Adoptions
                .Where(x => !usedAdoptions.Contains(x))
                .Where(x => x.Value <= 0)
                .ToList();

            var id = randomProvider.Next(0, adoptions.Count);
            usedAdoptions.Add(adoptions[id]);
            Console.WriteLine(adoptions[id]);
            return Question.CreateFromAdoption(adoptions[id]);
        }

        private void RefillAddoptions()
        {
            if (!state.Adoptions.Any())
                state.Adoptions.Add(new Adoption(2, 2, timeProvider));

            while (state.Adoptions.Count(x => x.Value <= 0) < 10)
            {
                var l = state.Adoptions.ToList();
                var multiplicand = state.Adoptions.Max(x => x.Multiplicand);
                var multiplicandCopy = multiplicand;
                var multipliers = state.Adoptions.Where(x => x.Multiplicand == multiplicandCopy).Select(x => x.Multiplier).ToList();
                var multiplier = multipliers.Any() ? multipliers.Max(x => x) : 2;

                if (multiplier == multiplicand)
                {
                    multiplicand++;
                    multiplier = 2;
                } 
                else
                {
                    multiplier++;
                }

                Console.WriteLine($"Refill {multiplicand}x{multiplier}");

                state.Adoptions.Add(new Adoption(multiplicand, multiplier, timeProvider));
            }
        }

        private Question GetRrandomQuestion()
        {
            Console.WriteLine("GetRrandomQuestion");

            var adoptions = state.Adoptions
                .ToList();

            var id = randomProvider.Next(0, adoptions.Count);
            usedAdoptions.Add(adoptions[id]);
            Console.WriteLine(adoptions[id]);
            return Question.CreateFromAdoption(adoptions[id]);
        }

        public void ProcessAnsweredQuestion(Question question)
        {
            var adoption = state.Adoptions
                .Where(x => x.Multiplicand == question.Multiplicand)
                .Where(x => x.Multiplier == question.Multiplier)
                .SingleOrDefault();

            if (adoption is null)
            {
                adoption = new Adoption(question.Multiplicand, question.Multiplier, timeProvider);
                state.Adoptions.Add(adoption);
            }

            if (question.SelectedAnswer.IsCorrect)
                adoption.Increase(timeProvider);
            else
                adoption.Decrease(timeProvider);
        }
    }
}
