using API_Application.Core.Database.InMemory;

namespace API_Application.Core.Database
{
    public class GenreMemorySeedAsync
    {
        public async Task SeedAsync(GenreMemory memory, DbComicAppContext dbContext)
        {
            var genres = await dbContext.Genres.ToListAsync();
            if (genres.Count > 0)
            {
                foreach (var gen in genres)
                {
                    memory.GenreMem.Add(gen.Id.ToString(), gen);
                }
            }
        }
    }
}
