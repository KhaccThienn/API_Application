namespace API_Application.Core.Database.InMemory
{
    public class EpisodeMemory
    {
        public Dictionary<string, Episode> EpisodeMem { get; set; }
        public EpisodeMemory()
        {
            EpisodeMem = new Dictionary<string, Episode>();
        }
    }
}
