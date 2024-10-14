namespace API_Application.Core.Database.InMemory
{
    public class ReviewMemory
    {
        public Dictionary<string, Review> ReviewMem { get; set; }
        public ReviewMemory()
        {
            ReviewMem = new Dictionary<string, Review>();
        }
    }
}
