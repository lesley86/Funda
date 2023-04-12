namespace Core.Models
{
	public class MakelaarWithTuinAndLocation
	{
		public IEnumerable<DrinkCategory> MakelaarWithObjectsForSaleInLocation { get; init; }

		public IEnumerable<DrinkCategory> MakelaarWithObjectsForSaleInLocationWithTuin { get; init; }

		public MakelaarWithTuinAndLocation(
			IEnumerable<DrinkCategory> makelaarWithObjectsForSaleInLocation,
			IEnumerable<DrinkCategory> MakelaarWithObjectsForSaleInLocationWithTuin)
		{
			MakelaarWithObjectsForSaleInLocation = makelaarWithObjectsForSaleInLocation;
			this.MakelaarWithObjectsForSaleInLocationWithTuin = MakelaarWithObjectsForSaleInLocationWithTuin;
		}
	}
}
