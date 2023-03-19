namespace Application
{
	public interface IHttpClientWrapper
	{
		Task<T> GetAsync<T>(string url);
	}
}
