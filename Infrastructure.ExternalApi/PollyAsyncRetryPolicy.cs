using Polly.CircuitBreaker;
using Polly.Timeout;
using Polly.Wrap;
using Polly;
using Microsoft.Extensions.Logging;
using Application.Exceptions;

namespace Infrastructure.ExternalApi
{
	public class PollyAsyncRetryPolicy : IPollyAsyncRetryPolicy
	{
		private readonly ILogger<PollyAsyncRetryPolicy> logger;

		public PollyAsyncRetryPolicy(ILogger<PollyAsyncRetryPolicy> logger)
		{
			this.logger = logger;
		}

		public AsyncPolicyWrap<TResponseMessage> InitiliazeApiPolicies<TResponseMessage>()
		{
			int maxRetries = 2;
			int breakCurcuitAfterErrors = 6;
			int keepCurcuitBreakForMinutes = 1;
			int timeoutInSeconds = 3;


			// Specify the type of exception that our policy can handle.
			// Alternately, we could specify the return results we would like to handle.
			var policyBuilder = Policy<TResponseMessage>
				.Handle<Exception>();

			var fallbackPolicy = CreateFallbackPolicy<TResponseMessage>(policyBuilder);
			var retryPolicy = CreateWaitAndRetryPolicy<TResponseMessage>(maxRetries, policyBuilder);
			var breakerPolicy = CreateCircuitBreakerPolicy<TResponseMessage>(breakCurcuitAfterErrors, keepCurcuitBreakForMinutes, policyBuilder);
			var fallbackPolicForCircuitBreaker = BrokenCircuitPolicy<TResponseMessage>();
			var timeoutPolicy = TimeoutException<TResponseMessage>(timeoutInSeconds);

			return Policy.WrapAsync(
				fallbackPolicy,
				retryPolicy,
				fallbackPolicForCircuitBreaker,
				breakerPolicy,
				timeoutPolicy);
		}

		private Polly.Fallback.AsyncFallbackPolicy<TResponseMessage> CreateFallbackPolicy<TResponseMessage>(PolicyBuilder<TResponseMessage> policyBuilder)
		{
			return policyBuilder
						  .FallbackAsync((calcellationToken) =>
						  {
							  logger.LogDebug($"Returning value from fallback policy");
							  throw new FailureToTransmitOrReceiveRestRequest();
						  });
		}

		private Polly.Retry.AsyncRetryPolicy<TResponseMessage> CreateWaitAndRetryPolicy<TResponseMessage>(int maxRetries, PolicyBuilder<TResponseMessage> policyBuilder)
		{
			return policyBuilder
				.WaitAndRetryAsync(maxRetries, retryAttempt =>
				{
					var waitTime = TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
					logger.LogDebug($"{DateTime.Now:u} - RetryPolicy | Retry Attempt: {retryAttempt} | WaitSeconds: {waitTime.TotalSeconds}");

					return waitTime;
				});
		}

		private AsyncCircuitBreakerPolicy<TResponseMessage> CreateCircuitBreakerPolicy<TResponseMessage>(int breakCurcuitAfterErrors, int keepCurcuitBreakForMinutes, PolicyBuilder<TResponseMessage> policyBuilder)
		{
			var breakerPolicy = policyBuilder
				 .CircuitBreakerAsync(breakCurcuitAfterErrors, TimeSpan.FromMinutes(keepCurcuitBreakForMinutes),
				 onBreak: (exception, timespan, context) =>
				 {
					 logger.LogDebug($"Connection has thrown {breakCurcuitAfterErrors} errors circut is blocked");
				 },
				 onReset: (context) =>
				 {
					 logger.LogDebug($"Circut is has been reset after {keepCurcuitBreakForMinutes} minutes");
				 });

			return breakerPolicy;
		}

		private Polly.Fallback.AsyncFallbackPolicy<TResponseMessage> BrokenCircuitPolicy<TResponseMessage>()
		{
			var fallbackPolicForCircuitBreaker = Policy<TResponseMessage>
				.Handle<BrokenCircuitException>()
				.FallbackAsync((calcellationToken) =>
				{
					// In our case we return a null response.
					logger.LogDebug($"{DateTime.Now:u} - The Circuit is Open (blocked) for this Provider. A fallback null value is returned. Try again later.");

					throw new NetworkUnreachableException();
				});

			return fallbackPolicForCircuitBreaker;
		}

		private AsyncTimeoutPolicy<TResponseMessage> TimeoutException<TResponseMessage>(int timeoutInSeconds)
		{
			return Policy
				.TimeoutAsync<TResponseMessage>(timeoutInSeconds, TimeoutStrategy.Pessimistic,
				onTimeoutAsync: (context, timespan, _, _) =>
				{
					logger.LogDebug($"{DateTime.Now:u} - TimeoutPolicy | Execution timed out after {timespan.TotalSeconds} seconds.");
					return Task.CompletedTask;
				});
		}
	}
}
