namespace portfolio_web.Dto.Certification
{
    public class CertificateDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Issuer { get; set; } = string.Empty;

        public DateTime IssueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ImagePath{ get; set; }

    }
}
