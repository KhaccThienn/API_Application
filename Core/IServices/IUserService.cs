namespace API_Application.Core.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetById(int id);
        Task<User> Insert(CreateUserDTO u);
        Task<User> Update(int id, UpdateUserDTO u);
        Task<User> UpdatePassword(int id, UpdatePasswordDTO model);
        Task<User> Delete(int id);
    }
}
