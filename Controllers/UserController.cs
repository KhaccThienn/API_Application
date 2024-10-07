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
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userService.GetUsers();
        }

        [HttpGet("{id}")]
        public async Task<User> GetUser(int id)
        {
            return await _userService.GetById(id);
        }

        [HttpPost]
        [Route("create")]
        public async Task<User> CreateUser([FromForm] CreateUserDTO userDTO)
        {
            return await _userService.Insert(userDTO);
        }

        [HttpPut("{id}")]
        public async Task<User> UpdateUser(int id, [FromForm] UpdateUserDTO user)
        {
            return await _userService.Update(id, user);
        }

        [HttpPut("ChangePassword/{id}")]
        public async Task<User> UpdatePassword(int id, [FromBody] UpdatePasswordDTO user)
        {
            return await _userService.UpdatePassword(id, user);
        }

        [HttpDelete("{id}")]
        public async Task<User> DeleteUser(int id)
        {
            return await _userService.Delete(id);
        }
    }
}
