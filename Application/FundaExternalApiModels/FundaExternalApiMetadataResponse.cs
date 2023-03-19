namespace Application.FundaExternalApiModels
{
	// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
	public class FundaExternalApiMetadataResponse
	{
		public string ObjectType { get; set; }
		public string Omschrijving { get; set; }
		public string Titel { get; set; }
	}

}
