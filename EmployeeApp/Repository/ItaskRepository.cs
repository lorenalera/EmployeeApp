using EmployeeApp.Models;
using Task = EmployeeApp.Models.Task;

namespace EmployeeApp.Repository
{
    public interface ItaskRepository
    {
        public Task addTask(Task task);
        public Task removeTask(int taskID);
        public Task updateTask(Task task);
        public ICollection<Task> GetTasks();
        public Task MarkTasksAsComplete(int taskId);
        public ICollection<Task> GetTasksByProjectId(int projectId);
    }
}
