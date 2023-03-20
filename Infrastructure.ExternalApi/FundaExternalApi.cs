using Application;
using Application.FundaExternalApiModels;
using Core;
using Microsoft.Extensions.Options;
using System.Drawing.Text;

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


		public Task<FundaExternalApiGetHousesResponse> GetAsync(string key, string aanbodType, List<string> locations, Tuin? tuin, string status, int page, int pageSize)
		{
			var relativeUrl = relativeUrlBuilder.WithTuin(tuin!)
				.WithStatus(status)
				.Build(key, aanbodType, locations, page, pageSize);

			return httpClientWrapper.GetAsync<FundaExternalApiGetHousesResponse>($"{externalApiBaseUrlOptions.FundaBaseUrl}/{relativeUrl}");
		}

		public async Task<FundaExternalApiGetHousesResponse> GetAllAsync(string key, string aanbodType, List<string> locations, Tuin? tuin, string status, int page, int pageSize)
		{
			string relativeUrl = relativeUrlBuilder.WithTuin(tuin!)
				.WithStatus(status)
				.Build(key, aanbodType, locations, page, pageSize);
			int huidigePagina = 1;

			FundaExternalApiGetHousesResponse result;

			var response =  await httpClientWrapper.GetAsync<FundaExternalApiGetHousesResponse>($"{externalApiBaseUrlOptions.FundaBaseUrl}/{relativeUrl}");
			result = response;
			while (response.Paging.AantalPaginas < response.Paging.HuidigePagina)
			{
				huidigePagina++;
				relativeUrl = relativeUrlBuilder.WithTuin(tuin!)
					.WithStatus(status)
					.Build(key, aanbodType, locations, huidigePagina, pageSize);

				response = await httpClientWrapper.GetAsync<FundaExternalApiGetHousesResponse>($"{externalApiBaseUrlOptions.FundaBaseUrl}/{relativeUrl}");
				result.Objects.AddRange(response.Objects);
			}

			return result;
		}
	}
}
