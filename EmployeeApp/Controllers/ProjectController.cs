using EmployeeApp.Models;
using EmployeeApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static EmployeeApp.Repository.ProjectRepository;
using Task = EmployeeApp.Models.Task;

namespace EmployeeApp.Controllers
{
    [ApiController]
    [Route("api/project")]
    [ApiExplorerSettings(GroupName = "Projects")]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private IprojectRepository projectRepository;

        public ProjectController(IprojectRepository _projectRepository) // dependecy injection
        {
            projectRepository = _projectRepository;
        }
        [HttpGet]
        public IActionResult getAllProjects()
        {
            return Ok(projectRepository.GetProjects());
        }
        [HttpPost]
        public IActionResult createTask(Project project)
        {
            return Ok(projectRepository.AddProject(project));
        }
        [HttpPut]
        public IActionResult updateTask(Project project)
        {
            return Ok(projectRepository.UpdateProject(project));
        }

        [HttpDelete("{projectID}")]
        public IActionResult deleteTask(int projectID)
        {
            var deletedTask = projectRepository.RemoveProject(projectID);
            if (deletedTask == null)
            {
                return NotFound(new { message = $"Task with ID {projectID} not found." });
            }
            return Ok(new { message = $"Task with ID {projectID} has been deleted successfully.", deletedTask });
        }

        [HttpPost]
        [Route("/add-user")]
        public IActionResult AddUserToProject(int userId, int projectId)
        {
            try
            {
                var project = projectRepository.AddUserToProject(userId, projectId);
                return Ok(project);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (BadRequestException ex)
            {
                return Conflict(new { message = ex.Message });
            }

        }

    }
}
    
    

