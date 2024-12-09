using EmployeeApp.Data;
using EmployeeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Task = EmployeeApp.Models.Task;

namespace EmployeeApp.Repository
{
        public class ProjectRepository : IprojectRepository
        {

            private readonly ApplicationDbContext _context;

            public ProjectRepository(ApplicationDbContext context)
            {
                _context = context;
            }
           
            public Project AddProject(Project project)
        {
             _context.Project.Add(project);
            _context.SaveChanges();
            return project;
        }

            public ICollection<Project> GetProjects()
            {
               return _context.Project.ToList();
            }

            public Project GetProjectById(int id)
            {
                return _context.Project.First(t => t.projectId == id); ;
            }
        public Project AddTasksToProject(int projectId, ICollection<Task> tasks)
        {
            // Find the project by ID
            var existingProject = _context.Project.FirstOrDefault(p => p.projectId == projectId);
            if (existingProject == null)
            {
                throw new ArgumentException("Project not found");
            }

            // Add each task to the project
            foreach (var task in tasks)
            {
                // Set the ProjectId of each task to the given projectId
                task.ProjectId = projectId;

                // Add the task to the database
                _context.Task.Add(task);
            }

            // Save changes to persist the tasks
            _context.SaveChanges();

            return existingProject; // Return the updated project
        }
        public Project RemoveProject(int projectID)
            {
            var existingProject = _context.Project.FirstOrDefault(t => t.projectId == projectID);
            if (existingProject == null)
            {
                return null;
            }

            _context.Project.Remove(existingProject);
            _context.SaveChanges();
            return existingProject;
        }

            public Project UpdateProject(Project project)
            {
            
                var existingProject = _context.Project.AsNoTracking().FirstOrDefault(u => u.projectId == project.projectId);
                if (existingProject == null)
                {
                    throw new ArgumentException("Project not found");
                }

                _context.Project.Update(project);
                _context.SaveChanges();
                return project;
            }
        public Project AddUserToProject(int userId, int projectId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            var project = _context.Projects.FirstOrDefault(p => p.projectId == projectId);

            if (user == null || project == null)
            {
                throw new NotFoundException("User or Project not found");
            }

            bool alreadyExists = _context.UserProjects.Any(up => up.UserId == userId && up.ProjectId == projectId);
            if (alreadyExists)
            {
                throw new BadRequestException("User is already part of the project");
            }

            var userProject = new UserProject
            {
                UserId = userId,
                ProjectId = projectId
            };

            _context.UserProjects.Add(userProject);
            _context.SaveChanges();
            return project;
        }

        public class NotFoundException : Exception
        {
            public NotFoundException(string message) : base(message) { }
        }

        public class BadRequestException : Exception
        {
            public BadRequestException(string message) : base(message) { }
        }



    }

}


