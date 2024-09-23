namespace API_Application.Core.Database.InMemory
{
    public class ComicMemory
    {
        public Dictionary<string, Comic> ComicMem { get; set; }
        public ComicMemory()
        {
            ComicMem = new Dictionary<string, Comic>();
        }
    }
}
