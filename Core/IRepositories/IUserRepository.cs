
namespace API_Application.Core.IRepositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> Insert(User u);
        Task<User> GetById(int id);
        Task<User> Update(int id, User u);
        Task<User> Delete(int id);
    }
}
