using Microsoft.EntityFrameworkCore;
using portfolio.Data;
using portfolio_web.Dto.Project;
using portfolio_web.Interfaces;

namespace portfolio_web.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _context;

        public ProjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        private static string? CleanImagePath(string? path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return null;
            if (path.Contains("557821552") || path.Contains("unsplash.com"))
                return null;
            return path;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            var projects = await _context.projects.ToListAsync();
            return projects.Select(p => new ProjectDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                GithubUrl = p.GithubUrl,
                LiveDemoUrl = p.LiveDemoUrl,
                Image = CleanImagePath(p.ImagePath),
                IsFeatured = p.IsFeatured,
                CreatedAt = p.CreatedAt
            });
        }

        public async Task<ProjectDto> CreateAsync(CreateUpdateProjectDto dto, string userid)
        {
            string? savedImagePath = null;

            if (dto.Image != null && dto.Image.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Projects");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(dto.Image.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(fileStream);
                }

                savedImagePath = $"/Uploads/Projects/{uniqueFileName}";
            }

            var project = new Project
            {
                Title = dto.Title,
                Description = dto.Description,
                GithubUrl = dto.GithubUrl,
                LiveDemoUrl = dto.LiveDemoUrl,
                ImagePath = savedImagePath,
                IsFeatured = dto.IsFeatured,
                ApplicationUserId = userid,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.projects.Add(project);
            await _context.SaveChangesAsync();

            return new ProjectDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                GithubUrl = project.GithubUrl,
                LiveDemoUrl = project.LiveDemoUrl,
                Image = project.ImagePath,
                IsFeatured = project.IsFeatured,
                CreatedAt = project.CreatedAt
            };
        }

        public async Task<ProjectDto?> GetByIdAsync(int id)
        {
            var project = await _context.projects.FirstOrDefaultAsync(p => p.Id == id);
            if (project == null)
                return null;

            return new ProjectDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                GithubUrl = project.GithubUrl,
                LiveDemoUrl = project.LiveDemoUrl,
                Image = CleanImagePath(project.ImagePath),
                IsFeatured = project.IsFeatured,
                CreatedAt = project.CreatedAt
            };
        }

        public async Task<bool> UpdateAsync(int id, CreateUpdateProjectDto dto)
        {
            var project = await _context.projects.FindAsync(id);

            if (project == null)
                return false;

            if (dto.Image != null && dto.Image.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Projects");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(dto.Image.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(fileStream);
                }

                project.ImagePath = $"/Uploads/Projects/{uniqueFileName}";
            }

            project.Title = dto.Title;
            project.Description = dto.Description;
            project.GithubUrl = dto.GithubUrl;
            project.LiveDemoUrl = dto.LiveDemoUrl;
            project.IsFeatured = dto.IsFeatured;
            project.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var project = await _context.projects.FindAsync(id);

            if (project == null)
                return false;

            _context.projects.Remove(project);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}