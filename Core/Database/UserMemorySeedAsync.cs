using API_Application.Core.Database.InMemory;

namespace API_Application.Core.Database
{
    public class UserMemorySeedAsync
    {
        public async Task SeedAsync(UserMemory memory, DbComicAppContext dbContext)
        {
            var users = await dbContext.Users.ToListAsync();
            if (users.Count > 0)
            {
                foreach (var user in users)
                {
                    memory.UserMem.Add(user.Id.ToString(), user);
                }
            }
        }
    }
}
