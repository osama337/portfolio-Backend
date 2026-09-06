using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portfolio_web.Dto.Project;
using portfolio_web.Interfaces;
using System.Security.Claims;

namespace portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }


        [HttpGet("AllProjects")]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _projectService.GetAllAsync();

            return Ok(projects);
        }


        [HttpGet("ProjectById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _projectService.GetByIdAsync(id);

            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] CreateUpdateProjectDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found");
            }

            if (dto.Image == null || dto.Image.Length == 0)
            {
                return BadRequest("Project image file is required.");
            }

            var project = await _projectService.CreateAsync(dto, userId);

            return CreatedAtAction(
                nameof(GetById),
                new { id = project.Id },
                project);
        }

        [Authorize]
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] CreateUpdateProjectDto dto)
        {
            var updated = await _projectService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [Authorize]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _projectService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}