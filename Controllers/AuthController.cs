namespace API_Application.Controllers
{
    [EnableCors("AllowOrigin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly DbComicAppContext _context;

        public AuthController(IAuthService authService, DbComicAppContext context)
        {
            _authService = authService;
            _context = context;
        }

        // Login method
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (string.IsNullOrEmpty(loginDTO.Email) || string.IsNullOrEmpty(loginDTO.Password))
            {
                return BadRequest("Invalid input");
            }

            var userFound = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginDTO.Email);

            if (userFound == null)
            {
                return BadRequest("User Not Found");
            }

            // Use BCrypt to verify the password
            if (!BCrypt.Net.BCrypt.Verify(loginDTO.Password, userFound.Password))
            {
                return BadRequest("Invalid password");
            }

            return Ok(userFound);
        }

        // Register method
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            if (string.IsNullOrEmpty(registerDTO.Name) || string.IsNullOrEmpty(registerDTO.Email) || string.IsNullOrEmpty(registerDTO.Password))
            {
                return BadRequest("Invalid input");
            }

            if (string.IsNullOrEmpty(registerDTO.Role))
            {
                registerDTO.Role = "USER"; // Default role if not provided
            }

            // Check if user already exists
            var userFound = await _context.Users.AnyAsync(u => u.Email == registerDTO.Email);
            if (userFound)
            {
                return BadRequest("User with this email already exists");
            }

            // Hash the password
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password);

            // Create new user
            var newUser = new User
            {
                Name     = registerDTO.Name,
                Email    = registerDTO.Email,
                Password = passwordHash,
                Role     = registerDTO.Role
            };

            // Add user to database
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(newUser);
        }
    }
}
