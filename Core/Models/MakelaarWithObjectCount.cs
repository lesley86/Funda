namespace Core.Models
{
    public class MakelaarWithObjectCount
    {
        public string MakelaarId { get; set; }

        public string MakelaarName { get; set; }

        public int ObjectCount { get; set; }

        public bool Tuin { get; set; }
        public MakelaarWithObjectCount(string makelaarId, string makelaarName, int objectCount, bool tuin)
        {
            MakelaarId = makelaarId;
            MakelaarName = makelaarName;
            ObjectCount = objectCount;
			Tuin = tuin;
		}
    }
}
