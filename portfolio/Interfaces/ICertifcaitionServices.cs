using portfolio_web.Dto.Certification;

namespace portfolio.Interfaces
{
    public interface ICertificateService
    {
        Task<IEnumerable<CertificateDto>> GetAllAsync();

        Task<CertificateDto?> GetByIdAsync(int id);

        Task<CertificateDto> CreateAsync(CreateUpdateCertificateDto dto, string userId);

        Task<bool> UpdateAsync(int id, CreateUpdateCertificateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}