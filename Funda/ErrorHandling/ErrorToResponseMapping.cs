using Application.Exceptions;

namespace Funda.ErrorHandling
{
	public static class ErrorToResponseMapping
	{
		private static Dictionary<Type, ErrorDetails> ErrorToResponse = new Dictionary<Type, ErrorDetails>()
		{
			{ typeof(RequiredDataMissing), new ErrorDetails { StatusCode = 400, Message = "Required Informationhas not been provided please check your request and try again." } },
			{ typeof(NetworkUnreachableException), new ErrorDetails { StatusCode = 408, Message = "The network cannot be reached at this time please try again later." } }
		};

		public static bool DoesErrorToResponseMappingExist(Type errorType)
		{
			ErrorDetails error;
			return ErrorToResponse.TryGetValue(errorType, out error);
		}

		public static ErrorDetails GetErrorResponse(Type errorType)
		{
			ErrorDetails error;
			ErrorToResponse.TryGetValue(errorType, out error);
			return error;
		}
	}
}
