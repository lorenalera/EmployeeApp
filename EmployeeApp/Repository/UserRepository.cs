using EmployeeApp.Data;
using EmployeeApp.Models;

namespace EmployeeApp.Repository
{
    public class UserRepository : IUserRepository {

        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public User addUser(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.User.Add(user);
            _context.SaveChanges();
            return user;
        }

        public ICollection<UserDto> GetUsers()
        {
            return _context.User
         .Select(user => new UserDto
         {
             UserId = user.UserId,
             Name = user.Name,
             Username = user.UserName,
             Role = user.Role
         })
         .ToList();
        }

        public User removeUser(User user)
        {
            var existingUser = _context.User.FirstOrDefault(u => u.UserId == user.UserId);
            if (existingUser == null)
            {
                throw new ArgumentException("User not found");
            }

            _context.User.Remove(existingUser);
            _context.SaveChanges();
            return existingUser;
        }

        public User updateUser(User user)
        {
            var existingUser = _context.User.FirstOrDefault(u => u.UserId == user.UserId);
            if (existingUser == null)
            {
                throw new ArgumentException("User not found");
            }

            _context.User.Remove(existingUser);
            _context.SaveChanges();
            return existingUser;
            
        }

        public class UserDto
        {
            public int UserId { get; set; }
            public string Name { get; set; }
            public string Username { get; set; }
            public string Role { get; set; }
        }
    }
}
