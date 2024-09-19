using API_Application.Core.Models.DTOs;

namespace API_Application.Core.IServices
{
    public interface IUserService
    {
        List<User> GetUsers();
        User Insert(CreateUserDTO u);
        User Update(int id, UpdateUserDTO u);
        User Delete(int id);
    }
}
