using System;

namespace BrainRapidFusion.Shared
{
    public interface ITimeProvider
    {
        DateTime UtcNow { get; }
    }
}