namespace Application
{
    public interface IFundaRelativeUrlBuilder
    {
        IFundaRelativeUrlBuilder WithTuin(Tuin tuin);

        string Build(string key, string aanbodType, List<string> locations, Tuin? tuin, int page, int pageSize);
    }
}
