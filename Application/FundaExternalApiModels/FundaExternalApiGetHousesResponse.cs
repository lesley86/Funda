namespace Application.FundaExternalApiModels
{
	public class FundaExternalApiGetHousesResponse
	{
		public int AccountStatus { get; set; }
		public bool EmailNotConfirmed { get; set; }
		public bool ValidationFailed { get; set; }
		public object ValidationReport { get; set; }
		public int Website { get; set; }
		public FundaExternalApiMetadataResponse Metadata { get; set; }
		public List<FundaExternalApiObjectResponse> Objects { get; set; }
		public FundaExternalApiPagingResponse Paging { get; set; }
		public int TotaalAantalObjecten { get; set; }
	}

}
