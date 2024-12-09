using EmployeeApp.Models;
using EmployeeApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Task = EmployeeApp.Models.Task;

namespace EmployeeApp.Controller
{
    [ApiController]
    [Route("api/tasks")]
    [ApiExplorerSettings(GroupName = "Tasks")]

    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ItaskRepository taskRepository;

        public TaskController(ItaskRepository _taskRepository) // dependecy injection
        {
            taskRepository = _taskRepository;
        }
        [HttpGet]
        public IActionResult getAllTasks() {
            return Ok(taskRepository.GetTasks());
    }
        [HttpPost]
        public IActionResult createTask(Task task)
        {
            return Ok(taskRepository.addTask(task));
        }

        [HttpPut]
        public IActionResult updateTask(Task task)
        {
            return Ok(taskRepository.updateTask(task));
        }

        [HttpDelete("{taskID}")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult deleteTask(int taskID)
        {
            var deletedTask = taskRepository.removeTask(taskID);
            if (deletedTask == null)
            {
                return NotFound(new { message = $"Task with ID {taskID} not found." });
            }
            return Ok(new { message = $"Task with ID {taskID} has been deleted successfully.", deletedTask });
        }

        [HttpPut]
        [Route("/complete")]
        public IActionResult markTaskComplete(int taskID)
        {
            var task = taskRepository.MarkTasksAsComplete(taskID);
            return Ok(task);
        }

        [HttpGet("{projectId}/tasks")]
        public IActionResult AddTasksToProject(int projectId)
        {
            var tasks = taskRepository.GetTasksByProjectId(projectId);
            return Ok(tasks);
        }


    }
}
