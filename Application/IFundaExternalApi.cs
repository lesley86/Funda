using Application.FundaExternalApiModels;

namespace Application
{
	public interface IFundaExternalApi
    {
        Task<FundaExternalApiGetHousesResponse> GetAsync(
            string key,
            string aanbodType,
            List<string> locations,
            Tuin? tuin,
            int page,
            int pageSize);
    }
}
