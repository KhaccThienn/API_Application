
namespace API_Application.Core.IRepositories
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User Insert(User u);
        User Update(int id, User u);
        User Delete(int id);
    }
}
