using API_Application.Core.IServices;
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
            return _userService.Insert(userDTO);
        }

        [HttpPut("{id}")]
        public User UpdateUser(int id, [FromForm] UpdateUserDTO user)
        {
            return _userService.Update(id, user);
        }

        [HttpPut("ChangePassword/{id}")]
        public User UpdatePassword(int id, [FromBody] UpdatePasswordDTO user)
        {
            return _userService.UpdatePassword(id, user);
        }

        [HttpDelete("{id}")]
        public User DeleteUser(int id)
        {
            return _userService.Delete(id);
        }


    }
}
