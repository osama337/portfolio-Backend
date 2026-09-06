using portfolio.Models;

public class Certificate
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Issuer { get; set; } = string.Empty;

    public DateTime IssueDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? ImagePath { get; set; }

    // FK
    public string ApplicationUserId { get; set; } = string.Empty;

    public ApplicationUser ApplicationUser { get; set; } = null!;
}