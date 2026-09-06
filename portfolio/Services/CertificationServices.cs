using Microsoft.EntityFrameworkCore;
using portfolio.Data;
using portfolio.Interfaces;
using portfolio.Models;
using portfolio_web.Dto.Certification;

namespace portfolio.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly ApplicationDbContext _context;

        public CertificateService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CertificateDto>> GetAllAsync()
        {
            return await _context.certificates
                .Select(c => new CertificateDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Issuer = c.Issuer,
                    IssueDate = c.IssueDate,
                    CreatedAt = c.CreatedAt,
                    ImagePath = c.ImagePath
                })
                .ToListAsync();
        }

        public async Task<CertificateDto?> GetByIdAsync(int id)
        {
            return await _context.certificates
                .Where(c => c.Id == id)
                .Select(c => new CertificateDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Issuer = c.Issuer,
                    IssueDate = c.IssueDate,
                    CreatedAt = c.CreatedAt,
                    ImagePath = c.ImagePath
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CertificateDto> CreateAsync(CreateUpdateCertificateDto dto, string userId)
        {
            string savedImagePath = null;

            if (dto.ImagePath != null && dto.ImagePath.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Certificates");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(dto.ImagePath.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.ImagePath.CopyToAsync(fileStream);
                }

                savedImagePath = $"/Uploads/Certificates/{uniqueFileName}";
            }

            var certificate = new Certificate
            {
                Title = dto.Title,
                Issuer = dto.Issuer,
                IssueDate = dto.IssueDate,
                ImagePath = savedImagePath,
                ApplicationUserId = userId
            };

            
            _context.certificates.Add(certificate);
            await _context.SaveChangesAsync();

            
            return new CertificateDto
            {
                Id = certificate.Id,
                Title = certificate.Title,
                Issuer = certificate.Issuer,
                IssueDate = certificate.IssueDate,
                CreatedAt = certificate.CreatedAt,
                ImagePath = certificate.ImagePath
            };
        }

        public async Task<bool> UpdateAsync(int id, CreateUpdateCertificateDto dto)
        {
            var certificate = await _context.certificates.FindAsync(id);

            if (certificate == null)
                return false;

            certificate.Title = dto.Title;
            certificate.Issuer = dto.Issuer;
            certificate.IssueDate = dto.IssueDate;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var certificate = await _context.certificates.FindAsync(id);

            if (certificate == null)
                return false;

            _context.certificates.Remove(certificate);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}