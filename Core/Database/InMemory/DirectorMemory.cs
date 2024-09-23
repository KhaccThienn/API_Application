namespace API_Application.Core.Database.InMemory
{
    public class DirectorMemory
    {
        public Dictionary<string, Director> DirectorMem { get; set; }
        public DirectorMemory()
        {
            DirectorMem = new Dictionary<string, Director>();
        }
    }
}
