using System;

namespace BrainRapidFusion.Shared
{
    public class RandomProvider : IRandomProvider
    {
        private readonly Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

        public int Next(int minValue, int maxValue) => random.Next(minValue, maxValue);

        public double NextDouble() => random.NextDouble();

        public decimal NextDecimal() => (decimal)random.NextDouble();
    }
}
