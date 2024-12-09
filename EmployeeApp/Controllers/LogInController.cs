using EmployeeApp.Data;
using EmployeeApp.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace EmployeeApp.Controllers
{
    [Route("api/login")]
    [ApiExplorerSettings(GroupName = "Login")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public LogInController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult authenticate (AuthRequest credentials)
        {
            
            var username = credentials.username;
            var password = credentials.password;
            var user = _context.Users.SingleOrDefault(u => u.UserName == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return Unauthorized();
            }

            var token = JwtUtil.GenerateToken(user);
            return Ok(new { Token = token });
        }
    }

    public class AuthRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
