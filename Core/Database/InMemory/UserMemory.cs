
namespace API_Application.Core.Database.InMemory
{
    public class UserMemory
    {
        public Dictionary<string, User> Memory { get; set; }
        public UserMemory()
        {
            Memory = new Dictionary<string, User>();
        }
    }
}
