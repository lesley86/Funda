namespace Funda
{
	public class MakelaarWithObjectCountResponseModel
	{
		public string MakelaarId { get; set; }

		public string MakelaarName { get; set; }

		public int ObjectCount { get; set; }

		public MakelaarWithObjectCountResponseModel(string makelaarId, string makelaarName, int objectCount)
		{
			MakelaarId = makelaarId;
			MakelaarName = makelaarName;
			ObjectCount = objectCount;
		}
	}
}
