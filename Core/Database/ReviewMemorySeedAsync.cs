namespace API_Application.Core.Database
{
    public class ReviewMemorySeedAsync
    {
        public async Task SeedAsync(ReviewMemory memory, DbComicAppContext dbContext)
        {
            var reviews = await dbContext.Reviews.ToListAsync();
            if (reviews.Count > 0)
            {
                foreach (var review in reviews)
                {
                    memory.ReviewMem.Add(review.Id.ToString(), review);
                }
            }
        }
    }
}
