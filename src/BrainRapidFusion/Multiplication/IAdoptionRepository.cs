using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrainRapidFusion.Multiplication
{
    public interface IAdoptionRepository
    {
        Task<IReadOnlyList<Adoption>> Get();
        Task Set(IEnumerable<Adoption> adoptions);
    }
}