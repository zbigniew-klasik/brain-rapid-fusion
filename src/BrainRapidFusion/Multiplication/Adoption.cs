using BrainRapidFusion.Shared;
using System;

namespace BrainRapidFusion.Multiplication
{
    public class Adoption
    {
        public Adoption(int multiplicand, int multiplier, ITimeProvider timeProvider)
        {
            Multiplicand = multiplicand;
            Multiplier = multiplier;
            Value = 0;
            ChangesNumber = 0;
            LastChangedUtc = timeProvider.UtcNow;
        }

        public int Multiplicand { get; }
        public int Multiplier { get; }
        public int Value { get; private set; }
        public int ChangesNumber { get; private set; }
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
