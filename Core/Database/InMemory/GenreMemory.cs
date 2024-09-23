namespace API_Application.Core.Database.InMemory
{
    public class GenreMemory
    {
        public Dictionary<string, Genre> GenreMem { get; set; }
        public GenreMemory()
        {
            GenreMem = new Dictionary<string, Genre>();
        }
    }
}
