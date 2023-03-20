using Core;

namespace Application
{
	public interface IFundaRelativeUrlBuilder
    {
        IFundaRelativeUrlBuilder WithTuin(Tuin tuin);

		IFundaRelativeUrlBuilder WithStatus(string? status);


		string Build(string key, string aanbodType, List<string> locations, int page, int pageSize);
    }
}
