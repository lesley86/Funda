using Core.Models;

namespace Application
{
    public interface IFundaService
    {
		Task<MakelaarWithTuinAndLocation> GetHouses();
    }
}