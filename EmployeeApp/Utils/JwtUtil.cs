using EmployeeApp.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeApp.Utils
{
    public class JwtUtil
    {
        public static string GenerateToken(User user)
        {

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim("id", user.UserId.ToString()),
            new Claim(ClaimTypes.Role, user.Role),
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecretKey"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "MyIssuer",
                audience: "MyAudience",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
