using API_Application.Core.IServices;
using API_Application.Core.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService service)
        {
            _userService = service;
        }

        [HttpGet]
        public List<User> GetUsers()
        {
            return _userService.GetUsers();
        }

        [HttpPost]
        [Route("create")]
        public User CreateUser([FromForm] CreateUserDTO userDTO)
        {
            if (userDTO.ImageFile != null)
            {
                // Generate a unique filename for the uploaded file
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(userDTO.ImageFile.FileName);
                var filePath = Path.Combine("wwwroot/uploads", fileName);

                // Save the file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    userDTO.ImageFile.CopyTo(stream);
                }

                // Update the avatar field with the file path
                userDTO.Avatar = $"https://{HttpContext.Request.Host.Value}/uploads/{fileName}"; ;
            }
            return _userService.Insert(userDTO);
        }

        [HttpPut("{id}")]
        public User UpdateUser(int id, [FromForm] UpdateUserDTO user)
        {
            return _userService.Update(id, user);
        }

        [HttpDelete("{id}")]
        public User DeleteUser(int id)
        {
            return _userService.Delete(id);
        }
    }
}
