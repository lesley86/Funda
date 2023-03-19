using System.Text.Json.Serialization;

namespace Application.FundaExternalApiModels
{
	public class FundaExternalApiObjectResponse
	{
		public string AangebodenSindsTekst { get; set; }
		[JsonConverter(typeof(JsonMicrosoftDateTimeConverter))]
		public DateTime AanmeldDatum { get; set; }
		public object AantalBeschikbaar { get; set; }
		public int AantalKamers { get; set; }
		public object AantalKavels { get; set; }
		public string Aanvaarding { get; set; }
		public string Adres { get; set; }
		public int Afstand { get; set; }
		public string BronCode { get; set; }
		public List<object> ChildrenObjects { get; set; }
		public object DatumAanvaarding { get; set; }
		public object DatumOndertekeningAkte { get; set; }
		public string Foto { get; set; }
		public string FotoLarge { get; set; }
		public string FotoLargest { get; set; }
		public string FotoMedium { get; set; }
		public string FotoSecure { get; set; }
		public object GewijzigdDatum { get; set; }
		public int GlobalId { get; set; }
		public string GroupByObjectType { get; set; }
		public bool Heeft360GradenFoto { get; set; }
		public bool HeeftBrochure { get; set; }
		public bool HeeftOpenhuizenTopper { get; set; }
		public bool HeeftOverbruggingsgrarantie { get; set; }
		public bool HeeftPlattegrond { get; set; }
		public bool HeeftTophuis { get; set; }
		public bool HeeftVeiling { get; set; }
		public bool HeeftVideo { get; set; }
		public object HuurPrijsTot { get; set; }
		public object Huurprijs { get; set; }
		public object HuurprijsFormaat { get; set; }
		public string Id { get; set; }
		public object InUnitsVanaf { get; set; }
		public bool IndProjectObjectType { get; set; }
		public object IndTransactieMakelaarTonen { get; set; }
		public bool IsSearchable { get; set; }
		public bool IsVerhuurd { get; set; }
		public bool IsVerkocht { get; set; }
		public bool IsVerkochtOfVerhuurd { get; set; }
		public int Koopprijs { get; set; }
		public string KoopprijsFormaat { get; set; }
		public int KoopprijsTot { get; set; }
		public object Land { get; set; }
		public int MakelaarId { get; set; }
		public string MakelaarNaam { get; set; }
		public string MobileURL { get; set; }
		public object Note { get; set; }

		public List<string> OpenHuis { get; set; }
		public int Oppervlakte { get; set; }
		public int Perceeloppervlakte { get; set; }
		public string Postcode { get; set; }
		public FundaExternalApiPrijsResponse Prijs { get; set; }
		public string PrijsGeformatteerdHtml { get; set; }
		public string PrijsGeformatteerdTextHuur { get; set; }
		public string PrijsGeformatteerdTextKoop { get; set; }
		public List<string> Producten { get; set; }
		public FundaExternalApiProjectResponse Project { get; set; }
		public object ProjectNaam { get; set; }
		public FundaExternalApiPromoLabelResponse PromoLabel { get; set; }

		[JsonConverter(typeof(JsonMicrosoftDateTimeConverter))]
		public DateTime PublicatieDatum { get; set; }
		public int PublicatieStatus { get; set; }
		public object SavedDate { get; set; }

		[JsonPropertyName("Soort-aanbod")]
		public string SoortaanbodType { get; set; }
		public int SoortAanbod { get; set; }
		public object StartOplevering { get; set; }
		public object TimeAgoText { get; set; }
		public object TransactieAfmeldDatum { get; set; }
		public object TransactieMakelaarId { get; set; }
		public object TransactieMakelaarNaam { get; set; }
		public int TypeProject { get; set; }
		public string URL { get; set; }
		public string VerkoopStatus { get; set; }
		public double WGS84_X { get; set; }
		public double WGS84_Y { get; set; }
		public int WoonOppervlakteTot { get; set; }
		public int Woonoppervlakte { get; set; }
		public string Woonplaats { get; set; }
		public List<int> ZoekType { get; set; }
	}

}
