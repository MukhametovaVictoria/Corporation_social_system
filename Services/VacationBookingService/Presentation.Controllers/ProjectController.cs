using Application.DTOs;
using Application.Mappers;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Helpers;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        [ApiController]
        [Route("api/[controller]")]
        public class ProjectController : ControllerBase
        {
            private readonly IProjectService _projectService;
            private readonly IEmployeeService _employeeService;

            public ProjectController(IProjectService projectService, IEmployeeService employeeService)
            {
                _projectService = projectService;
                _employeeService = employeeService;
            }

            [HttpPost]
            public async Task<IActionResult> CreateProject([FromBody] ProjectDto projectDto)
            {
                if (!AuthChecking.CheckIsAuthorized(User, _employeeService))
                {
                    return Unauthorized();
                }
                var projectEntity = ProjectMapper.ToEntity(projectDto);
                if (projectDto == null || projectEntity == null)
                    return BadRequest("Empty data.");
                
                var project = await _projectService.AddProjectAsync(projectEntity);
                return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, ProjectMapper.ToDto(project));
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetProjectById(Guid id)
            {
                if (id == Guid.Empty)
                    return BadRequest("Empty data.");

                if (!AuthChecking.CheckIsAuthorized(User, _employeeService))
                {
                    return Unauthorized();
                }

                var project = await _projectService.GetProjectByIdAsync(id);
                if (project == null) return NotFound();
                return Ok(ProjectMapper.ToDto(project));
            }

            [HttpGet]
            public async Task<IActionResult> GetAllProjects()
            {
                if (!AuthChecking.CheckIsAuthorized(User, _employeeService))
                {
                    return Unauthorized();
                }
                var projects = await _projectService.GetAllProjectsAsync();
                return Ok(projects);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateProject(Guid id, [FromBody] ProjectDto projectDto)
            {
                if (id == Guid.Empty || projectDto == null)
                    return BadRequest("Empty data.");

                if (!AuthChecking.CheckIsAuthorized(User, _employeeService))
                {
                    return Unauthorized();
                }
                var project = await _projectService.GetProjectByIdAsync(id);
                if (project == null) return NotFound();
                
                project.Name = projectDto.Name;
                project.ModifiedOn = DateTime.UtcNow;

                await _projectService.UpdateProjectAsync(project);
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteProject(Guid id)
            {
                if (id == Guid.Empty)
                    return BadRequest("Empty data.");

                if (!AuthChecking.CheckIsAuthorized(User, _employeeService))
                {
                    return Unauthorized();
                }
                await _projectService.DeleteProjectAsync(id);
                return NoContent();
            }

            [HttpGet("{employeeId}")]
            public async Task<IActionResult> GetProjectsByEmployeeId(Guid employeeId)
            {
                if (employeeId == Guid.Empty)
                    return BadRequest("Empty data.");

                if (!AuthChecking.CheckIsAuthorized(User, _employeeService))
                {
                    return Unauthorized();
                }
                var projects = await _projectService.GetProjectsByEmployeeIdAsync(employeeId);
                return Ok(projects);
            }
        }
    }
}