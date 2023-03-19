namespace Application
{
    public interface IFundaRelativeUrlBuilder
    {
        IFundaRelativeUrlBuilder WithTuin(Tuin tuin);

        string Build(string key, string aanbodType, List<string> locations, int page, int pageSize);
    }
}
