using Application;
using Application.FundaExternalApiModels;
using Microsoft.Extensions.Options;

namespace Infrastructure.ExternalApi
{
	public class ExternalCocktailApi : IExternalCocktailApi
	{
		private readonly IHttpClientWrapper httpClientWrapper;
		private readonly ExternalApiBaseUrlOptions externalApiBaseUrlOptions;

		public ExternalCocktailApi(
			IHttpClientWrapper IHttpClientWrapper,
			IOptions<ExternalApiBaseUrlOptions> externalApiBaseUrlOptions)
		{
			httpClientWrapper = IHttpClientWrapper;
			this.externalApiBaseUrlOptions = externalApiBaseUrlOptions.Value;
		}


		public Task<CateogryListExtenalApiResponse> GetCategories(string key,int page, int pageSize)
		{
			var result = httpClientWrapper.GetAsync<CateogryListExtenalApiResponse>($"{externalApiBaseUrlOptions.CocktailBaseUrl}/{key}/list.php?c=list");
			return result;
		}
	}
}
