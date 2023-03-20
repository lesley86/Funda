namespace Core.Entities
{
	public class HouseEntity
    {
		public Guid Id { get; set; }

		public int ObjectCount { get; set; }

		public string MakelaarId { get; set; }

		public string MakelaarName { get; set;  }

		public bool Tuin { get; set; }


		public HouseEntity()
		{

		}

		public HouseEntity(string makelaarId, string makelaarName, int objectCount, bool tuin)
		{
			MakelaarId = makelaarId;
			MakelaarName = makelaarName;
			ObjectCount = objectCount;
			Tuin = tuin;
			Id = Guid.NewGuid();
		}

		public void StoreUpdatedInformation(int objectCount)
		{
			ObjectCount = objectCount;
		}
	}
}
