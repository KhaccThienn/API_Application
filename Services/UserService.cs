using API_Application.Core.IRepositories;
using API_Application.Core.IServices;
using API_Application.Core.Models.DTOs;
using Microsoft.AspNetCore.Http;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;

namespace API_Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IHttpContextAccessor _httpContext;

        public UserService(IUserRepository repository, IHttpContextAccessor accessor)
        {
            _repository = repository;
            _httpContext = accessor;
        }

        public User Delete(int id)
        {
            var userFound = _repository.GetUsers().FirstOrDefault(x => x.Id == id);
            if (userFound != null)
            {
                try
                {
                    var oldFileName = userFound.Avatar.Split($"{_httpContext.HttpContext.Request.Host.Value}/uploads/");
                    Console.WriteLine($"Old File: {oldFileName}");
                    var pathOldFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", oldFileName[1]);
                    if (System.IO.File.Exists(pathOldFile))
                    {
                        System.IO.File.Delete(pathOldFile);
                    }
                }
                catch
                {
                    throw;
                }
            }
            return _repository.Delete(id);
        }

        public List<User> GetUsers()
        {
            return _repository.GetUsers();
        }

        public User Insert(User u)
        {
            return _repository.Insert(u);
        }

        public User Insert(CreateUserDTO u)
        {
            if (u.ImageFile != null)
            {
                // Generate a unique filename for the uploaded file
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(u.ImageFile.FileName);
                var filePath = Path.Combine("wwwroot/uploads", fileName);

                // Save the file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    u.ImageFile.CopyTo(stream);
                }

                // Update the avatar field with the file path
                u.Avatar = $"https://{_httpContext.HttpContext.Request.Host.Value}/uploads/{fileName}";
            }
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(u.Password);
            var newUser = new User
            {
                Name = u.Name,
                Email = u.Email,
                Password = hashedPassword,
                Avatar = u.Avatar,
                Role = u.Role,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                UpdatedAt = DateOnly.FromDateTime(DateTime.Now)
            };

            return _repository.Insert(newUser);
        }

        public User Update(int id, UpdateUserDTO u)
        {
            var userFound = _repository.GetUsers().FirstOrDefault(x => x.Id == id);
            if (u.ImageFile != null)
            {
                try
                {
                    // Generate a unique filename for the uploaded file
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(u.ImageFile.FileName);
                    var filePath = Path.Combine("wwwroot/uploads", fileName);

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", u.ImageFile.FileName);
                    var oldFileName = userFound.Avatar.Split($"{_httpContext.HttpContext.Request.Host.Value}/uploads/");
                    Console.WriteLine($"Old File: {oldFileName}");
                    var pathOldFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", oldFileName[1]);
                    if (System.IO.File.Exists(pathOldFile))
                    {
                        System.IO.File.Delete(pathOldFile);
                    }

                    // Save the file to the server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        u.ImageFile.CopyTo(stream);
                    }

                    // Update the avatar field with the file path
                    u.Avatar = $"https://{_httpContext.HttpContext.Request.Host.Value}/uploads/{fileName}";
                } catch
                {
                    throw;
                }
                
            } else
            {
                u.Avatar = userFound.Avatar;
            }

            var user = new User
            {
                Name = u.Name,
                Email = u.Email,
                Password = userFound.Password,
                Avatar = u.Avatar,
                Role = u.Role,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                UpdatedAt = DateOnly.FromDateTime(DateTime.Now)
            };
            return _repository.Update(id, user);
        }

        public User UpdatePassword(int id, UpdatePasswordDTO model)
        {
            var userFound = _repository.GetById(id);
            if (userFound == null)
            {
                throw new Exception("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(model.OldPassword, userFound.Password))
            {
                throw new Exception("Old password is incorrect");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);

            var updatedUser = new User
            {
                Id = userFound.Id,
                Name = userFound.Name,
                Email = userFound.Email,
                Password = hashedPassword,
                Avatar = userFound.Avatar,
                Role = userFound.Role,
                CreatedAt = userFound.CreatedAt,
                UpdatedAt = DateOnly.FromDateTime(DateTime.Now)
            };

            return _repository.Update(id, updatedUser);
        } 
    }
}
