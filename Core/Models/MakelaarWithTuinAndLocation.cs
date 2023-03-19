namespace Core.Models
{
	public class MakelaarWithTuinAndLocation
	{
		public IEnumerable<MakelaarWithObjectCount> MakelaarWithObjectsForSaleInLocation { get; init; }

		public IEnumerable<MakelaarWithObjectCount> MakelaarWithObjectsForSaleInLocationWithTuin { get; init; }

		public MakelaarWithTuinAndLocation(
			IEnumerable<MakelaarWithObjectCount> makelaarWithObjectsForSaleInLocation,
			IEnumerable<MakelaarWithObjectCount> MakelaarWithObjectsForSaleInLocationWithTuin)
		{
			MakelaarWithObjectsForSaleInLocation = makelaarWithObjectsForSaleInLocation;
			this.MakelaarWithObjectsForSaleInLocationWithTuin = MakelaarWithObjectsForSaleInLocationWithTuin;
		}
	}
}
