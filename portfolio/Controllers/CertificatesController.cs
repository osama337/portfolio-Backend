using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using portfolio.Interfaces;
using portfolio_web.Dto.Certification;
using System.Security.Claims;

namespace portfolio_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificatesController : ControllerBase
    {
        private readonly ICertificateService _certificateService;

        public CertificatesController(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }

        [HttpGet("AllCerificate")]
        public async Task<IActionResult> GetAll()
        {
            var certificates = await _certificateService.GetAllAsync();

            return Ok(certificates);
        }

        [HttpGet("CertificateByID/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var certificate = await _certificateService.GetByIdAsync(id);

            if (certificate == null)
                return NotFound();

            return Ok(certificate);
        }
        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] CreateUpdateCertificateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var certificate = await _certificateService.CreateAsync(dto, userId);

            return CreatedAtAction(
                nameof(GetById),
                new { id = certificate.Id },
                certificate);
        }
        [Authorize]
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] CreateUpdateCertificateDto dto)
        {
            var updated = await _certificateService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }
        [Authorize]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _certificateService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
     }
}
