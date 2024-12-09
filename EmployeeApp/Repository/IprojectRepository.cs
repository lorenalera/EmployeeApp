using EmployeeApp.Models;
using Task = EmployeeApp.Models.Task;

namespace EmployeeApp.Repository
{
    public interface IprojectRepository
    {
        public Project AddProject(Project project);
        public Project RemoveProject(int projectId);
        public Project UpdateProject(Project project);
        public ICollection<Project> GetProjects();

        public Project AddUserToProject(int userId, int projectId);
    }
}
