using Application.FundaExternalApiModels;

namespace Application
{
	public interface IExternalCocktailApi
    {
		Task<CateogryListExtenalApiResponse> GetCategories(string key, int page, int pageSize);
	}
}
