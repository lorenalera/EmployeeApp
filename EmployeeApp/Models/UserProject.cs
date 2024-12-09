using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeApp.Models
{
    
    public class UserProject
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; } 
        public User User { get; set; }

        [Key, Column(Order = 1)]
        public int ProjectId { get; set; } 
        public Project Project { get; set; } 

    }
}
