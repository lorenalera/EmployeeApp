using EmployeeApp.Data;
using EmployeeApp.Models;
using Microsoft.EntityFrameworkCore;
using Task = EmployeeApp.Models.Task;

namespace EmployeeApp.Repository
{
    public class TaskRepository : ItaskRepository
    {

        private readonly ApplicationDbContext _context;

         public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        

        public Task addTask(Task task)
        {
             var existingProject = _context.Project.FirstOrDefault(p => p.projectId == task.ProjectId);
            if (existingProject == null)
            {
                task.ProjectId = null;
            }
            task.Project = existingProject;
            _context.Task.Add(task);
            _context.SaveChanges();
            return task;
        }
        public Task MarkTasksAsComplete(int taskID)
        {
            var task = GetTaskById(taskID);
            task.status = "COMPLETED";

            _context.Task.Update(task);
            _context.SaveChanges();

            return task;
        }
        public ICollection<Task> GetTasks()
        {
            return _context.Task.ToList();
        }

        public Task GetTaskById(int id)
        {
            return _context.Task.First(t => t.taskId == id); ;
        }

        public ICollection<Task> GetTasksByProjectId(int projectId)
        {
            var tasks = _context.Task
            .Where(t => t.ProjectId == projectId)
            .ToList();
            return tasks;
        }

        public Task removeTask(int taskID)
        {
            var existingTask = _context.Task.FirstOrDefault(t => t.taskId == taskID);
            if (existingTask == null)
            {
                return null;
            }

            _context.Task.Remove(existingTask);
            _context.SaveChanges();
            return existingTask;
        }

        public Task updateTask(Task task)
        {
            var existingTask = _context.Task.AsNoTracking().FirstOrDefault(u => u.taskId == task.taskId);
            if (existingTask == null)
            {
                throw new ArgumentException("Task not found");
            }

            _context.Task.Update(task);
            _context.SaveChanges();
            return task;
        }
    }
}

