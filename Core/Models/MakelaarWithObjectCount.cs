namespace Core.Models
{
    public class MakelaarWithObjectCount
    {
        public string MakelaarId { get; set; }

        public string MakelaarName { get; set; }

        public int ObjectCount { get; set; }

        public MakelaarWithObjectCount(string makelaarId, string makelaarName, int objectCount)
        {
            MakelaarId = makelaarId;
            MakelaarName = makelaarName;
            ObjectCount = objectCount;
        }
    }
}
