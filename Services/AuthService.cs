namespace API_Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;

        public AuthService(IAuthRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            try
            {
                var user = _repository.Login(loginDTO);

                if (user == null)
                {
                    return new BadRequestObjectResult("Invalid credentials");
                }

                return new OkObjectResult(user);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return new BadRequestObjectResult(new { message = "Login failed", details = ex.Message });
            }
        }

        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            try
            {
                if (registerDTO.Role == null)
                {
                    registerDTO.Role = "USER";
                }
                var user = await _repository.Register(registerDTO);

                return new OkObjectResult(user);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return new BadRequestObjectResult(new { message = "Register failed", details = ex.Message });
            }
        }
    }
}
