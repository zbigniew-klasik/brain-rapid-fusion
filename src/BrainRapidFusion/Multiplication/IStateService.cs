using System.Threading.Tasks;

namespace BrainRapidFusion.Multiplication
{
    public interface IStateService
    {
        Task<State> Get();
        Task Set(State state);
        Task Sync();
    }
}