namespace API_Application.Core.Database.InMemory
{
    public class ActorMemory
    {
        public Dictionary<string, Actor> ActorMem { get; set; }
        public ActorMemory()
        {
            ActorMem = new Dictionary<string, Actor>();
        }
    }
}
