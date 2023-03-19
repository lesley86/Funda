using Infrastructure.ExternalApi;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace Application
{
	public class HttpClientWrapper : IHttpClientWrapper
	{
		private readonly ILogger<HttpClientWrapper> logger;
		private readonly IPollyAsyncRetryPolicy pollyAsyncRetryPolicy;
		private HttpClient _httpClient;
		public HttpClientWrapper(
			ILogger<HttpClientWrapper> logger, 
			IPollyAsyncRetryPolicy pollyAsyncRetryPolicy)
		{
			_httpClient = new HttpClient();
			this.logger = logger;
			this.pollyAsyncRetryPolicy = pollyAsyncRetryPolicy;
		}

		public async Task<T> GetAsync<T>(string url)
		{
			var retryPollicies = pollyAsyncRetryPolicy.InitiliazeApiPolicies<T>();
			return await retryPollicies.ExecuteAsync(async () =>
			{
				try
				{
					using HttpResponseMessage response = await _httpClient.GetAsync(url);
					response.EnsureSuccessStatusCode();
					var responseBody = await response.Content.ReadFromJsonAsync<T>();
					return responseBody;
				}
				catch (Exception ex)
				{
					logger.LogError("Something broke that shouldn't have", ex);
					throw;
				}
			});
		}
	}
}
