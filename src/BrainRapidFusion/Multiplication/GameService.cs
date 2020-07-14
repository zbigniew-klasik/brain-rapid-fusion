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
        }

        public void StartGame()
        {
            RefillAddoptions();
        }

        public void CancelGame()
        {

        }

        public void FinishGame()
        {

        }

        public Question GetNextQuestion()
        {
            RefillAddoptions();

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

            return question;
        }

        private Question GetAdoptedQuestion()
        {
            var adoptions = state.Adoptions
                .Where(x => !usedAdoptions.Contains(x))
                .Where(x => x.Value >= 2)
                .OrderBy(x => x.Value)
                .ToList();

            if (!adoptions.Any())
                return null;

            var adoption = adoptions.Random(randomProvider);
            usedAdoptions.Add(adoption);
            return Question.CreateFromAdoption(adoption, randomProvider);
        }

        private Question GetFreshQuestion()
        {
            var adoptions = state.Adoptions
                .Where(x => !usedAdoptions.Contains(x))
                .Where(x => x.Value < 2)
                .ToList();

            if (!adoptions.Any())
                return null;

            var adoption = adoptions.Random(randomProvider);
            usedAdoptions.Add(adoption);
            return Question.CreateFromAdoption(adoption, randomProvider);
        }

        private Question GetRrandomQuestion()
        {
            var adoptions = state.Adoptions
                .ToList();

            var adoption = adoptions.Random(randomProvider);
            usedAdoptions.Add(adoption);
            return Question.CreateFromAdoption(adoption, randomProvider);
        }

        private void RefillAddoptions()
        {
            if (!state.Adoptions.Any())
                state.Adoptions.Add(new Adoption(2, 2, timeProvider));

            while (state.Adoptions.Count(x => x.Value <= 0) < 10)
            {
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

                state.Adoptions.Add(new Adoption(multiplicand, multiplier, timeProvider));
            }
        }

        public int ProcessAnsweredQuestion(Question question)
        {
            var adoption = state.Adoptions.GetForQuestion(question);

            if (question.SelectedAnswer.IsCorrect)
            {
                adoption.Increase(timeProvider);
                return adoption.Multiplicand * adoption.Multiplier * adoption.Value;
            }

            adoption.Clear(timeProvider);
            return 0;
        }
    }
}
