namespace Application.FundaExternalApiModels
{
	public class FundaExternalApiPromoLabelResponse
	{
		public bool HasPromotionLabel { get; set; }
		public List<string> PromotionPhotos { get; set; }
		public List<string> PromotionPhotosSecure { get; set; }
		public int PromotionType { get; set; }
		public int RibbonColor { get; set; }
		public object RibbonText { get; set; }
		public string Tagline { get; set; }
	}

}
