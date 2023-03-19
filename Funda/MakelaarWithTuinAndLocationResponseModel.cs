namespace Core.Models
{
	public class MakelaarWithTuinAndLocationResponseModel
	{
		public IEnumerable<MakelaarWithObjectCount> MakelaarWithObjectsForSaleInLocation { get; init; }

		public IEnumerable<MakelaarWithObjectCount> MakelaarWithObjectsForSaleInLocationWithTuin { get; init; }

		public MakelaarWithTuinAndLocationResponseModel(
			IEnumerable<MakelaarWithObjectCount> makelaarWithObjectsForSaleInLocation,
			IEnumerable<MakelaarWithObjectCount> MakelaarWithObjectsForSaleInLocationWithTuin)
		{
			MakelaarWithObjectsForSaleInLocation = makelaarWithObjectsForSaleInLocation;
			this.MakelaarWithObjectsForSaleInLocationWithTuin = MakelaarWithObjectsForSaleInLocationWithTuin;
		}
	}
}
