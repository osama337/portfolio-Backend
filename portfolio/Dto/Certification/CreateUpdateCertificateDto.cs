namespace portfolio_web.Dto.Certification
{
    public class CreateUpdateCertificateDto
    {
        public string Title { get; set; } = string.Empty;

        public string Issuer { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public IFormFile? ImagePath { get; set; }

    }
}
