using API_Application.Core.Models.DTOs;

namespace API_Application.Core.IRepositories
{
    public interface IAuthRepository
    {
        Task Login(LoginDTO loginDTO);
        Task Register(RegisterDTO registerDTO);
    }
}
