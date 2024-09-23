using API_Application.Core.Database.InMemory;

namespace API_Application.Core.Database
{
    public class DirectorMemorySeedAsync
    {
        public async Task SeedAsync(DirectorMemory memory, DbComicAppContext dbContext)
        {
            var directors = await dbContext.Directors.ToListAsync();
            if (directors.Count > 0)
            {
                foreach (var director in directors)
                {
                    memory.DirectorMem.Add(director.Id.ToString(), director);
                }
            }
        }
    }
}
