namespace API_Application.Core.Database
{
    public class EpisodeMemorySeedAsync
    {
        public async Task SeedAsync(EpisodeMemory memory, DbComicAppContext dbContext)
        {
            var eps = await dbContext.Episodes.ToListAsync();
            if (eps.Count > 0)
            {
                foreach (var ep in eps)
                {
                    memory.EpisodeMem.Add(ep.Id.ToString(), ep);
                }
            }
        }
    }
}
