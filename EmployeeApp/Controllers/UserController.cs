using EmployeeApp.Models;
using EmployeeApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.Controllers
{
    [Route("api/user")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Users")]

    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository _userRepository) // dependecy injection
        {
            userRepository = _userRepository;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public IActionResult AddUser(User user)
        {
            return Ok(userRepository.addUser(user));
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult GetUsers()
        {
            return Ok(userRepository.GetUsers());
        }

    }
}
