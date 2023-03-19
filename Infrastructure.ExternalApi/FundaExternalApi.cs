using Application;
using Application.FundaExternalApiModels;
using Microsoft.Extensions.Options;

namespace Infrastructure.ExternalApi
{
	public class FundaExternalApi : IFundaExternalApi
	{
		private readonly IFundaRelativeUrlBuilder relativeUrlBuilder;
		private readonly IHttpClientWrapper httpClientWrapper;
		private readonly ExternalApiBaseUrlOptions externalApiBaseUrlOptions;

		public FundaExternalApi(
			IFundaRelativeUrlBuilder relativeUrlBuilder,
			IHttpClientWrapper IHttpClientWrapper,
			IOptions<ExternalApiBaseUrlOptions> externalApiBaseUrlOptions)
		{
			this.relativeUrlBuilder = relativeUrlBuilder;
			httpClientWrapper = IHttpClientWrapper;
			this.externalApiBaseUrlOptions = externalApiBaseUrlOptions.Value;
		}


		public Task<FundaExternalApiGetHousesResponse> GetAsync(string key, string aanbodType, List<string> locations, Tuin? tuin, int page, int pageSize)
		{
			var relativeUrl = relativeUrlBuilder.WithTuin(tuin!)
				.Build(key, aanbodType, locations, page, pageSize);

			return httpClientWrapper.GetAsync<FundaExternalApiGetHousesResponse>($"{externalApiBaseUrlOptions.FundaBaseUrl}/{relativeUrl}");
		}
	}
}
