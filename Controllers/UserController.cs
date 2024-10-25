using API_Application.Core.Models;
using NuGet.Protocol.Core.Types;

namespace API_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DbComicAppContext   _context;
        private readonly IWebHostEnvironment _env;

        public UserController(DbComicAppContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // Get all users
        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users
                .Include(u => u.Reviews)  // Eagerly load reviews if needed
                .ToListAsync();
        }

        // Get user by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.Reviews)  // Eagerly load reviews if needed
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return NotFound();

            return user;
        }

        [HttpGet("onlyUser/{id}")]
        public async Task<ActionResult<User>> GetOnlyUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // Create a new user
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<User>> CreateUser([FromForm] CreateUserDTO userDTO)
        {
            if (true)
            {
                
            }
            if (userDTO.ImageFile != null)
            {
                var file     = userDTO.ImageFile;
                // Generate  a unique filename for the uploaded file
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine("wwwroot/uploads", fileName);

                // Save the file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                userDTO.Avatar = $"uploads/{fileName}";
            }
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
            var user = new User
            {
                Name      = userDTO.Name,
                Email     = userDTO.Email,
                Password  = hashedPassword, 
                Role      = userDTO.Role,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                UpdatedAt = DateOnly.FromDateTime(DateTime.Now),
                Avatar    = userDTO.Avatar
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // Update user information
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, [FromForm] UpdateUserDTO userDTO)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();
            user.Name        = userDTO.Name ?? user.Name;
            user.Email       = userDTO.Email ?? user.Email;
            user.Role        = userDTO.Role ?? user.Role;
            user.Password    = !string.IsNullOrEmpty(userDTO.Password) ? BCrypt.Net.BCrypt.HashPassword(userDTO.Password) : user.Password;

            user.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);

            // Update avatar if a new image is provided
            if (userDTO.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(userDTO.OldImage))
                {
                    DeleteOldAvatar(userDTO.OldImage);
                }
                user.Avatar = await SaveAvatarFile(userDTO.ImageFile);
            }

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // Change password
        [HttpPut("ChangePassword/{id}")]
        public async Task<ActionResult> UpdatePassword(int id, [FromBody] UpdatePasswordDTO passwordDTO)
        {
            var userFound = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userFound == null)
            {
                return BadRequest("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(passwordDTO.OldPassword, userFound.Password))
            {
                return BadRequest("Old password is incorrect");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(passwordDTO.NewPassword);

            userFound.Password = hashedPassword;

            _context.Users.Update(userFound);
            _context.SaveChanges();

            return Ok(userFound);
        }

        // Delete user by ID
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            if (!string.IsNullOrEmpty(user.Avatar))
            {
                DeleteOldAvatar(user.Avatar);
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // Helper methods for file handling
        private async Task<string?> SaveAvatarFile(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return null;

            // Generate a unique filename for the uploaded file
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine("wwwroot/uploads", fileName);

            // Save the file to the server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"uploads/{fileName}";
        }

        private void DeleteOldAvatar(string fileName)
        {
            var filePath = Path.Combine(_env.WebRootPath, "avatars", fileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
