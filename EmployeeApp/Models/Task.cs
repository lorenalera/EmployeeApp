using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeApp.Models
{
    public class Task
    {
        [Key]
        public int? taskId { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        [RegularExpression("^(OPENED|COMPLETED)$", ErrorMessage = "Status must be 'opened' or 'completed'.")]
        public string status { get; set; }
        public int? ProjectId { get; set; } // Foreign Key
        [ForeignKey("ProjectId")]
        public Project? Project { get; set; } = null!;
    }
}
