using Polly.Wrap;

namespace Infrastructure.ExternalApi
{
	public interface IPollyAsyncRetryPolicy
	{
		AsyncPolicyWrap<TResponseMessage> InitiliazeApiPolicies<TResponseMessage>();
	}
}