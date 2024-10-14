namespace API_Application.Core.Database
{
    public class ImageMemorySeedAsync
    {
        public async Task SeedAsync(ImagesMemory memory, DbComicAppContext dbContext)
        {
            var images = await dbContext.Images.ToListAsync();
            if (images.Count > 0)
            {
                foreach (var image in images)
                {
                    memory.ImagesInMem.Add(image.Id.ToString(), image);
                }
            }
        }
    }
}
