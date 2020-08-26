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
        private readonly IContextProvider contextProvider;
        private readonly IAdoptionRepository adoptionRepository;
        private readonly IScoreRepository scoreRepository;

        private List<Adoption> allAdoptions = new List<Adoption>();
        private List<Adoption> usedAdoptions = new List<Adoption>();

        public GameService(
            IRandomProvider randomProvider,
            ITimeProvider timeProvider,
            IContextProvider contextProvider,
            IAdoptionRepository adoptionRepository,
            IScoreRepository scoreRepository)
        {
            this.randomProvider = randomProvider;
            this.timeProvider = timeProvider;
            this.contextProvider = contextProvider;
            this.adoptionRepository = adoptionRepository;
            this.scoreRepository = scoreRepository;
        }

        public async Task StartGame()
        {
            allAdoptions = (await adoptionRepository.Get()).ToList();

            contextProvider.Get().Reset();

            usedAdoptions.Clear();
            RefillAddoptions();
        }

        public async Task CancelGame()
        {
            await adoptionRepository.Set(allAdoptions);
        }

        public async Task FinishGame()
        {
            await adoptionRepository.Set(allAdoptions);
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
            var adoptions = allAdoptions
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
            var adoptions = allAdoptions
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
            var adoptions = allAdoptions
                .ToList();

            var adoption = adoptions.Random(randomProvider);
            usedAdoptions.Add(adoption);
            return Question.CreateFromAdoption(adoption, randomProvider);
        }

        private void RefillAddoptions()
        {
            if (!allAdoptions.Any())
                allAdoptions.Add(Adoption.CreateNew(2, 2, timeProvider));

            while (allAdoptions.Count(x => x.Value <= 0) < 6)
            {
                var multiplicand = allAdoptions.Max(x => x.Multiplicand);
                var multiplicandCopy = multiplicand;
                var multipliers = allAdoptions.Where(x => x.Multiplicand == multiplicandCopy).Select(x => x.Multiplier).ToList();
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

                allAdoptions.Add(Adoption.CreateNew(multiplicand, multiplier, timeProvider));
            }
        }

        private bool IsBasicMultiplicationTableAdopted()
        {
            return allAdoptions.Any(x => x.Multiplicand == 10 && x.Multiplier == 10)
                && allAdoptions.Where(x => x.Multiplicand <= 10 && x.Multiplier <= 10).All(x => x.Value > 0);
        }

        public void ProcessAnsweredQuestion(Question question)
        {
            var adoption = allAdoptions.GetForQuestion(question);
            var context = contextProvider.Get();

            if (question.SelectedAnswer.IsCorrect)
            {
                adoption.Increase(timeProvider);
                context.AddPoints(adoption.Multiplicand * adoption.Multiplier * adoption.Value);
                return;
            }

            adoption.Clear(timeProvider);
        }
    }
}
