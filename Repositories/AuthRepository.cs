using API_Application.Core.Database.InMemory;
using API_Application.Core.Database;
using API_Application.Core.IRepositories;

namespace API_Application.Repositories
{
    public class AuthRepository : IAuthRepository

    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthRepository> _logger;
        private readonly DbComicAppContext _db;
        private readonly UserMemory _inMem;

        public AuthRepository(IConfiguration configuration, ILogger<AuthRepository> logger, DbComicAppContext db, UserMemory memory)
        {
            _configuration = configuration;
            _logger = logger;
            _db = db;
            _inMem = memory;
        }

        public User Login(LoginDTO loginDTO)
        {
            var user = _inMem.UserMem.FirstOrDefault(u => u.Value.Email == loginDTO.Email);
            
            if (user.Value == null)
            {
                throw new Exception("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Value.Password))
            {
                throw new Exception("Invalid password");
            }

            return user.Value;
        }

        public async Task<User> Register(RegisterDTO registerDTO)
        {
            // Check if user already exists
            if (_inMem.UserMem.Any(u => u.Value.Email == registerDTO.Email))
            {
                throw new Exception("User with this email already exists");
            }

            // Hash the password
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password, 12);

            // Create new user
            var newUser = new User
            {
                Name = registerDTO.Name,
                Email = registerDTO.Email,
                Password = passwordHash,
                Role = registerDTO.Role
            };

            // Add user to database
            var user = await _db.Users.AddAsync(newUser);
            _db.SaveChanges();

            _inMem.UserMem.Add(user.Entity.Id.ToString(), user.Entity);

            return user.Entity;
        }
        
    }
}
