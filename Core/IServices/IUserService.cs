namespace API_Application.Core.IServices
{
    public interface IUserService
    {
        List<User> GetUsers();
        User Insert(User u);
        User Update(int id, User u);
        User Delete(int id);
    }
}
