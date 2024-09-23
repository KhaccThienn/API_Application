using API_Application.Core.Database.InMemory;

namespace API_Application.Core.Database
{
    public class ActorMemorySeedAsync
    {
        public async Task SeedAsync(ActorMemory memory, DbComicAppContext dbContext)
        {
            var actors = await dbContext.Actors.ToListAsync();
            if (actors.Count > 0)
            {
                foreach (var actor in actors)
                {
                    memory.ActorMem.Add(actor.Id.ToString(), actor);
                }
            }
        }
    }
}
