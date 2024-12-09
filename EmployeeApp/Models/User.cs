using System.ComponentModel.DataAnnotations;

namespace EmployeeApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
        public string ProfilePicture { get; set; }
        public ICollection<UserProject> userProjects  { get; set; }
        
    }
}
