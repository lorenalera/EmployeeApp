using EmployeeApp.Models;
using static EmployeeApp.Repository.UserRepository;

namespace EmployeeApp.Repository
{
    public interface IUserRepository
    {
        public User addUser(User user);
        public User removeUser(User user);
        public User updateUser(User user);
        public ICollection<UserDto> GetUsers();

    }
}
