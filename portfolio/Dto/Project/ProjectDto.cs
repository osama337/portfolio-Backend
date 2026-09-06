namespace portfolio_web.Dto.Project
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? GithubUrl { get; set; }

        public string? LiveDemoUrl { get; set; }

        public string? Image { get; set; }

        public bool IsFeatured { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
