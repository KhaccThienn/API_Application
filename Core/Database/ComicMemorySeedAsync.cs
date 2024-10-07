using API_Application.Core.Database.InMemory;

namespace API_Application.Core.Database
{
    public class ComicMemorySeedAsync
    {
        public async Task SeedAsync(ComicMemory memory, DbComicAppContext dbContext)
        {
            var comics = await dbContext.Comics.ToListAsync();
            if (comics.Count > 0)
            {
                foreach (var comic in comics)
                {
                    memory.ComicMem.Add(comic.Id.ToString(), comic);
                }
            }
        }
    }
}
