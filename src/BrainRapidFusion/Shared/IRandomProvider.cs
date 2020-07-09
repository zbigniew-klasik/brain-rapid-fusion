namespace BrainRapidFusion.Shared
{
    public interface IRandomProvider
    {
        int Next(int minValue, int maxValue);
        decimal NextDecimal();
        double NextDouble();
    }
}