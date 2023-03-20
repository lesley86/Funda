using Application.FundaExternalApiModels;
using Core;

namespace Application
{
	public interface IFundaExternalApi
    {
		Task<FundaExternalApiGetHousesResponse> GetAllAsync(string key, string aanbodType, List<string> locations, Tuin? tuin, string status, int page, int pageSize);

		Task<FundaExternalApiGetHousesResponse> GetAsync(string key, string aanbodType, List<string> locations, Tuin? tuin, string status, int page, int pageSize);
	}
}
