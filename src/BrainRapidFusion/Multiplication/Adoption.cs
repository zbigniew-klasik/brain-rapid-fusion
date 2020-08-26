using BrainRapidFusion.Shared;
using System;

namespace BrainRapidFusion.Multiplication
{
    public class Adoption
    {
        public static Adoption CreateNew(int multiplicand, int multiplier, ITimeProvider timeProvider)
        {
            return new Adoption(multiplicand, multiplier, 0, timeProvider.UtcNow);
        }

        public Adoption(int multiplicand, int multiplier, int value, DateTime lastChangedUtc)
        {
            Multiplicand = multiplicand;
            Multiplier = multiplier;
            Value = value;
            LastChangedUtc = lastChangedUtc;
        }

        public int Multiplicand { get; }
        public int Multiplier { get;}
        public int Value { get; private set; }
        public DateTime LastChangedUtc { get; private set; }

        public void Increase(ITimeProvider timeProvider) => ChangeAdoption(1, timeProvider);

        public void Clear(ITimeProvider timeProvider) => ChangeAdoption(-Value, timeProvider);

        private void ChangeAdoption(int change, ITimeProvider timeProvider)
        {
            LastChangedUtc = timeProvider.UtcNow;
            Value += change;
        }
    }
}
