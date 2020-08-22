using BrainRapidFusion.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainRapidFusion.Multiplication
{
    public class GameService : IGameService
    {
        private readonly IRandomProvider randomProvider;
        private readonly ITimeProvider timeProvider;
        private readonly IStateService stateService;
        private readonly IContextProvider contextProvider;
        private State state = new State();
        private List<Adoption> usedAdoptions = new List<Adoption>();

        public GameService(IRandomProvider randomProvider, ITimeProvider timeProvider, IStateService stateService, IContextProvider contextProvider)
        {
            this.randomProvider = randomProvider;
            this.timeProvider = timeProvider;
            this.stateService = stateService;
            this.contextProvider = contextProvider;
        }

        public async Task StartGame()
        {
            state = await stateService.Get();

            usedAdoptions.Clear();
            RefillAddoptions();
        }

        public async Task CancelGame()
        {
            await stateService.Set(state);
        }

        public async Task FinishGame()
        {
            await stateService.Set(state);
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
                state.Adoptions.Add(new Adoption(2, 2));

            while (state.Adoptions.Count(x => x.Value <= 0) < 6)
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

                if (multiplicand >= 10 && !IsBasicMultiplicationTableAdopted())
                    break;

                state.Adoptions.Add(new Adoption(multiplicand, multiplier));
            }
        }

        private bool IsBasicMultiplicationTableAdopted()
        {
            return state.Adoptions.Any(x => x.Multiplicand == 10 && x.Multiplier == 10)
                && state.Adoptions.Where(x => x.Multiplicand <= 10 && x.Multiplier <= 10).All(x => x.Value > 0);
        }

        public void ProcessAnsweredQuestion(Question question)
        {
            var adoption = state.Adoptions.GetForQuestion(question);
            var context = contextProvider.Get();

            if (question.SelectedAnswer.IsCorrect)
            {
                adoption.Increase(timeProvider);
                context.AddPoints(adoption.Multiplicand * adoption.Multiplier * adoption.Value);
            }

            adoption.Clear(timeProvider);
        }
    }
}
