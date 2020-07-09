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
            Value = 0m;
            ChangesNumber = 0;
            LastChangedUtc = timeProvider.UtcNow;
        }

        public int Multiplicand { get; }
        public int Multiplier { get; }
        public decimal Value { get; private set; }
        public int ChangesNumber { get; private set; }
        public DateTime LastChangedUtc { get; private set; }

        public void Increase(ITimeProvider timeProvider) => ChangeAdoption(1, timeProvider);

        public void Decrease(ITimeProvider timeProvider) => ChangeAdoption(-1, timeProvider);

        private void ChangeAdoption(decimal change, ITimeProvider timeProvider)
        {
            LastChangedUtc = timeProvider.UtcNow;
            const decimal factor = 0.3m;
            Value = (1 - factor) * Value + factor * change;
        }

        public override string ToString() => $"{Multiplicand}x{Multiplier} @{Value}";
    }
}
