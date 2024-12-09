using System.ComponentModel.DataAnnotations;

namespace EmployeeApp.Models
{
    public class Project
    {
        [Key]
        public int projectId { get; set; }
        [Required]
        public string Name { get; set; }
        
        public  ICollection<Task> Tasks { get; set; }

        public ICollection<UserProject> userProjects { get; set; }
    }
}
