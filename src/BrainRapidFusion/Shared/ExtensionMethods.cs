using BrainRapidFusion.Multiplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainRapidFusion.Shared
{
    public static class ExtensionMethods
    {
        public static T Random<T>(this IEnumerable<T> enumerable, IRandomProvider randomProvider)
        {
            if (enumerable is null)
                throw new ArgumentException("Enumerable is null.", "enumerable");

            if (!enumerable.Any())
                throw new ArgumentException("Enumerable is empty.", "enumerable");

            if (randomProvider is null)
                throw new ArgumentException("Random Provider is null", "randomProvider");

            var list = enumerable.ToList();
            var id = randomProvider.Next(0, list.Count);
            return list[id];
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable, IRandomProvider randomProvider)
        {
            if (enumerable is null)
                throw new ArgumentException("Enumerable is null.", "enumerable");

            if (randomProvider is null)
                throw new ArgumentException("Random Provider is null", "randomProvider");

            return enumerable.OrderBy(x => randomProvider.NextDouble()).ToList();
        }

        public static Adoption GetForQuestion(this IEnumerable<Adoption> adoptions, Question question)
        {
            if (adoptions is null)
                throw new ArgumentException("Adoptions is null.", "adoptions");

            if (!adoptions.Any())
                throw new ArgumentException("Adoptions is empty.", "adoptions");

            if (question is null)
                throw new ArgumentException("Question is null", "question");

            var adoption = adoptions
                .Where(x => (
                    x.Multiplicand == question.Multiplicand && x.Multiplier == question.Multiplier)
                    || (x.Multiplicand == question.Multiplier && x.Multiplier == question.Multiplicand))
                .SingleOrDefault();

            if (adoption is null)
                throw new ArgumentException($"There is no adoption for question: {question}");

            return adoption;
        }
    }
}
