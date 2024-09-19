using API_Application.Core.Database;
using API_Application.Core.Database.InMemory;
using API_Application.Core.IRepositories;
using Microsoft.IdentityModel.Tokens;

namespace API_Application.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserRepository> _logger;
        private readonly DbComicAppContext _db;
        private readonly UserMemory _inMem;

        public UserRepository(IConfiguration configuration, ILogger<UserRepository> logger, DbComicAppContext context, UserMemory inMem)
        {
            _configuration = configuration;
            _logger = logger;
            _db = context;
            _inMem = inMem;
        }

        public User Delete(int id)
        {
            var user = new User();
            var u = _inMem.UserMem.FirstOrDefault(x => x.Key.Equals(id.ToString()));
            if (u.Value != null)
            {
                try
                {
                    user = u.Value;

                    _inMem.UserMem.Remove(u.Key);

                    _db.Users.Remove(user);
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
            
            return user;
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            try
            {
                users = _inMem.UserMem.Values.ToList();
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }

            return users;
        }

        public User Insert(User u)
        {
            try
            {
                // insert into memory
                _inMem.UserMem.Add(u.Id.ToString(), u);

                // insert into db
                _db.Users.Add(u);
                _db.SaveChanges();

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return u;
        }

        public User Update(int id, User u)
        {
            var user = new User();
            var oldUser = _inMem.UserMem.FirstOrDefault(x => x.Key.Equals(id.ToString()));
            user.Id = oldUser.Value.Id;
            user.Name = u.Name ;
            user.Email = u.Email;
            user.Password = u.Password;
            user.Avatar = u.Avatar.Length > 0 ? u.Avatar : oldUser.Value.Avatar;
            user.Role = u.Role;
            user.CreatedAt = oldUser.Value.CreatedAt;
            user.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);

            try
            {
                _db.Users.Update(user);
                _db.SaveChanges();

                _inMem.UserMem.Remove(id.ToString());
                _inMem.UserMem.Add(u.Id.ToString(), u);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

            return user;
        }
    }
}
