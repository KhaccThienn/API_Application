
namespace API_Application.Core.IRepositories
{
    public interface IAuthRepository
    {
        User Login(LoginDTO loginDTO);
        Task<User> Register(RegisterDTO registerDTO);
    }
}
