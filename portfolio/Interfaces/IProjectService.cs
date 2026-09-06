using portfolio_web.Dto.Project;

namespace portfolio_web.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllAsync();

        Task<ProjectDto?> GetByIdAsync(int id);

        Task<ProjectDto> CreateAsync(CreateUpdateProjectDto dto, string userid);

        Task<bool> UpdateAsync(int id, CreateUpdateProjectDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
