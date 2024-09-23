using Microsoft.AspNetCore.Mvc;

namespace API_Application.Core.IServices
{
    public interface IAuthService
    {
        Task<IActionResult> Login(LoginDTO loginDTO);
        Task<IActionResult> Register(RegisterDTO registerDTO);
    }
}
