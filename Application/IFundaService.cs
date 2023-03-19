using Core.Models;

namespace Application
{
    public interface IFundaService
    {
		Task<IEnumerable<MakelaarWithObjectCount>> GetHouses();
    }
}