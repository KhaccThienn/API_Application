
namespace API_Application.Core.Database.InMemory
{
    public class UserMemory
    {
        public Dictionary<string, User> UserMem { get; set; }
        public UserMemory()
        {
            UserMem = new Dictionary<string, User>();
        }
    }
}
